using System.Net;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RB.JobAssistant.Controllers;
using RB.JobAssistant.Util;
using RB.JobAssistant.Models;
using RestSharp.Portable;
using Xunit;

namespace RB.JobAssistant.Tests.Api
{
    public class MaterialApiRestSharpLiveDbTests : TestSharpHttpClientApiKestrel
    {
        public MaterialApiRestSharpLiveDbTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<MaterialsController>();
        }

        private readonly ILogger<MaterialsController> _logger;

        [Fact]
        [Trait("Category", "Api-Sharp")]
        public async void CreatePostAndVerify1()
        {
            var client = GetClient();
            var request = new RestRequest { Resource = "api/materials/" };
            request.Method = Method.POST;
            request.Parameters.Clear();
            request.AddHeader("Accept", "application/json");
            request.AddHeader(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var aMaterial = new MaterialModel { Name = "A test material", TenantDomain = "Bosch Blue"};
            request.AddParameter("application/json", aMaterial, ParameterType.RequestBody);
            var response = await client.Execute(request);
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.True(response.StatusCode == HttpStatusCode.OK);

            client.Dispose();
        }

        [Fact]
        public async void GetAllValidMaterials()
        {
            var client = GetClient();

            var request = new RestRequest
            {
                Resource = "api/materials/",
                Method = Method.GET
            };
            request.Parameters.Clear();
            request.AddHeader("Accept", "application/json");
            request.AddHeader(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var response = await client.Execute(request);
            Assert.NotNull(response);
            _logger.LogDebug("HTTP GET of Materials returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = response.Content;
            _logger.LogDebug("HTTP GET of Materials returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var materials = JsonConvert.DeserializeObject<MaterialModel[]>(jsonContent);

            client.Dispose();
        }
    }
}
