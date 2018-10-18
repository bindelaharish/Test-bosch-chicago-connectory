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
    public class ValidateCategoryExistsAttribute : TypeFilterAttribute
    {
        public ValidateCategoryExistsAttribute() : base(typeof(ValidateCategoryExistsFilterImpl))
        {

        }
    }

    public class ValidateCategoryExistsFilterImpl : IAsyncActionFilter
    {
        private readonly IRepository _repository;

        private readonly ILogger<ValidateCategoryExistsFilterImpl> _logger;

        public ValidateCategoryExistsFilterImpl(IRepository repo, ILogger<ValidateCategoryExistsFilterImpl> logger)
        {
            _repository = repo;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("Entered CategoryExists filter");

            if (context.ActionArguments.ContainsKey("categoryId"))
            {
                _logger.LogInformation("Found id argument");
                if (context.ActionArguments["categoryId"] is int categoryId)
                {
                    _logger.LogInformation("Has integer-based id");
                    if (!_repository.Contains<Category>(c => c.CategoryId == categoryId))
                    {
                        _logger.LogInformation("Category associated to id is not found");
                        context.Result = new NotFoundObjectResult(categoryId);
                        return;
                    }
                }
            }
            else if (context.ActionArguments.ContainsKey("id"))
            {
                var categoryId = context.ActionArguments["id"] as string;
                _logger.LogInformation("Found expected id argument in action. Guid is {0}.", categoryId);
                var queryExpression = ApiQueryExpression.GenerateCategoryPredicate<Category>(categoryId, context.HttpContext);
                _logger.LogInformation("Generated Category query expression: {0}", queryExpression.ToString());
                if (!_repository.Contains(queryExpression))
                {
                    _logger.LogInformation("Category associated to id is not found");
                    context.Result = new NotFoundObjectResult(queryExpression.ToString());
                    return;
                }
            }
            await next();
        }
    }
}