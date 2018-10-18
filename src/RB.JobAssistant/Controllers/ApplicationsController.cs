#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RB.JobAssistant.Data;
using RB.JobAssistant.Filters;
using RB.JobAssistant.Models;
using RB.JobAssistant.Models.Mapper;
using RB.JobAssistant.Repo;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RB.JobAssistant.Controllers
{
    [Route("api/applications")]
    public class ApplicationsController : Controller
    {
        private readonly Tenant _currentTenant;
        private readonly ILogger<ApplicationsController> _logger;
        private readonly IRepository _repo;

        public ApplicationsController(IRepository repository, ILogger<ApplicationsController> logger, Tenant tenant = null)
        {
            _currentTenant = tenant ?? Tenant.CreateSingleTenant();
            _repo = repository;
            _logger = logger;
        }

        // GET api/applications
        [HttpGet(Order = 1)]
        [SwaggerResponse(200, Type = typeof(IEnumerable<ApplicationModel>))]
        [SwaggerResponse(404, Description = "The source was not found")]
        [SwaggerResponse(400, Description = "Your request was not understood")]
        [SwaggerResponse(500, Description = "Oops, something broke..")]
        [Produces(typeof(IEnumerable<ApplicationModel>))]
        public IActionResult GetAllApplications([FromQuery] int pageNumber = 0,
            [FromQuery] int pageSize = 25)
        {
            _logger.LogInformation("Returning the list of applications");
            IEnumerable<ApplicationModel> applicationModels;
            try
            {
                var tenantDomain = _currentTenant.DomainId;
                _logger.LogDebug($"Looking up all application data for data tenant: {tenantDomain})");
                var applications = _repo.Filter(Application.IsMatching(tenantDomain), out var total, pageNumber, pageSize);
                applicationModels = JobAssistantMapper.MapObjects<ApplicationModel>(applications);
                _logger.LogDebug("Pagination application models (count): " + total);
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to get all applications in DB repository");
                throw;
            }

            return Ok(applicationModels.AsEnumerable());
        }

        /// <summary>
        ///     Example application id is '61'. Enter this as the parameter value and try it out!
        /// </summary>
        /// <remarks>
        ///     Query database by application id and return the associated application data. Specify '61' to try out this
        ///     query API.
        /// </remarks>
        [HttpGet("{id}", Order = 10)]
        [SwaggerResponse(200, Type = typeof(ApplicationModel))]
        [ValidateApplicationExists]
        public async Task<IActionResult> GetByApplicationId(string id, [FromHeader] string queryBy)
        {
            var tenantDomain = _currentTenant.DomainId;
            var applicationResult = await _repo.All<Application>().Include(a => a.ToolRelationships)
                .Include(a => a.AccessoryRelationships)
                .FirstOrDefaultAsync(ApiQueryExpression.GenerateApplicationPredicate(id, queryBy, tenantDomain));
            var applicationModel = JobAssistantMapper.Map<ApplicationModel>(applicationResult);
            return Ok(applicationModel);
        }

        /// <summary>
        ///     Example application id is '61'. Enter this as the parameter value and try it out!
        /// </summary>
        /// <remarks>
        ///     Query database by application id and return the associated accessories. Specify '61' to try out this query
        ///     API.
        /// </remarks>
        [HttpGet("{applicationId:int}/accessories", Order = 20)]
        [SwaggerResponse(200, Type = typeof(IEnumerable<AccessoryModel>))]
        public async Task<IActionResult> GetAssociatedAccessoriesByApplicationId(int applicationId)
        {
            var applicationResult = await _repo.All<Application>().Include(a => a.AccessoryRelationships)
                .SingleAsync(a => a.ApplicationId == applicationId);
            var applicationModel = JobAssistantMapper.Map<ApplicationModel>(applicationResult);
            new LateModelDataBinder(_repo).BindRelatedAccessoryDataToModel(applicationModel.Accessories);
            return Ok(applicationModel.Accessories);
        }

        /// <summary>
        ///     Example application id is '61'. Enter this as the parameter value and try it out!
        /// </summary>
        /// <remarks>Query database by application id and return the associated tools. Specify '61' to try out this query API.</remarks>
        [HttpGet("{applicationId:int}/tools", Order = 30)]
        [SwaggerResponse(200, Type = typeof(IEnumerable<ToolModel>))]
        public async Task<IActionResult> GetAssociatedToolsByApplicationId(int applicationId)
        {
            var applicationResult = await _repo.All<Application>().Include(a => a.ToolRelationships)
                .SingleAsync(a => a.ApplicationId == applicationId);
            var applicationModel = JobAssistantMapper.Map<ApplicationModel>(applicationResult);
            new LateModelDataBinder(_repo).BindRelatedToolDataToModel(applicationModel.Tools);
            return Ok(applicationModel.Tools);
        }

        [HttpPost]
        public async Task<IActionResult> CreateApplication([FromBody] ApplicationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var applicationData = JobAssistantMapper.Map<Application>(model);
                await _repo.Create(applicationData);
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to create application in DB repository");
                throw;
            }

            return Ok();
        }

        // PUT api/applications/5
        [HttpPut("{applicationId:int}")]
        public async Task<IActionResult> UpdateApplication(int applicationId, [FromBody] ApplicationModel model)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            try
            {
                var applicationData = JobAssistantMapper.Map<Application>(model);
                await _repo.Update(applicationData);
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to update application in DB repository");
                throw;
            }

            return Ok();
        }

        // DELETE api/applications/5
        [HttpDelete("{applicationId:int}")]
        public /* async */ void DeleteApplication(int applicationId)
        {
            throw new NotImplementedException("Delete application not implemented!");
        }
    }
}