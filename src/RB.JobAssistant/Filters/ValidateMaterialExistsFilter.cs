#pragma warning disable 1591
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using RB.JobAssistant.Controllers;
using RB.JobAssistant.Data;
using RB.JobAssistant.Repo;

namespace RB.JobAssistant.Filters
{
    /**
     * 
     * See https://msdn.microsoft.com/en-us/magazine/mt767699.aspx for a great article on Filters in ASP.NET Core.
     * Also see https://github.com/ardalis/GettingStartedWithFilters for useful source examples.
     * 
     */
    public class ValidateMaterialExistsAttribute : TypeFilterAttribute
    {
        public ValidateMaterialExistsAttribute() : base(typeof(ValidateMaterialExistsFilterImpl))
        {

        }
    }

    public class ValidateMaterialExistsFilterImpl : IAsyncActionFilter
    {
        private readonly IRepository _repository;

        private readonly ILogger<ValidateJobExistsFilterImpl> _logger;

        public ValidateMaterialExistsFilterImpl(IRepository repo, ILogger<ValidateJobExistsFilterImpl> logger)
        {
            _repository = repo;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           _logger.LogInformation("Entered MaterialExists filter");

            if (context.ActionArguments.ContainsKey("materialId"))
            {
                _logger.LogInformation("Found material id argument");
                if (context.ActionArguments["materialId"] is int materialId)
                {
                    _logger.LogInformation("Has integer-based id");
                    if (!_repository.Contains<Material>(m => m.MaterialId == materialId))
                    {
                        _logger.LogInformation("Material associated to id is not found");
                        context.Result = new NotFoundObjectResult(materialId);
                        return;
                    }
                }
            }
            else if (context.ActionArguments.ContainsKey("id"))
            {
                if (context.ActionArguments.ContainsKey("id"))
                {
                    var materialId = context.ActionArguments["id"] as string;
                    _logger.LogInformation("Found expected id argument in action. Guid is {0}.", materialId);
                    var queryExpression = ApiQueryExpression.GenerateMaterialPredicate(materialId, context.HttpContext);
                    _logger.LogInformation("Generated Job query expression: {0}", queryExpression.ToString());
                    if (!_repository.Contains(queryExpression))
                    {
                        _logger.LogInformation("Job associated to id is not found");
                        context.Result = new NotFoundObjectResult(queryExpression.ToString());
                        return;
                    }
                }
            }
            await next();
        }
    }
}