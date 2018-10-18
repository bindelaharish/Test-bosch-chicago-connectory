#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    [Route("api/tools")]
    public class ToolsController : Controller
    {
        private readonly Tenant _currentTenant;
        private readonly ILogger<ToolsController> _logger;
        private readonly IRepository _repo;

        public ToolsController(IRepository repository, ILogger<ToolsController> logger, Tenant tenant = null)
        {
            _currentTenant = tenant ?? Tenant.CreateSingleTenant();
            _repo = repository;
            _logger = logger;
        }

        // GET api/tools
        [HttpGet(Order = 1)]
        [SwaggerResponse(200, Type = typeof(ToolModel))]
        [SwaggerResponse(404, Description = "The source was not found")]
        [SwaggerResponse(400, Description = "Your request was not understood")]
        [SwaggerResponse(500, Description = "Oops, something broke..")]
        public IActionResult GetAllTools([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 25)
        {
            _logger.LogInformation("Returning the list of tools");
            IEnumerable<ToolModel> toolModels;
            try
            {
                var tenantDomain = _currentTenant.DomainId;
                // TODO: Validate that the tenant/domain exists and based on the security context, 
                // TODO (continued): that the API caller should have access to it!
                _logger.LogDebug($"Looking up all tool data for data tenant: {tenantDomain})");
                var tools = _repo.Filter(Tool.IsMatching(tenantDomain), out var total, pageNumber, pageSize);
                toolModels = JobAssistantMapper.MapObjects<ToolModel>(tools);
                _logger.LogDebug("Pagination tool models (count): " + total);

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
                _logger.LogError(1, e, "Failed to get all tools in DB repository");
                throw;
            }

            return Ok(toolModels.AsEnumerable());
        }

        /// <summary>
        ///     Example tool id is 'HD18-2'. Enter this as the parameter value and try it out!
        /// </summary>
        /// <remarks>Query database by tool id and return the associated tool data. Specify 'HD18-2' to try out this query API.</remarks>
        [HttpGet("{id}", Order = 10)]
        [SwaggerResponse(200, Type = typeof(ToolModel))]
        public async Task<IActionResult> GetToolById(string id, [FromHeader] string queryBy)
        {
            _logger.LogDebug("Looking up tool data associated to the specified id: " + id);
            var tenantDomain = _currentTenant.DomainId;
            var toolResult = await _repo.All<Tool>()
                .SingleOrDefaultAsync(ApiQueryExpression.GenerateToolPredicate(id, queryBy, tenantDomain));
            _logger.LogDebug("Tool data " + toolResult.Name + " (ToolId = " + toolResult.ToolId + ")");
            var toolModel = JobAssistantMapper.Map<ToolModel>(toolResult);
            _logger.LogDebug("Returning tool data for the specified tool: " + toolModel.Name);
            return Ok(toolModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTool([FromBody] ToolModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var toolData = JobAssistantMapper.Map<Tool>(model);
                await _repo.Create(toolData);
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to create tool in DB repository");
                throw;
            }

            return StatusCode((int)HttpStatusCode.Created); // TODO: Transition to CreatedAtRoute( .. ) and adjust unit tests.
        }

        // PUT api/tools/5
        [HttpPut("{toolId:int}")]
        public async Task<IActionResult> UpdateTool(int toolId, [FromBody] ToolModel model)
        {
            try
            {
                var toolData = JobAssistantMapper.Map<Tool>(model);
                await _repo.Update(toolData);
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to update tool in DB repository");
                throw;
            }

            return Ok();
        }

        // DELETE api/tools/5
        [HttpDelete("{toolId:int}")]
        public /* async */ void DeleteTool(int toolId)
        {
            throw new NotImplementedException("Delete tool not implemented!");
        }
    }
}