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
    public class ValidateApplicationExistsAttribute : TypeFilterAttribute
    {
        public ValidateApplicationExistsAttribute() : base(typeof(ValidateApplicationExistsFilterImpl))
        {

        }
    }

    public class ValidateApplicationExistsFilterImpl : IAsyncActionFilter
    {
        private readonly IRepository _repository;
        private readonly ILogger<ValidateApplicationExistsFilterImpl> _logger;

        public ValidateApplicationExistsFilterImpl(IRepository repo, ILogger<ValidateApplicationExistsFilterImpl> logger)
        {
            _repository = repo;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("Entered ApplicationExists filter");

            if (context.ActionArguments.ContainsKey("applicationId"))
            {
                _logger.LogInformation("Found application id argument");
                if (context.ActionArguments["applicationId"] is int applicationId)
                {
                    _logger.LogInformation("Has integer-based id");
                    if (!_repository.Contains<Application>(a => a.ApplicationId == applicationId))
                    {
                        _logger.LogInformation("Application associated to id is not found");
                        context.Result = new NotFoundObjectResult(applicationId);
                        return;
                    }
                }
            }
            else if (context.ActionArguments.ContainsKey("id"))
            {
                var applicationId = context.ActionArguments["id"] as string;
                _logger.LogInformation("Found expected id argument in action. Guid is {0}.", applicationId);
                var queryExpression = ApiQueryExpression.GenerateApplicationPredicate(applicationId, context.HttpContext);
                _logger.LogInformation("Generated Application query expression: {0}", queryExpression.ToString());
                if (!_repository.Contains(queryExpression))
                {
                    _logger.LogInformation("Application associated to id is not found");
                    context.Result = new NotFoundObjectResult(queryExpression.ToString());
                    return;
                }
            }

            await next();
        }
    }
}

