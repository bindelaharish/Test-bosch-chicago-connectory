using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RB.JobAssistant.Controllers;
using RB.JobAssistant.Models;
using RB.JobAssistant.Util;
using Xunit;

namespace RB.JobAssistant.Tests.Api
{
    public class ApplicationApiMemDbTests : TestHttpClientApiInMemory
    {
        public ApplicationApiMemDbTests()
        {
            _client = GetClient();
            _logger = ApplicationLogging.CreateTypeLogger<ApplicationsController>();
        }

        private readonly HttpClient _client;

        private readonly ILogger<ApplicationsController> _logger;

        [Fact]
        public async Task GetAllValidApplications()
        {
            _client.DefaultRequestHeaders.Add(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var response = await _client.GetAsync($"/api/applications");
            _logger.LogDebug("HTTP GET of Applications returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("HTTP GET of Applications returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var applications = JsonConvert.DeserializeObject<ApplicationModel[]>(jsonContent);
            Assert.NotNull(applications);
            _logger.LogDebug("HTTP GET of Applications returned a count of N applications: " + applications.Length);
            Assert.True(applications.Length > 0);
            Assert.All(applications, j => Assert.False(string.IsNullOrWhiteSpace(j.Name)));
            Assert.All(applications, j => Assert.False(j.ApplicationId <= 0));
        }

        [Fact]
        public async Task GetDriveApplication()
        {
            _client.DefaultRequestHeaders.Add(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var response = await _client.GetAsync($"/api/applications/Drive");
            _logger.LogDebug("HTTP GET of Applications returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("HTTP GET of Application returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var job = JsonConvert.DeserializeObject<ApplicationModel>(jsonContent);
            Assert.NotNull(job);
        }

        [Fact]
        public async Task GetSurfaceFoamingApplicationWithSpaces()
        {
            _client.DefaultRequestHeaders.Add(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var response = await _client.GetAsync($"/api/applications/Surface Forming App");
            _logger.LogDebug("HTTP GET of Applications returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("HTTP GET of Application returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var job = JsonConvert.DeserializeObject<ApplicationModel>(jsonContent);
            Assert.NotNull(job);
        }
    }
}