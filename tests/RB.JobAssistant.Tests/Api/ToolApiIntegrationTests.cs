using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RB.JobAssistant.Controllers;
using RB.JobAssistant.Models;
using RB.JobAssistant.Util;
using Xunit;

namespace RB.JobAssistant.Tests.Api
{
    public class ToolApiIntegrationTests : IClassFixture<TestFixture<Startup>>

    {
        public ToolApiIntegrationTests(TestFixture<Startup> fixture)
        {
            _client = fixture.Client;
            _logger = ApplicationLogging.CreateTypeLogger<ToolsController>();
        }

        private readonly ILogger<ToolsController> _logger;

        private readonly HttpClient _client;

        [Fact]
        [Trait("Category", "Api")]
        public async void CreatePostReturnsBadRequestForMissingValues()
        {
            var aTool = new ToolModel {Name = "A test tool"};
            var response = await _client.PostAsJsonAsync("/api/tools/", aTool);
            _logger.LogDebug(response.ToString());
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            _logger.LogDebug(responseString);
            Assert.Empty(responseString);
        }

        [Fact]
        [Trait("Category", "Api")]
        public async void CreatePostToolAndReturnCreated()
        {
            var levelTool = new ToolModel
            {
                Name = "GLL 3-50 360° Three - Plane Leveling and Alignment - Line Laser",
                ModelNumber = "GLL 3-50"
            };
            var response = await _client.PostAsJsonAsync("/api/tools", levelTool);
            _logger.LogDebug(response.ToString());
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            _logger.LogDebug(responseString);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            Assert.NotNull(response.Content);
        }

        [Fact]
        [Trait("Category", "Api")]
        public async void GetToolByIdAndByModelNumber()
        {
            _client.DefaultRequestHeaders.Clear();
            var response = await _client.GetAsync("/api/tools/HD18-2");
            _logger.LogDebug(response.ToString());
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("queryBy", "DatabaseId");
            var theTool = JsonConvert.DeserializeObject<ToolModel>(responseString);
            response = await _client.GetAsync($"/api/tools/{theTool.ToolId}");
            _logger.LogDebug(response.ToString());
            response.EnsureSuccessStatusCode();
            responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.NotNull(responseString);
        }
    }
}