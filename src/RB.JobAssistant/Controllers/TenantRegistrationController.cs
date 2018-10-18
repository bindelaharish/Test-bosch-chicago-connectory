#pragma warning disable 1591
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RB.JobAssistant.Data;
using RB.JobAssistant.Models;
using RB.JobAssistant.Models.Mapper;
using RB.JobAssistant.Repo;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RB.JobAssistant.Controllers
{
    [Produces("application/json")]
    [Route("api/tenants")]
    public class TenantRegistrationController : Controller
    {
        // TODO: Bring together aspects of the Tenant and security context 

        private readonly ILogger<TenantRegistrationController> _logger;
        private readonly IRepository _repo;

        public TenantRegistrationController(IRepository repository, ILogger<TenantRegistrationController> logger)
        {
            _repo = repository;
            _logger = logger;
        }

        // TODO: Limit this API to the admin security context
        [HttpGet(Order = 1)]
        [SwaggerResponse(200, Description = "Returned tenants successfully")]
        [SwaggerResponse(404, Description = "The source was not found")]
        [SwaggerResponse(500, Description = "Oops, something broke..")]
        public IActionResult GetAllTenants()
        {
            var tenantData = _repo.Filter(Tenant.IsValid());
            var tenantModels = JobAssistantMapper.MapObjects<TenantModel>(tenantData);
            _logger.LogDebug("Pagination tenant models (count): " + tenantModels.Count);
            return Ok(tenantModels.AsEnumerable());
        }

        [HttpPost]
        [SwaggerResponse(200, Description = "Create tenant successfully")]
        [SwaggerResponse(404, Description = "The source was not found")]
        [SwaggerResponse(500, Description = "Oops, something broke..")]
        public async Task<IActionResult> CreateAndRegisterTenant([FromBody] TenantModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var tenantData = JobAssistantMapper.Map<Tenant>(model);
                await _repo.Create(tenantData);
                _logger.LogDebug("Self-registered the new tenant: " + tenantData.DomainId);
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to create tenant in DB repository");
                throw;
            }

            return Ok();
        }

        [HttpGet("{domainId}", Order = 10)]
        [SwaggerResponse(200, Description = "Returned tenant by domain id successfully")]
        [SwaggerResponse(404, Description = "The source was not found")]
        [SwaggerResponse(500, Description = "Oops, something broke..")]
        public async Task<IActionResult> GetTenantById(string domainId)
        {
            _logger.LogDebug("Returning the specified tenant: " + domainId);
            var tenantResult = await _repo.All<Tenant>().SingleAsync(t => t.DomainId == domainId);
            var tenantModel = JobAssistantMapper.Map<TenantModel>(tenantResult);
            return Ok(tenantModel);
        }
    }
}