using System.Net.Http;
using Microsoft.Extensions.Logging;
using RB.JobAssistant.Controllers;
using RB.JobAssistant.Util;
using RB.JobAssistant.Models;
using Xunit;

namespace RB.JobAssistant.Tests.Api
{
    public class JobApiIntegrationFixtureTests : IClassFixture<TestFixture<Startup>>
    {
        public JobApiIntegrationFixtureTests(TestFixture<Startup> fixture)
        {
            _client = fixture.Client;
            _logger = ApplicationLogging.CreateTypeLogger<JobsController>();
        }

        private readonly ILogger<JobsController> _logger;

        private readonly HttpClient _client;

        [Fact]
        [Trait("Category", "Api")]
        public async void CreateJobWithApiClient()
        {
            var nextId = CustomRandomNumberHelper.NextInteger();
            var aJob = new JobModel {JobId = nextId, Name = "A test Job"};
            var response = await _client.PostAsJsonAsync("/api/jobs/", aJob);
            response.EnsureSuccessStatusCode();
            _logger.LogDebug(response.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Empty(responseString);
        }
    }
}