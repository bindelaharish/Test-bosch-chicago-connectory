#pragma warning disable 1591 
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

/*
  Visit https://stackoverflow.com/questions/41908957/get-all-registered-routes-in-asp-net-core for more details.
 */

namespace RB.JobAssistant.Controllers
{
    [Route("monitor")]
    public class MonitorController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _provider;

        public MonitorController(IActionDescriptorCollectionProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("routes")]
        public IActionResult GetRoutes()
        {
            var routes = _provider.ActionDescriptors.Items.Select(x => new
            {
                Action = x.RouteValues["Action"],
                Controller = x.RouteValues["Controller"],
                x.AttributeRouteInfo.Name,
                x.AttributeRouteInfo.Template
            }).ToList();
            return Ok(routes);
        }
    }
}