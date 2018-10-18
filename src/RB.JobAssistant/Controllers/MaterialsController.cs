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
using RB.JobAssistant.Models;
using RB.JobAssistant.Models.Mapper;
using RB.JobAssistant.Repo;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RB.JobAssistant.Controllers
{
    [Route("api/materials")]
    public class MaterialsController : Controller
    {
        private readonly Tenant _currentTenant;
        private readonly ILogger<MaterialsController> _logger;
        private readonly IRepository _repo;

        public MaterialsController(IRepository repository, ILogger<MaterialsController> logger, Tenant tenant = null)
        {
            _currentTenant = tenant ?? Tenant.CreateSingleTenant();
            _repo = repository;
            _logger = logger;
        }

        [HttpGet(Order = 1)]
        [SwaggerResponse(200, Type = typeof(IEnumerable<MaterialModel>))]
        [SwaggerResponse(404, Description = "The source was not found")]
        [SwaggerResponse(400, Description = "Your request was not understood")]
        [SwaggerResponse(500, Description = "Oops, something broke..")]
        public IActionResult GetAllMaterials([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 25)
        {
            _logger.LogInformation("Returning the list of materials");
            IEnumerable<MaterialModel> materialModels;
            try
            {
                var tenantDomain = _currentTenant.DomainId;
                // TODO: Validate that the tenant/domain exists and based on the security context, 
                // TODO (continued): that the API caller should have access to it!
                _logger.LogDebug($"Looking up all material data for data tenant: {tenantDomain})");
                var materials = _repo.Filter(Material.IsMatching(tenantDomain), out var total, pageNumber, pageSize);
                _logger.LogDebug("Pagination material models (count): " + total);
                materialModels = JobAssistantMapper.MapObjects<MaterialModel>(materials);

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
                _logger.LogError(1, e, "Failed to get all materials in DB repository");
                throw;
            }

            return Ok(materialModels.AsEnumerable());
        }

        /// <summary>
        ///     Example material id is '100'. Enter this as the parameter value and try it out!
        /// </summary>
        /// <remarks>
        ///     Query database by material id and return the associated material data. Specify '100' to try out this query
        ///     API.
        /// </remarks>
        [HttpGet("{id}", Order = 10)]
        [SwaggerResponse(200, Type = typeof(MaterialModel))]
        [ValidateMaterialExists]
        public async Task<IActionResult> GetByMaterialId(string id, [FromHeader] string queryBy)
        {
            _logger.LogDebug("Returning material data for the specified id: " + id);
            var tenantDomain = _currentTenant.DomainId;
            // TODO: Validate that the tenant/domain exists and based on the security context, 
            // TODO (continued): that the API caller should have access to it!
            var materialResult = await _repo.All<Material>().Include(m => m.Applications)
                .FirstOrDefaultAsync(ApiQueryExpression.GenerateMaterialPredicate(id, queryBy, tenantDomain));
            var materialModel = JobAssistantMapper.Map<MaterialModel>(materialResult);
            return Ok(materialModel);
        }

        // GET api/materials/5/applications
        [HttpGet("{materialId:int}/applications", Order = 100)]
        [SwaggerResponse(200, Type = typeof(IEnumerable<ApplicationModel>))]
        [ValidateMaterialExists]
        public async Task<IActionResult> GetApplicationsByMaterialId(int materialId)
        {
            _logger.LogDebug("Returning associated applications for the specified material: " + materialId);
            // TODO: Validate that the tenant/domain exists and based on the security context, 
            // TODO (continued): that the API caller should have access to it!
            var materialResult = await _repo.All<Material>().Include(m => m.Applications)
                .SingleAsync(m => m.MaterialId == materialId);
            var materialModel = JobAssistantMapper.Map<MaterialModel>(materialResult);
            _logger.LogDebug("Mapped application models (count): " + materialModel.Applications.Count);
            return Ok(materialModel.Applications);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMaterial([FromBody] MaterialModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var materialData = JobAssistantMapper.Map<Material>(model);
                // TODO: Validate that the tenant/domain exists and based on the security context, that the API caller should have access to it!
                // TODO: var tenant = await _repo.All<Tenant>().SingleAsync(t => t.DomainId == model.TenantDomain);
                await _repo.Create(materialData);
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to create material in DB repository");
                throw;
            }

            return Ok();
        }

        // PUT api/materials/5
        [HttpPut("{materialId:int}")]
        [ValidateMaterialExists]
        public async Task<IActionResult> UpdateMaterial(int materialId, [FromBody] MaterialModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var materialData = JobAssistantMapper.Map<Material>(model);
                await _repo.Update(materialData);
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to update material in DB repository");
                throw;
            }

            return Ok();
        }

        // DELETE api/materials/5
        [HttpDelete("{materialId:int}")]
        [ValidateMaterialExists]
        public /* async */ void DeleteMaterial(int materialId)
        {
            throw new NotImplementedException("Delete material not implemented!");
        }
    }
}