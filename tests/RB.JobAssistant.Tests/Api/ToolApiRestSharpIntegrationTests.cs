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
    public class ToolApiRestSharpIntegrationTests : TestSharpHttpClientApiKestrel
    {
        public ToolApiRestSharpIntegrationTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<ToolsController>();
        }

        private readonly ILogger<ToolsController> _logger;

        [Fact]
        public async Task GetAllValidTools()
        {
            var client = GetClient();

            var request = new RestRequest
            {
                Resource = "api/tools/",
                Method = Method.GET
            };
            request.Parameters.Clear();
            request.AddHeader("Accept", "application/json");
            request.AddHeader(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var response = await client.Execute(request);
            Assert.NotNull(response);
            _logger.LogDebug("HTTP GET of Tools returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.Headers.Contains("X-Pagination"));
            Assert.NotNull(response.Content);
            var jsonContent = response.Content;
            _logger.LogDebug("HTTP GET of Tools returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var tools = JsonConvert.DeserializeObject<ToolModel[]>(jsonContent);
            Assert.NotNull(tools);
            _logger.LogDebug("HTTP GET of Tools returned a count of N tools: " + tools.Length);
            Assert.True(tools.Length > 0);
            Assert.All(tools, t => Assert.False(string.IsNullOrWhiteSpace(t.Name)));
            Assert.All(tools, t => Assert.False(t.ToolId <= 0));

            _host.Dispose();
            client.Dispose();
        }

        [Fact]
        [Trait("Category", "Api")]
        public async void GetToolByModelNumberAndThenByToolId()
        {
            const string expectedToolName = "IWMH182-01 18 V EC Brushless 1/2 In. Impact Wrench Kit with Ball Detent";

            var client = GetClient();
            var toolModelRequest = new RestRequest
            {
                Method = Method.GET,
                Resource = "api/tools/IWMH182-01"
            };
            toolModelRequest.AddHeader("Accept", "application/json");
            var toolModelResponse = await client.Execute<ToolModel>(toolModelRequest);
            Assert.True(toolModelResponse.IsSuccess);
            _logger.LogDebug(toolModelResponse.ToString());
            Assert.Equal(expectedToolName, toolModelResponse.Data.Name);

            var toolIdRequest = new RestRequest
            {
                Method = Method.GET,
                Resource = $"api/tools/{toolModelResponse.Data.ToolId}"
            };
            toolIdRequest.AddHeader("Accept", "application/json");
            toolIdRequest.AddParameter("QueryBy", "DatabaseId", ParameterType.HttpHeader);
            var toolIdResponse = await client.Execute<ToolModel>(toolIdRequest);
            Assert.True(toolIdResponse.IsSuccess);
            _logger.LogDebug(toolIdResponse.ToString());
            Assert.Equal(expectedToolName, toolIdResponse.Data.Name);
        }

        [Fact]
        [Trait("Category", "Api")]
        public async void CreatePostAndVerifyOk()
        {
            var client = GetClient();
            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "api/tools/";
            request.AddHeader("Accept", "application/json");
            string modelNumber = "TL" + RandomNumberHelper.NextInteger();
            var theTool = new ToolModel {Name = "A Power Tool", ModelNumber = modelNumber};
            request.AddJsonBody(theTool);
            var response = await client.Execute(request);
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.True(response.StatusCode == HttpStatusCode.Created);

            var toolModelRequest = new RestRequest
            {
                Method = Method.GET,
                Resource = $"api/tools/{modelNumber}"
            };
            toolModelRequest.AddHeader("Accept", "application/json");
            toolModelRequest.AddParameter("GetBy", "ModelNumber", ParameterType.HttpHeader);
            var toolIdResponse = await client.Execute<ToolModel>(toolModelRequest);
            Assert.True(toolIdResponse.IsSuccess);
            _host.Dispose();
        }

        [Fact]
        [Trait("Category", "Api")]
        public async void CreateUpdateAndVerify()
        {
            var client = GetClient();

            var postRequest = new RestRequest();
            postRequest.Method = Method.POST;
            postRequest.Resource = "api/tools/";
            postRequest.AddHeader("Accept", "application/json");
            string modelNumber = "TL" + RandomNumberHelper.NextInteger();
            var theTool = new ToolModel {Name = "A Power Tool", ModelNumber = modelNumber};
            postRequest.AddJsonBody(theTool);
            var postResponse = await client.Execute(postRequest);
            Assert.NotNull(postResponse);
            Assert.NotNull(postResponse.Content);
            Assert.True(postResponse.StatusCode == HttpStatusCode.Created);

            _logger.LogDebug($"Created test tool for model id: {modelNumber}");

            var toolModelRequest = new RestRequest
            {
                Method = Method.GET,
                Resource = $"api/tools/{modelNumber}"
            };
            toolModelRequest.AddHeader("Accept", "application/json");
            toolModelRequest.AddParameter("QueryBy", "ModelNumber", ParameterType.HttpHeader);
            var toolModelResponse = await client.Execute<ToolModel>(toolModelRequest);
            Assert.True(toolModelResponse.IsSuccess);

            _logger.LogDebug($"Retrieved test tool with an id of: {toolModelResponse.Data.ToolId}");
            
            var updateRequest = new RestRequest();
            updateRequest.Method = Method.PUT;
            updateRequest.Resource = $"api/tools/{toolModelResponse.Data.ToolId}";
            updateRequest.AddHeader("Accept", "application/json");
            var theUpdatedTool = toolModelResponse.Data;
            theUpdatedTool.Name = "A Power Tool (UPDATED)";
            updateRequest.AddJsonBody(theUpdatedTool);
            var updateResponse = await client.Execute(updateRequest);
            Assert.NotNull(updateResponse);
            Assert.NotNull(updateResponse.Content);
            Assert.True(updateResponse.StatusCode == HttpStatusCode.OK);

            _logger.LogDebug($"Updated test tool with an id of: {theUpdatedTool.ToolId}");

            _host.Dispose();
        }


    }
}