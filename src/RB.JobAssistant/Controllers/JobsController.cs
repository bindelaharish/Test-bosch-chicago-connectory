#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RB.JobAssistant.Data;
using RB.JobAssistant.Filters;
using RB.JobAssistant.Links;
using RB.JobAssistant.Models;
using RB.JobAssistant.Models.Mapper;
using RB.JobAssistant.Repo;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RB.JobAssistant.Controllers
{
    [Route("api/jobs")]
    [ValidateModel]
    public class JobsController : Controller
    {
        private readonly Tenant _currentTenant;

        private readonly ILogger<JobsController> _logger;
        private readonly IRepository _repo;

        public JobsController(IRepository repository, ILogger<JobsController> logger, Tenant tenant = null)
        {
            _currentTenant = tenant ?? Tenant.CreateSingleTenant();
            _repo = repository;
            _logger = logger;
        }

        /// <summary>
        /// No parameter value input is required. Invoke HTTP GET to review list of jobs!
        /// </summary>
        /// <remarks>Query database and return all specified jobs.</remarks>
        [HttpGet(Order = 1)]
        [SwaggerResponse(200, Type = typeof(IEnumerable<JobModel>))]
        [SwaggerResponse(404, Description = "The source was not found")]
        [SwaggerResponse(500, Description = "Oops, something broke..")]
        public IActionResult GetAllJobs([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 25)
        {
            _logger.LogInformation("Returning the list of jobs");
            IEnumerable<JobModel> jobModels;
            try
            {
                var tenantDomain = _currentTenant.DomainId;
                _logger.LogDebug($"Looking up all job data for data tenant: {tenantDomain})");
                var jobs = _repo.Filter(Job.IsMatching(tenantDomain), out var total, pageNumber, pageSize);
                _logger.LogDebug("Pagination job models (count): " + total);
                jobModels = JobAssistantMapper.MapObjects<JobModel>(jobs);

                var paginationMetadata = new
                {
                    totalCount = total,
                    pageSize,
                    pageNumber,
                    totalPages = total / pageSize
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to get all accessories in DB repository");
                throw;
            }
            return Ok(jobModels.AsEnumerable());
        }

        /// <summary>
        /// Example job id is 'JXZ'. Enter this as the parameter value and try it out!
        /// </summary>
        /// <remarks>Query database by job id and return the associated job data. Specify 'JXZ' to try out this query API.</remarks>
        [HttpGet("{id}", Order = 10)]
        [SwaggerResponse(200, Type = typeof(JobModel))]
        [SwaggerResponse(404, Description = "The source was not found")]
        [SwaggerResponse(500, Description = "Oops, something broke..")]
        [ValidateJobExists]
        public async Task<IActionResult> GetByJobId(string id, [FromHeader] string queryBy)
        {
            var tenantDomain = _currentTenant.DomainId;
            _logger.LogDebug($"Looking up job data for the specified: {id} (using data tenant: {tenantDomain})");
            var jobResult = await _repo.All<Job>().FirstOrDefaultAsync(ApiQueryExpression.GenerateJobPredicate(id, queryBy, tenantDomain));
            var jobModel = JobAssistantMapper.Map<JobModel>(jobResult);
            _logger.LogDebug("Returning job data for the specified job: " + jobModel.Name);

            var helper = new JobLinksBuilder();
            var modelWithLinks = helper.ToModelWithLinks(jobModel, HttpContext.Request);
            return Ok(modelWithLinks);
        }

        // GET api/jobs/5/materials
        [HttpGet("{jobId:int}/materials", Order = 100)]
        [SwaggerResponse(200, Type = typeof(IEnumerable<MaterialModel>))]
        [SwaggerResponse(404, Description = "The source was not found")]
        [SwaggerResponse(500, Description = "Oops, something broke..")]
        public IActionResult GetAssociatedMaterialsByJobId(int jobId)
        {
            _logger.LogDebug("Looking up materials for the specified job: " + jobId);
            var theJob = _repo.All<Job>().Include(j => j.Materials).Single(j => j.JobId == jobId);
            var jobModel = JobAssistantMapper.Map<JobModel>(theJob);
            _logger.LogDebug("Returning the associated material data for the specified job: " + jobModel.Name);
            return Ok(JobAssistantMapper.MapObjects(jobModel.Materials));
        }

        // GET api/jobs/5/tools
        [HttpGet("{jobId:int}/tools", Order = 200)]
        [SwaggerResponse(200, Type = typeof(IEnumerable<ToolModel>))]
        [SwaggerResponse(404, Description = "The source was not found")]
        [SwaggerResponse(500, Description = "Oops, something broke..")]
        [ValidateJobExists]
        public IActionResult GetAssociatedToolsByJobId(int jobId)
        {
            _logger.LogDebug("Looking up tools for the specified job: " + jobId);
            var theJob = _repo.All<Job>().Include(j => j.ToolRelationships).Single(j => j.JobId == jobId);
            var jobModel = JobAssistantMapper.Map<JobModel>(theJob);
            _logger.LogDebug("Returning the associated tool data for the specified job: " + jobModel.Name);
            return Ok(JobAssistantMapper.MapObjects(jobModel.Tools));
        }

        // GET api/jobs/5/accessories
        [HttpGet("{jobId:int}/accessories", Order = 300)]
        [ValidateJobExists]
        public IActionResult GetAssociatedAccessoriesByJobId(int jobId)
        {
            _logger.LogDebug("Looking up accessories for the specified job: " + jobId);
            var theJob = _repo.All<Job>().Include(j => j.ToolRelationships).Single(j => j.JobId == jobId);
            var jobModel = JobAssistantMapper.Map<JobModel>(theJob);
            _logger.LogDebug("Returning the associated accessory data for the specified job: " + jobModel.Name);
            return Ok(JobAssistantMapper.MapObjects(jobModel.Accessories));
        }

        // POST api/jobs
        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] JobModel model)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            try
            {
                var jobData = JobAssistantMapper.Map<Job>(model);
                await _repo.Create(jobData);
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to create job in DB repository");
                throw;
            }

            return Ok();
        }

        // PUT api/jobs/5
        [HttpPut("{jobId:int}")]
        [ValidateJobExists]
        public async Task<IActionResult> UpdateJob(int jobId, [FromBody] JobModel model)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            try
            {
                var jobData = JobAssistantMapper.Map<Job>(model);
                await _repo.Update(jobData);
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to update job in DB repository");
                throw;
            }

            return Ok();
        }

        // DELETE api/jobs/5
        [HttpDelete("{jobId:int}")]
        [ValidateJobExists]
        public /* async */ void DeleteJob(int jobId)
        {
            throw new NotImplementedException("Delete job not implemented!");
        }
    }
}