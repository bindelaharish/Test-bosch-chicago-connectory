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
    public class CategoryFilterRestSharpMemDbTests : TestSharpHttpClientApiKestrel
    {
        public CategoryFilterRestSharpMemDbTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<CategoriesController>();
        }

        private readonly ILogger<CategoriesController> _logger;

        [Fact]
        [Trait("Category", "Api-Sharp")]
        public async void GetBenchtop()
        {
            var client = GetClient();
            var request = RestSharpApiClientHelper.BuildBoschBlueRequest(Method.GET, "api/categories/benchtop");
            request.AddHeader("QueryBy", "CategoryName");
            var response = await client.Execute(request);
            Assert.NotNull(response);
            _logger.LogDebug("HTTP GET of Categories returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = response.Content;
            _logger.LogDebug("HTTP GET of Categories returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var categoryData = JsonConvert.DeserializeObject<CategoryModel>(jsonContent);
            Assert.NotNull(categoryData);

            client.Dispose();
        }
    }
}