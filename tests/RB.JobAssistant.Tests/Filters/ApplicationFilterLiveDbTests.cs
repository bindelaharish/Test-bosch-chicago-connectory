using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RB.JobAssistant.Models;
using Xunit;

namespace RB.JobAssistant.Tests.Filters
{
    public class ApplicationFilterLiveDbTests : TestHttpClientApiLiveData
    {
        private readonly HttpClient _client;

        public ApplicationFilterLiveDbTests()
        {
            _client = GetClient();
        }

        [Theory]
        [InlineData("applications")]
        [InlineData("applications2")]
        public async Task ReturnsNotFoundForGetId321321(string controllerName)
        {
            var response = await _client.GetAsync($"/api/{controllerName}/321321");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("applications")]
        [InlineData("applications2")]
        public async Task ReturnsNotFoundForPutId489(string controllerName)
        {
            var emptyApplication = new ApplicationModel();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(emptyApplication), Encoding.UTF8,
                "application/json");
            var response = await _client.PutAsync($"/api/{controllerName}/489489", jsonContent);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("applications")]
        [InlineData("applications2")]
        public async Task ReturnsNotFoundForDeleteId987(string controllerName)
        {
            var response = await _client.DeleteAsync($"/api/{controllerName}/987");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}