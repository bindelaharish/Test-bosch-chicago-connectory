using System.Net;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RB.JobAssistant.Controllers;
using RB.JobAssistant.Links;
using RB.JobAssistant.Models;
using RB.JobAssistant.Tests.Api;
using RB.JobAssistant.Util;
using RestSharp.Portable;
using Xunit;

namespace RB.JobAssistant.Tests.Filters
{
    public class JobFilterRestSharpMemDbTests : TestSharpHttpClientApiKestrel
    {
        public JobFilterRestSharpMemDbTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<JobsController>();
        }

        private readonly ILogger<JobsController> _logger;

        [Fact]
        [Trait("Category", "Api-Sharp")]
        public async void GetGrindJob()
        {
            var client = GetClient();
            var request = RestSharpApiClientHelper.BuildBoschBlueRequest(Method.GET, "api/jobs/grind");
            request.AddHeader("QueryBy", "JobName");
            var response = await client.Execute(request);
            Assert.NotNull(response);
            _logger.LogDebug("HTTP GET of Jobs returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = response.Content;
            _logger.LogDebug("HTTP GET of Jobs returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var jobLink = JsonConvert.DeserializeObject<LinksWrapper<JobModel>>(jsonContent);
            Assert.NotNull(jobLink);
            Assert.Equal("[ Grind ]", jobLink.Value.Name);

            client.Dispose();
        }
    }
}