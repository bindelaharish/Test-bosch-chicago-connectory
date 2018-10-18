using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RB.JobAssistant.Controllers;
using RB.JobAssistant.Models;
using RB.JobAssistant.Util;
using RestSharp.Portable;
using Xunit;

namespace RB.JobAssistant.Tests.Api
{
    public class ApplicationApiRestSharpIntegrationTests : TestSharpHttpClientApiKestrel
    {
        public ApplicationApiRestSharpIntegrationTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<ApplicationsController>();
        }

        private readonly ILogger<ApplicationsController> _logger;

        [Fact]
        public async Task GetAllValidApplications()
        {
            var client = GetClient();
            var request = RestSharpApiClientHelper.BuildBoschBlueRequest(Method.GET, "api/applications/");
            var response = await client.Execute(request);
            Assert.NotNull(response);
            _logger.LogDebug("HTTP GET of Applications returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = response.Content;
            _logger.LogDebug("HTTP GET of Applications returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var applications = JsonConvert.DeserializeObject<ApplicationModel[]>(jsonContent);
            Assert.NotNull(applications);
            _logger.LogDebug("HTTP GET of applications returned a count of N tools: " + applications.Length);
            Assert.True(applications.Length > 0);
            Assert.All(applications, t => Assert.False(string.IsNullOrWhiteSpace(t.Name)));
            Assert.All(applications, t => Assert.False(t.ApplicationId <= 0));

            _host.Dispose();
            client.Dispose();
        }


        [Fact]
        [Trait("Category", "Api")]
        public async void GetApplicationByNameAndThenByApplicationId()
        {
            const string expectedApplicationName = "Medium Torque Drive & Fasten";

            var client = GetClient();
            var applicationModelRequest = RestSharpApiClientHelper.BuildBoschBlueRequest(Method.GET, "api/applications/Medium Torque Drive & Fasten");
            var applicationModelResponse = await client.Execute<ApplicationModel>(applicationModelRequest);
            Assert.True(applicationModelResponse.IsSuccess);
            _logger.LogDebug(applicationModelResponse.ToString());
            Assert.Equal(expectedApplicationName, applicationModelResponse.Data.Name);

            var applicationIdRequest = RestSharpApiClientHelper.BuildBoschBlueRequest(Method.GET,
                $"api/applications/{applicationModelResponse.Data.ApplicationId}");
            applicationIdRequest.AddParameter("QueryBy", "DatabaseId", ParameterType.HttpHeader);
            var applicationIdResponse = await client.Execute<ToolModel>(applicationIdRequest);
            Assert.True(applicationIdResponse.IsSuccess);
            _logger.LogDebug(applicationIdResponse.ToString());
            Assert.Equal(expectedApplicationName, applicationIdResponse.Data.Name);

            // TODO; assert tag name on Drill & Drive Job with Chip Board Material
        }
    }
}
