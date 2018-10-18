using System.Net.Http;
using Microsoft.Extensions.Logging;
using RB.JobAssistant.Controllers;
using RB.JobAssistant.Util;
using RB.JobAssistant.Models;
using Xunit;

namespace RB.JobAssistant.Tests.Api
{
    public class TradeApiIntegrationTests : TestHttpClientApiInMemory
    {
        public TradeApiIntegrationTests()
        {
            _client = GetClient();
            _logger = ApplicationLogging.CreateTypeLogger<JobsController>();
        }

        private readonly HttpClient _client;

        private readonly ILogger<JobsController> _logger;

        [Fact]
        [Trait("Category", "Api")]
        public async void CreatePostReturnsBadRequestForMissingValues()
        {
            var theTrade = new TradeModel {Name = "A test tool"};
            var response = await _client.PostAsJsonAsync("/api/trades/", theTrade);
            _logger.LogDebug(response.ToString());
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            _logger.LogDebug(responseString);
            Assert.Empty(responseString);
        }

        [Fact]
        [Trait("Category", "Api")]
        public async void CreatePostToolAndReturnOk()
        {
            var automotiveTrade = new TradeModel
            {
                Name = "Automotive and Other Vehicle Maintenance"
            };
            var client = GetClient();
            var response = await client.PostAsJsonAsync("/api/trades", automotiveTrade);
            _logger.LogDebug(response.ToString());
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            _logger.LogDebug(responseString);
            Assert.Empty(responseString);
        }
    }
}