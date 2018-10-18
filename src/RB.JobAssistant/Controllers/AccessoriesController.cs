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
using RB.JobAssistant.Models;
using RB.JobAssistant.Models.Mapper;
using RB.JobAssistant.Repo;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RB.JobAssistant.Controllers
{
    [Route("api/accessories")]
    public class AccessoriesController : Controller
    {
        private readonly Tenant _currentTenant;
        private readonly ILogger<AccessoriesController> _logger;
        private readonly IRepository _repo;

        public AccessoriesController(IRepository repository, ILogger<AccessoriesController> logger, Tenant tenant = null)
        {
            _currentTenant = tenant ?? Tenant.CreateSingleTenant();
            _repo = repository;
            _logger = logger;
        }

        // GET api/accessories
        [HttpGet(Order = 1)]
        [SwaggerResponse(200, Type = typeof(IEnumerable<AccessoryModel>))]
        [SwaggerResponse(404, Description = "The source was not found")]
        [SwaggerResponse(400, Description = "Your request was not understood")]
        [SwaggerResponse(500, Description = "Oops, something broke..")]
        [Produces(typeof(IEnumerable<AccessoryModel>))]
        public IActionResult GetAllAccessories([FromQuery] int pageNumber = 0,
            [FromQuery] int pageSize = 25)
        {
            _logger.LogInformation("Returning the list of accessories");
            IEnumerable<AccessoryModel> accessoryModels;
            try
            {
                var tenantDomain = _currentTenant.DomainId;
                _logger.LogDebug($"Looking up all accessory data for data tenant: {tenantDomain})");
                var accessories = _repo.Filter(Accessory.IsMatching(tenantDomain), out int total, pageNumber, pageSize);
                _logger.LogDebug("Pagination accessory models (count): " + total);
                accessoryModels = JobAssistantMapper.MapObjects<AccessoryModel>(accessories);

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

            return Ok(accessoryModels.AsEnumerable());
        }

        /// <summary>
        ///     Example accessory id is 'DSB5010'. Enter this as the parameter value and try it out!
        /// </summary>
        /// <remarks>
        ///     Query database by accessory id and return the associated accessory data. Specify 'DSB5010' to try out this
        ///     query API.
        /// </remarks>
        [HttpGet("{id}", Order = 10)]
        [SwaggerResponse(200, Type = typeof(AccessoryModel))]
        public async Task<IActionResult> GetAccessoryById(string id, [FromHeader] string queryBy)
        {
            _logger.LogDebug("Looking up accessory data for the specified id: " + id);
            var tenantDomain = _currentTenant.DomainId;
            var accessoryResult = await _repo.All<Accessory>()
                .SingleOrDefaultAsync(ApiQueryExpression.GenerateAccessoryPredicate(id, queryBy, tenantDomain));
            var accessoryModel = JobAssistantMapper.Map<AccessoryModel>(accessoryResult);
            _logger.LogDebug("Returning accessory data for the specified accessory: " + accessoryModel.Name);
            return Ok(accessoryModel);
        }

        // POST api/accessories
        [HttpPost]
        public async Task<IActionResult> CreateAccessory([FromBody] AccessoryModel model)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            try
            {
                var accessoryData = JobAssistantMapper.Map<Accessory>(model);
                await _repo.Create(accessoryData);
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to create accessory in DB repository");
                throw;
            }

            return CreatedAtRoute(new {id = model.AccessoryId}, model);
        }

        // PUT api/accessory/5
        [HttpPut("{accessoryId:int}")]
        public async Task<IActionResult> UpdateAccessory(int accessoryId, [FromBody] AccessoryModel model)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            try
            {
                var accessoryData = JobAssistantMapper.Map<Accessory>(model);
                await _repo.Update(accessoryData);
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to update accessory in DB repository");
                throw;
            }

            return Ok();
        }

        // DELETE api/accessory/5
        [HttpDelete("{accessoryId:int}")]
        public /* async */ void DeleteAccessory(int accessoryId)
        {
            throw new NotImplementedException("Delete accessory not implemented!");
        }
    }
}