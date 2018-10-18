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
    public class ValidateJobExistsAttribute : TypeFilterAttribute
    {
        public ValidateJobExistsAttribute() : base(typeof(ValidateJobExistsFilterImpl))
        {
        }
    }

    public class ValidateJobExistsFilterImpl : IAsyncActionFilter
    {
        private readonly ILogger<ValidateJobExistsFilterImpl> _logger;
        private readonly IRepository _repository;

        public ValidateJobExistsFilterImpl(IRepository repo, ILogger<ValidateJobExistsFilterImpl> logger)
        {
            _repository = repo;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("Entered JobExists filter");
            if (context.ActionArguments.ContainsKey("jobId"))
            {
                _logger.LogInformation("Found job id argument");
                if (context.ActionArguments["jobId"] is int jobId)
                {
                    _logger.LogInformation("Has integer-based id");
                    if (!_repository.Contains<Job>(m => m.JobId == jobId))
                    {
                        _logger.LogInformation("Job associated to id is not found");
                        context.Result = new NotFoundObjectResult(jobId);
                        return;
                    }
                }
            }
            else if (context.ActionArguments.ContainsKey("id"))
            {
                var jobId = context.ActionArguments["id"] as string;
                _logger.LogInformation("Found expected id argument in action. Guid is {0}.", jobId);
                var queryExpression = ApiQueryExpression.GenerateJobPredicate(jobId, context.HttpContext);
                _logger.LogInformation("Generated Job query expression: {0}", queryExpression.ToString());
                if (!_repository.Contains(queryExpression))
                {
                    _logger.LogInformation("Job associated to id is not found");
                    context.Result = new NotFoundObjectResult(queryExpression.ToString());
                    return;
                }
            }

            await next();
        }
    }
}