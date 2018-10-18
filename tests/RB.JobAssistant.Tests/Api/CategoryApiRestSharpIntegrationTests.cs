using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RB.JobAssistant.Controllers;
using RB.JobAssistant.Util;
using RB.JobAssistant.Models;
using RestSharp.Portable;
using Xunit;

namespace RB.JobAssistant.Tests.Api
{
    public class CategoryApiRestSharpIntegrationTests : TestSharpHttpClientApiKestrel
    {
        public CategoryApiRestSharpIntegrationTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<CategoriesController>();
        }

        private readonly ILogger<CategoriesController> _logger;

        [Fact]
        public async void CreatePostAndVerify()
        {
            var client = GetClient();
            var request = new RestRequest {Resource = "api/categories/", Method = Method.POST};
            request.AddHeader("Accept", "application/json");
            var aCategory = new CategoryModel {Name = "A test category"};
            request.AddJsonBody(aCategory);
            var response = await client.Execute(request);
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            _host.Dispose();
        }

        [Fact]
        public async void CreatePostAndVerify2()
        {
            var client = GetClient();
            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "api/categories/";
            request.AddHeader("Accept", "application/json");
            var aCategory = new CategoryModel {Name = "A test category"};
            request.AddJsonBody(aCategory);
            var response = await client.Execute(request);
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            _host.Dispose();
        }

        [Fact(Skip="Implement lookup by category name")]
        [Trait("Category", "Api")]
        public async void CreateUpdateAndVerify()
        {
            var client = GetClient();

            var postRequest = new RestRequest();
            postRequest.Method = Method.POST;
            postRequest.Resource = "api/categories/";
            postRequest.AddHeader("Accept", "application/json");
            string categoryName = "CAT" + RandomNumberHelper.NextInteger();
            var theCategory = new CategoryModel { Name = categoryName };
            postRequest.AddJsonBody(theCategory);
            var postResponse = await client.Execute(postRequest);
            Assert.NotNull(postResponse);
            Assert.NotNull(postResponse.Content);
            Assert.True(postResponse.StatusCode == HttpStatusCode.OK);

            _logger.LogDebug($"Created test category with name: {categoryName}");

            var categoryModelRequest = new RestRequest
            {
                Method = Method.GET,
                Resource = $"api/categories/{categoryName}"
            };
            categoryModelRequest.AddHeader("Accept", "application/json");
            var categoryModelResponse = await client.Execute<CategoryModel>(categoryModelRequest);
            Assert.True(categoryModelResponse.IsSuccess);

            _logger.LogDebug($"Retrieved test category with an id of: {categoryModelResponse.Data.CategoryId}");

            var updateRequest = new RestRequest();
            updateRequest.Method = Method.PUT;
            updateRequest.Resource = $"api/categories/{categoryModelResponse.Data.CategoryId}";
            updateRequest.Parameters.Clear();
            updateRequest.AddHeader(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            updateRequest.AddHeader("Accept", "application/json");
            var theUpdatedCategory = categoryModelResponse.Data;
            theUpdatedCategory.Name = "UPDATED_CAT" + RandomNumberHelper.NextInteger();
            updateRequest.AddJsonBody(theUpdatedCategory);
            var updateResponse = await client.Execute(updateRequest);
            Assert.NotNull(updateResponse);
            Assert.NotNull(updateResponse.Content);
            Assert.True(updateResponse.StatusCode == HttpStatusCode.OK);

            _logger.LogDebug($"Updated test category with an id of: {theUpdatedCategory.CategoryId}");

            _host.Dispose();
        }

        [Fact]
        public async Task GetAllValidCategorys()
        {
            var client = GetClient();
            var request = new RestRequest
            {
                Resource = "api/categories/",
                Method = Method.GET
            };
            request.Parameters.Clear();
            request.AddHeader("Accept", "application/json");
            var response = await client.Execute(request);
            Assert.NotNull(response);
            _logger.LogDebug("HTTP GET of Categorys returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = response.Content;
            _logger.LogDebug("HTTP GET of Categorys returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var categories = JsonConvert.DeserializeObject<CategoryModel[]>(jsonContent);
            Assert.NotNull(categories);
            _logger.LogDebug("HTTP GET of Categorys returned a count of N jobs: " + categories.Length);
            Assert.True(categories.Length > 0);
            Assert.All(categories, c => Assert.False(c.Name == null));
            Assert.All(categories, c => Assert.False(c.CategoryId <= 0));
            _host.Dispose();
        }
    }
}