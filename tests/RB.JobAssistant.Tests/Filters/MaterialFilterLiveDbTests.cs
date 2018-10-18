using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RB.JobAssistant.Models;
using Xunit;

namespace RB.JobAssistant.Tests.Filters
{
    public class MaterialFilterLiveDbTests : TestHttpClientApiLiveData
    {
        private readonly HttpClient _client;

        public MaterialFilterLiveDbTests()
        {
            _client = GetClient();
        }

        [Theory]
        [InlineData("materials")]
        [InlineData("materials2")]
        public async Task ReturnsNotFoundForGetId456(string controllerName)
        {
            var response = await _client.GetAsync($"/api/{controllerName}/456");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("materials")]
        [InlineData("materials2")]
        public async Task ReturnsNotFoundForPutId579(string controllerName)
        {
            var emptyMaterial = new MaterialModel();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(emptyMaterial), Encoding.UTF8,
                "application/json");
            var response = await _client.PutAsync($"/api/{controllerName}/579", jsonContent);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("materials")]
        [InlineData("materials2")]
        public async Task ReturnsNotFoundForDeleteId864(string controllerName)
        {
            var response = await _client.DeleteAsync($"/api/{controllerName}/864");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}