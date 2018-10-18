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
    [Route("api/categories")]
    [ValidateModel]
    public class CategoriesController : Controller
    {
        private readonly Tenant _currentTenant;
        private readonly ILogger<CategoriesController> _logger;
        private readonly IRepository _repo;

        public CategoriesController(IRepository repository, ILogger<CategoriesController> logger, Tenant tenant = null)
        {
            _currentTenant = tenant ?? Tenant.CreateSingleTenant();
            _repo = repository;
            _logger = logger;
        }

        // GET api/categories
        [HttpGet(Order = 1)]
        [SwaggerResponse(200, Type = typeof(IEnumerable<CategoryModel>))]
        [SwaggerResponse(404, Description = "The source was not found")]
        [SwaggerResponse(400, Description = "Your request was not understood")]
        [SwaggerResponse(500, Description = "Oops, something broke..")]
        [Produces(typeof(IEnumerable<CategoryModel>))]
        public IActionResult GetAllCategories([FromQuery] int pageNumber = 0,
            [FromQuery] int pageSize = 25)
        {
            _logger.LogInformation("Returning the list of categories");

            var tenantDomain = _currentTenant.DomainId;
            int total;
            _logger.LogDebug($"Looking up all category data for data tenant: {tenantDomain})");
            var categoryData = _repo.Filter(Category.IsValid(), out total, pageNumber, pageSize);
            var categoryModels = JobAssistantMapper.MapObjects<CategoryModel>(categoryData);
            _logger.LogDebug("Pagination category models (count): " + total);
            return Ok(categoryModels.AsEnumerable());
        }

        /// <summary>
        ///     Enter a category id as the parameter value and try it out!
        /// </summary>
        /// <remarks>
        ///     Query database by category id and return the basic category data.  HINT: First, query all categoriese to learn
        ///     specific names and ids, then as needed query by id with this API.
        /// </remarks>
        [HttpGet("{id}", Order = 10)]
        [SwaggerResponse(200, Type = typeof(CategoryModel))]
        [Produces(typeof(CategoryModel))]
        [ValidateCategoryExists]
        public async Task<IActionResult> GetByCategoryId(string id, [FromHeader] string queryBy)
        {
            _logger.LogDebug("Returning the specified category: " + id);
            var tenantDomain = _currentTenant.DomainId;
            var categoryResult = await _repo.All<Category>().Include(c => c.Jobs)
                .SingleAsync(ApiQueryExpression.GenerateCategoryPredicate(id, queryBy, tenantDomain));
            var categoryModel = JobAssistantMapper.Map<CategoryModel>(categoryResult);
            return Ok(categoryModel);
        }

        /// <summary>
        ///     Enter a category id as the parameter value and try it out!
        /// </summary>
        /// <remarks>
        ///     Query database by category id and return the associated job data. HINT: First, query all categoriese to learn
        ///     specific names and ids, then as needed query by id with this API.
        /// </remarks>
        /// >
        [HttpGet("{categoryId:int}/jobs", Order = 100)]
        [SwaggerResponse(200, Type = typeof(IEnumerable<JobModel>))]
        public IActionResult GetJobsByCategoryId(int categoryId)
        {
            _logger.LogDebug("Returning jobs for the specified category: " + categoryId);
            var theCategory = _repo.All<Category>().Include(c => c.Jobs).Single(c => c.CategoryId == categoryId);
            var categoryModel = JobAssistantMapper.Map<CategoryModel>(theCategory);
            return Ok(JobAssistantMapper.MapObjects(categoryModel.Jobs));
        }

        /// <summary>
        ///     Enter a category id as the parameter value and try it out!
        /// </summary>
        /// <remarks>Query database by category id and return the associated material data.</remarks>
        /// >
        [HttpGet("{categoryId:int}/materials", Order = 100)]
        [SwaggerResponse(200, Type = typeof(IEnumerable<MaterialModel>))]
        public IActionResult GetMaterialsByCategoryId(int categoryId)
        {
            _logger.LogDebug("Returning materials for the specified category: " + categoryId);
            var theCategory = _repo.All<Category>().Include(c => c.Materials).Single(c => c.CategoryId == categoryId);
            var categoryModel = JobAssistantMapper.Map<CategoryModel>(theCategory);
            return Ok(JobAssistantMapper.MapObjects(categoryModel.Materials));
        }

        /// <summary>
        ///     Enter a category id as the parameter value and try it out!
        /// </summary>
        /// <remarks>Query database by category id and return the associated child category data.</remarks>
        /// >
        [HttpGet("{categoryId:int}/categories", Order = 200)]
        [SwaggerResponse(200, Type = typeof(IEnumerable<CategoryModel>))]
        public IActionResult GeChildCategoriessByParentCategoryId(int categoryId)
        {
            _logger.LogDebug("Returning child categories for the specified (parent) category: " + categoryId);
            var theCategory = _repo.All<Category>().Include(c => c.Categories).Single(c => c.CategoryId == categoryId);
            var categoryModel = JobAssistantMapper.Map<CategoryModel>(theCategory);
            return Ok(JobAssistantMapper.MapObjects(categoryModel.Categories));
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryModel model)
        {
            _logger.LogDebug("Create category model: " + model);

            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            try
            {
                var categoryData = JobAssistantMapper.Map<Category>(model);
                await _repo.Create(categoryData);
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to create category in DB repository");
                throw;
            }
            return Ok();
        }

        // PUT api/categories/5
        [HttpPut("{categoryId:int}")]
        [ValidateCategoryExists]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryModel model)
        {
            _logger.LogDebug("Update category model: " + model);

            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            try
            {
                var categoryData = JobAssistantMapper.Map<Category>(model);
                await _repo.Update(categoryData);
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to update category in DB repository");
                throw;
            }
            return Ok();
        }

        // DELETE api/categories/5
        [HttpDelete("{categoryId:int}")]
        [ValidateCategoryExists]
        public /* async */ void DeleteCategory(int categoryId)
        {
            throw new NotImplementedException("Delete job not implemented!");
        }
    }
}