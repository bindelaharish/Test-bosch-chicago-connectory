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
    public class JobApiRestSharpLiveDbTests : TestSharpHttpClientApiKestrel
    {
        public JobApiRestSharpLiveDbTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<JobsController>();
        }

        private readonly ILogger<JobsController> _logger;

        [Fact]
        [Trait("Category", "Api-Sharp")]
        public async void CreatePostAndVerify1()
        {
            var client = GetClient();
            var request = new RestRequest { Resource = "api/jobs/" };
            request.Method = Method.POST;
            request.AddHeader("Accept", "application/json");
            request.AddHeader(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var aJob = new JobModel { Name = "A test job" };
            request.AddParameter("application/json", aJob, ParameterType.RequestBody);
            var response = await client.Execute(request);
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.True(response.StatusCode == HttpStatusCode.OK);

            client.Dispose();
        }

        [Fact]
        [Trait("Category", "Api-Sharp")]
        public async void CreatePostJobAndTenantVerify2()
        {
            var client = GetClient();
            var request = new RestRequest { Resource = "api/jobs/" };
            request.Method = Method.POST;
            request.AddHeader("Accept", "application/json");
            request.AddHeader(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var aJob = new JobModel { Name = "A test job", TenantDomain = "Test DomainId" };
            request.AddParameter("application/json", aJob, ParameterType.RequestBody);
            var response = await client.Execute(request);
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.True(response.StatusCode == HttpStatusCode.OK);

            client.Dispose();
        }

        [Fact]
        [Trait("Category", "Api-Sharp")]
        public async void CreatePostAndVerify2()
        {
            var client = GetClient();
            var request = new RestRequest {Method = Method.POST, Resource = "api/jobs/"};
            request.AddHeader("Accept", "application/json");
            request.AddHeader(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var aJob = new JobModel {Name = "A test job"};
            request.AddJsonBody(aJob);
            var response = await client.Execute(request);
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.True(response.StatusCode == HttpStatusCode.OK);

            client.Dispose();
        }

        [Fact]
        [Trait("Category", "Api-Sharp")]
        public async void CreatePostAndVerify3()
        {
            var client = GetClient();
            var request = new RestRequest {Method = Method.POST, Resource = "api/jobs/"};
            request.AddHeader("Accept", "application/json");
            request.AddHeader(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var aJob = new JobModel {Name = "A test job"};
            request.AddJsonBody(aJob);
            var response = await client.Execute(request);
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.True(response.StatusCode == HttpStatusCode.OK);

            client.Dispose();
        }

        [Fact]
        public async Task GetAllValidJobs()
        {
            var client = GetClient();

            var request = new RestRequest
            {
                Resource = "api/jobs/",
                Method = Method.GET
            };
            request.AddHeader("Accept", "application/json");
            request.AddHeader(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var response = await client.Execute(request);
            Assert.NotNull(response);
            _logger.LogDebug("HTTP GET of Jobs returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.Headers.Contains("X-Pagination"));
            Assert.NotNull(response.Content);
            var jsonContent = response.Content;
            _logger.LogDebug("HTTP GET of Jobs returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var jobs = JsonConvert.DeserializeObject<JobModel[]>(jsonContent);
            Assert.NotNull(jobs);
            _logger.LogDebug("HTTP GET of Jobs returned a count of N jobs: " + jobs.Length);
            Assert.True(jobs.Length > 0);
            Assert.All(jobs, j => Assert.False(string.IsNullOrWhiteSpace(j.Name)));
            Assert.All(jobs, j => Assert.False(j.JobId <= 0));

            client.Dispose();
        }

        [Fact]
        public async Task GetAllValidJobsByPageAndSize()
        {
            var client = GetClient();

            var request = new RestRequest
            {
                Resource = "api/jobs?pageNumber=2&pageSize=4",
                Method = Method.GET
            };
            request.AddHeader("Accept", "application/json");
            request.AddHeader(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var response = await client.Execute(request);
            Assert.NotNull(response);
            _logger.LogDebug("HTTP GET of Jobs returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.Headers.Contains("X-Pagination"));
            Assert.NotNull(response.Content);
            var jsonContent = response.Content;
            _logger.LogDebug("HTTP GET of Jobs returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var jobs = JsonConvert.DeserializeObject<JobModel[]>(jsonContent);
            Assert.NotNull(jobs);
            _logger.LogDebug("HTTP GET of Jobs returned a count of N jobs: " + jobs.Length);
            Assert.True(jobs.Length == 4);
            Assert.All(jobs, j => Assert.False(string.IsNullOrWhiteSpace(j.Name)));
            Assert.All(jobs, j => Assert.False(j.JobId <= 0));

            client.Dispose();
        }

        [Fact]
        public async Task GetAllValidJobsByPage1AndSize8()
        {
            var client = GetClient();

            var request = new RestRequest
            {
                Resource = "api/jobs?pageNumber=2&pageSize=4",
                Method = Method.GET
            };
            request.AddHeader("Accept", "application/json");
            request.AddHeader(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var response = await client.Execute(request);
            Assert.NotNull(response);
            _logger.LogDebug("HTTP GET of Jobs returned status code: " + response.StatusCode);
            Assert.True(response.Headers.Contains("X-Pagination"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = response.Content;
            _logger.LogDebug("HTTP GET of Jobs returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var jobs = JsonConvert.DeserializeObject<JobModel[]>(jsonContent);
            Assert.NotNull(jobs);
            _logger.LogDebug("HTTP GET of Jobs returned a count of N jobs: " + jobs.Length);
            Assert.True(jobs.Length == 4);
            Assert.All(jobs, j => Assert.False(string.IsNullOrWhiteSpace(j.Name)));
            Assert.All(jobs, j => Assert.False(j.JobId <= 0));

            client.Dispose();
        }

        [Fact]
        public async Task GetAllValidJobsByPage100AndSize8()
        {
            var client = GetClient();

            var request = new RestRequest
            {
                Resource = "api/jobs?pageNumber=100&pageSize=8",
                Method = Method.GET
            };
            request.AddHeader("Accept", "application/json");
            request.AddHeader(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var response = await client.Execute(request);
            Assert.NotNull(response);
            _logger.LogDebug("HTTP GET of Jobs returned status code: " + response.StatusCode);
            Assert.True(response.Headers.Contains("X-Pagination"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = response.Content;
            _logger.LogDebug("HTTP GET of Jobs returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var jobs = JsonConvert.DeserializeObject<JobModel[]>(jsonContent);
            Assert.NotNull(jobs);
            _logger.LogDebug("HTTP GET of Jobs returned a count of N jobs: " + jobs.Length);
            Assert.True(jobs.Length == 0);
            Assert.All(jobs, j => Assert.False(string.IsNullOrWhiteSpace(j.Name)));
            Assert.All(jobs, j => Assert.False(j.JobId <= 0));

            client.Dispose();
        }

    }
}