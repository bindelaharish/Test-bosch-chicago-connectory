using System;
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
    public class TenantApiRegistrationTests : TestHttpClientApiLiveData
    {
        private readonly HttpClient _client;

        private readonly ILogger<TenantRegistrationController> _logger;

        public TenantApiRegistrationTests()
        {
            _client = GetClient();
            _logger = ApplicationLogging.CreateTypeLogger<TenantRegistrationController>();
        }

        [Fact]
        public async void GetAllTenants()
        {
            _client.DefaultRequestHeaders.Add(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            var response = await _client.GetAsync($"/api/tenants");
            _logger.LogDebug("HTTP GET of Tenants returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("HTTP GET of Tenants returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
            var tenants = JsonConvert.DeserializeObject<TenantModel[]>(jsonContent);
            Assert.NotNull(tenants);
            _logger.LogDebug("HTTP GET of Tenants returned a count of N Tenants: " + tenants.Length);
            Assert.True(tenants.Length > 0);
            Assert.All(tenants, t => Assert.False(string.IsNullOrWhiteSpace(t.Description)));
        }

        [Fact]
        public async void CreateSimpleTenantAndVerify()
        {
            var uniqueId = RandomNumberHelper.NextInteger();
            var domain = $"Orange DIY (yId={uniqueId})";
            var newTenant = new TenantModel
            {
                Name = "Generic Tools Tenant",
                Domain = domain,
                Guid = Guid.NewGuid(),
                Description = "Do-It-Yourself tool data domain (tenant)",
                CreatedAt = DateTimeOffset.Now
            };

            var response = await _client.PostAsJsonAsync("/api/tenants", newTenant);
            _logger.LogDebug(response.ToString());
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            _logger.LogDebug(responseString);
            Assert.Empty(responseString);

            _client.DefaultRequestHeaders.Add(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            response = await _client.GetAsync($"/api/tenants/{domain}");
            _logger.LogDebug("HTTP GET of Tenants returned status code: " + response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var jsonContent = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("HTTP GET of Tenants returned contents: " + jsonContent);
            Assert.False(string.IsNullOrWhiteSpace(jsonContent));
        }
    }
}