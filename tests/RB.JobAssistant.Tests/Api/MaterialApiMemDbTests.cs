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
    public class MaterialApiMemDbTests : TestHttpClientApiInMemory
    {
        public MaterialApiMemDbTests()
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
            _logger.LogDebug("HTTP GET of Materials returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("HTTP GET of Materials returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var materials = JsonConvert.DeserializeObject<MaterialModel[]>(jsonContent);
            Assert.NotNull(materials);
            _logger.LogDebug("HTTP GET of Materials returned a count of N materials: " + materials.Length);
            Assert.True(materials.Length > 0);
            Assert.All(materials, m => Assert.False(string.IsNullOrWhiteSpace(m.Name)));
            Assert.All(materials, m => Assert.False(m.MaterialId <= 0));
        }

        [Fact]
        public async Task GetSingleMaterialInvalidMaterial()
        {
            _client.DefaultRequestHeaders.Add(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var response = await _client.GetAsync($"/api/materials/998877");
            _logger.LogDebug("HTTP GET of Jobs returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetSingleMaterialInvalidMaterialId()
        {
            var invalidMaterialId = 111222333;
            _client.DefaultRequestHeaders.Add(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var response = await _client.GetAsync($"/api/materials/{invalidMaterialId}");
            _logger.LogDebug("HTTP GET of Material returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetSingleValidMaterial()
        {
            _client.DefaultRequestHeaders.Add(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            _client.DefaultRequestHeaders.Add("QueryBy", "DatabaseId");
            var response = await _client.GetAsync($"/api/materials/114");
            _logger.LogDebug("HTTP GET of Material returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("HTTP GET of Material returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var plywoodMaterial = JsonConvert.DeserializeObject<MaterialModel>(jsonContent);
            Assert.NotNull(plywoodMaterial);
            Assert.Equal("Plywood", plywoodMaterial.Name);
        }
    }
}