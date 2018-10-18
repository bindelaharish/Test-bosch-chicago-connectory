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
    public class MaterialApiLiveDbTests : TestHttpClientApiLiveData
    {
        public MaterialApiLiveDbTests()
        {
            _client = GetClient();
            _logger = ApplicationLogging.CreateTypeLogger<MaterialsController>();
        }

        private readonly HttpClient _client;

        private readonly ILogger<MaterialsController> _logger;

        [Fact]
        public async Task GetAllValidMaterials()
        {
            _client.DefaultRequestHeaders.Add(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var response = await _client.GetAsync($"/api/materials");
            _logger.LogDebug("HTTP GET of materials returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.Headers.Contains("X-Pagination"));
            Assert.NotNull(response.Content);
            var jsonContent = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("HTTP GET of materials returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var materials = JsonConvert.DeserializeObject<MaterialModel[]>(jsonContent);
            Assert.NotNull(materials);
            _logger.LogDebug("HTTP GET of materials returned a count of N materials: " + materials.Length);
            Assert.True(materials.Length > 0);
            Assert.All(materials, j => Assert.False(string.IsNullOrWhiteSpace(j.Name)));
            Assert.All(materials, j => Assert.False(j.MaterialId <= 0));
	    }

        [Fact]
        public async Task GetFirstPageOfMaterials()
        {
            _client.DefaultRequestHeaders.Add(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var response = await _client.GetAsync($"/api/materials?pageNumber=1&pageSize=4");
            _logger.LogDebug("HTTP GET of Materials returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("HTTP GET of Materials returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var materials = JsonConvert.DeserializeObject<MaterialModel[]>(jsonContent);
            Assert.True(4 == materials.Length);
        }
     }
}
