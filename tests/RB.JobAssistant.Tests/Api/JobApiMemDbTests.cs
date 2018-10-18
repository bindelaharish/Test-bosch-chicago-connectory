using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RB.JobAssistant.Controllers;
using RB.JobAssistant.Util;
using RB.JobAssistant.Models;
using Xunit;

namespace RB.JobAssistant.Tests.Api
{
    public class JobApiMemDbTests : TestHttpClientApiInMemory
    {
        public JobApiMemDbTests()
        {
            _client = GetClient();
            _logger = ApplicationLogging.CreateTypeLogger<JobsController>();
        }

        private readonly HttpClient _client;

        private readonly ILogger<JobsController> _logger;

        [Fact]
        public async Task GetAllValidJobs()
        {
            _client.DefaultRequestHeaders.Add(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var response = await _client.GetAsync($"/api/jobs");
            _logger.LogDebug("HTTP GET of Jobs returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("HTTP GET of Jobs returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var jobs = JsonConvert.DeserializeObject<JobModel[]>(jsonContent);
            Assert.NotNull(jobs);
            _logger.LogDebug("HTTP GET of Jobs returned a count of N jobs: " + jobs.Length);
            Assert.True(jobs.Length > 0);
            Assert.All(jobs, j => Assert.False(string.IsNullOrWhiteSpace(j.Name)));
            Assert.All(jobs, j => Assert.False(j.JobId <= 0));
        }

        [Fact]
        public async Task GetSingleValidJob()
        {
            _client.DefaultRequestHeaders.Add(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var response = await _client.GetAsync($"/api/jobs/Fasten");
            _logger.LogDebug("HTTP GET of Jobs returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("HTTP GET of Job returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var job = JsonConvert.DeserializeObject<JobModel>(jsonContent);
            Assert.NotNull(job);
        }

    }
}