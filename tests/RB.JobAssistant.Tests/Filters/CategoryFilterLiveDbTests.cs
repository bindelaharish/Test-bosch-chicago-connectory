using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RB.JobAssistant.Models;
using Xunit;

namespace RB.JobAssistant.Tests.Filters
{

    public class CategoryFilterLiveDbTests : TestHttpClientApiLiveData
    {
        private readonly HttpClient _client;

        public CategoryFilterLiveDbTests()
        {
            _client = GetClient();
        }

        [Theory]
        [InlineData("categories")]
        [InlineData("categories2")]
        public async Task ReturnsNotFoundForGetId321321(string controllerName)
        {
            var response = await _client.GetAsync($"/api/{controllerName}/321321");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("categories")]
        [InlineData("categories2")]
        public async Task ReturnsNotFoundForPutId489(string controllerName)
        {
            var emptyCategory = new CategoryModel();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(emptyCategory), Encoding.UTF8,
                "application/json");
            var response = await _client.PutAsync($"/api/{controllerName}/489489", jsonContent);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("categories")]
        [InlineData("categories2")]
        public async Task ReturnsNotFoundForDeleteId987(string controllerName)
        {
            var response = await _client.DeleteAsync($"/api/{controllerName}/987");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
