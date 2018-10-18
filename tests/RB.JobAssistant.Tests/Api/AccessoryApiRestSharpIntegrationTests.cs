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
    public class AccessoryApiRestSharpIntegrationTests : TestSharpHttpClientApiKestrel
    {
        public AccessoryApiRestSharpIntegrationTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<AccessoriesController>();
        }

        private readonly ILogger<AccessoriesController> _logger;

        [Fact]
        public async Task GetAllValidAccessories()
        {
            var client = GetClient();
            var request = RestSharpApiClientHelper.BuildBoschBlueRequest(Method.GET, "api/accessories/");
            var response = await client.Execute(request);
            Assert.NotNull(response);
            _logger.LogDebug("HTTP GET of Accessories returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.Headers.Contains("X-Pagination"));
            Assert.NotNull(response.Content);
            var jsonContent = response.Content;
            _logger.LogDebug("HTTP GET of Accessories returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var accessories = JsonConvert.DeserializeObject<AccessoryModel[]>(jsonContent);
            Assert.NotNull(accessories);
            _logger.LogDebug("HTTP GET of Accessories returned a count of N tools: " + accessories.Length);
            Assert.True(accessories.Length > 0);
            Assert.All(accessories, t => Assert.False(string.IsNullOrWhiteSpace(t.Name)));
            Assert.All(accessories, t => Assert.False(t.AccessoryId <= 0));

            _host.Dispose();
            client.Dispose();
       }

        [Fact]
        [Trait("Category", "Api")]
        public async void GetAccessoryByModelNumberAndThenByAccessoryId()
        {
            const string expectedAccessoryName = "85613M 1/4 In. x 1 In. Carbide Tipped 2-Flute Straight Bit";

            var client = GetClient();
            var accessoryModelRequest = RestSharpApiClientHelper.BuildBoschBlueRequest(Method.GET, "api/accessories/85613M");
            var accessoryModelResponse = await client.Execute<AccessoryModel>(accessoryModelRequest);
            Assert.True(accessoryModelResponse.IsSuccess);
            _logger.LogDebug(accessoryModelResponse.ToString());
            Assert.Equal(expectedAccessoryName, accessoryModelResponse.Data.Name);

            var accessoryIdRequest = RestSharpApiClientHelper.BuildBoschBlueRequest(Method.GET,
                $"api/accessories/{accessoryModelResponse.Data.AccessoryId}");
            accessoryIdRequest.AddParameter("QueryBy", "DatabaseId", ParameterType.HttpHeader);
            var accessoryIdResponse = await client.Execute<ToolModel>(accessoryIdRequest);
            Assert.True(accessoryIdResponse.IsSuccess);
            _logger.LogDebug(accessoryIdResponse.ToString());
            Assert.Equal(expectedAccessoryName, accessoryIdResponse.Data.Name);
        }

        [Fact]
        public async void CreatePostAndVerifyCreated()
        {
            var client = GetClient();
            var request = RestSharpApiClientHelper.BuildBoschBlueRequest(Method.POST, "api/accessories/");
            var anAccessory = new AccessoryModel {Name = "A test accessory"};
            request.AddJsonBody(anAccessory);
            var response = await client.Execute(request);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            Assert.NotNull(response.Content);
            _host.Dispose();
        }

        [Fact]
        [Trait("Category", "Api")]
        public async void CreateUpdateAndVerify()
        {
            var client = GetClient();

            var postRequest = RestSharpApiClientHelper.BuildBoschBlueRequest(Method.POST, "api/accessories/");
            string modelNumber = "ACC" + RandomNumberHelper.NextInteger();
            var theAccessory = new AccessoryModel { Name = "A Power Accessory", ModelNumber = modelNumber, TenantDomain = BoschTenants.BoschBlueDomain };
            postRequest.AddJsonBody(theAccessory);
            var postResponse = await client.Execute(postRequest);
            Assert.NotNull(postResponse);
            Assert.NotNull(postResponse.Content);
            Assert.True(postResponse.StatusCode == HttpStatusCode.Created);

            _logger.LogDebug($"Created test accessory for model id: {modelNumber}");

            var accessoryModelRequest =
                RestSharpApiClientHelper.BuildBoschBlueRequest(Method.GET, $"api/accessories/{modelNumber}");
            accessoryModelRequest.AddParameter("QueryBy", "ModelNumber", ParameterType.HttpHeader);
            var accessoryModelResponse = await client.Execute<AccessoryModel>(accessoryModelRequest);
            Assert.True(accessoryModelResponse.IsSuccess);

            _logger.LogDebug($"Retrieved test accessory with an id of: {accessoryModelResponse.Data.AccessoryId}");

            var updateRequest = RestSharpApiClientHelper.BuildBoschBlueRequest(Method.PUT,
                $"api/accessories/{accessoryModelResponse.Data.AccessoryId}");
            var theUpdatedAccessory = accessoryModelResponse.Data;
            theUpdatedAccessory.Name = "A Power Accessory (UPDATED)";
            updateRequest.AddJsonBody(theUpdatedAccessory);
            var updateResponse = await client.Execute(updateRequest);
            Assert.NotNull(updateResponse);
            Assert.NotNull(updateResponse.Content);
            Assert.True(updateResponse.StatusCode == HttpStatusCode.OK);

            _logger.LogDebug($"Updated test accessory with an id of: {theUpdatedAccessory.AccessoryId}");

            _host.Dispose();
        }
    }
}