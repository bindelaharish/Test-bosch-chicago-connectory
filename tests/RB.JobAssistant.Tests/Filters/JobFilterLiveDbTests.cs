using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RB.JobAssistant.Models;
using Xunit;

namespace RB.JobAssistant.Tests.Filters
{
    public class JobFilterLiveDbTests : TestHttpClientApiLiveData
    {
        private readonly HttpClient _client;

        public JobFilterLiveDbTests()
        {
            _client = GetClient();
        }

        [Theory]
        [InlineData("jobs")]
        [InlineData("jobs2")]
        public async Task ReturnsNotFoundForGetId321321(string controllerName)
        {
            var response = await _client.GetAsync($"/api/{controllerName}/321321");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("jobs")]
        [InlineData("jobs2")]
        public async Task ReturnsNotFoundForPutId246246(string controllerName)
        {
            var emptyJob = new JobModel();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(emptyJob), Encoding.UTF8,
                "application/json");
            var response = await _client.PutAsync($"/api/{controllerName}/246246", jsonContent);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("jobs")]
        [InlineData("jobs2")]
        public async Task ReturnsNotFoundForDeleteId987(string controllerName)
        {
            var response = await _client.DeleteAsync($"/api/{controllerName}/987");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
