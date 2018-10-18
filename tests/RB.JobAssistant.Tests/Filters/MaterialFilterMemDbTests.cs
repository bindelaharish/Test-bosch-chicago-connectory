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
    public class MaterialFilterRestSharpMemDbTests : TestSharpHttpClientApiKestrel
    {
        public MaterialFilterRestSharpMemDbTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<MaterialsController>();
        }

        private readonly ILogger<MaterialsController> _logger;

        [Fact]
        [Trait("Category", "Api-Sharp")]
        public async void GetConcreteMaterial()
        {
            var client = GetClient();
            var request = RestSharpApiClientHelper.BuildBoschBlueRequest(Method.GET, "api/materials/concrete");
            request.AddHeader("QueryBy", "MaterialName");
            var response = await client.Execute(request);
            Assert.NotNull(response);
            _logger.LogDebug("HTTP GET of Materials returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = response.Content;
            _logger.LogDebug("HTTP GET of Materials returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var materialData = JsonConvert.DeserializeObject<MaterialModel>(jsonContent);
            Assert.NotNull(materialData);

            client.Dispose();
        }
    }
}