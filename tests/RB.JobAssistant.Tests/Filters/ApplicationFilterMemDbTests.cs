using System.Net;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RB.JobAssistant.Controllers;
using RB.JobAssistant.Models;
using RB.JobAssistant.Tests.Api;
using RB.JobAssistant.Util;
using RestSharp.Portable;
using Xunit;

namespace RB.JobAssistant.Tests.Filters
{
    public class ApplicationFilterRestSharpMemDbTests : TestSharpHttpClientApiKestrel
    {
        public ApplicationFilterRestSharpMemDbTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<ApplicationsController>();
        }

        private readonly ILogger<ApplicationsController> _logger;

        [Fact]
        [Trait("Category", "Api-Sharp")]
        public async void GetPlane()
        {
            var client = GetClient();
            var request = RestSharpApiClientHelper.BuildBoschBlueRequest(Method.GET, "api/applications/plane");
            request.AddHeader("QueryBy", "ApplicationName");
            var response = await client.Execute(request);
            Assert.NotNull(response);
            _logger.LogDebug("HTTP GET of Applications returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = response.Content;
            _logger.LogDebug("HTTP GET of Applications returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var applicationData = JsonConvert.DeserializeObject<ApplicationModel>(jsonContent);
            Assert.NotNull(applicationData);

            client.Dispose();
        }

    }
}