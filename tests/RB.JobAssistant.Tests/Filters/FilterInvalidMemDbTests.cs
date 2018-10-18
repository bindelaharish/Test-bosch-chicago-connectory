using System.Net.Http;
using System.Threading.Tasks;
using RestSharp.Portable;
using Xunit;

namespace RB.JobAssistant.Tests.Filters
{
    public class FilterInvalidMemDbTests : TestSharpHttpClientApiKestrel
    {
        [Theory]
        [InlineData("applications")]
        [InlineData("jobs")]
        [InlineData("materials")]
        public async void GetSingleDataEntityInvalidName(string path)
        {
            var client = GetClient();
            var request = new RestRequest
            {
                Resource = "api/" + path + "/invalid",
                Method = Method.GET
            };
            request.Parameters.Clear();
            request.AddHeader("Accept", "application/json");
            await Assert.ThrowsAsync<HttpRequestException>(() => client.Execute(request));
            client.Dispose();
        }

        [Theory]
        [InlineData("applications")]
        [InlineData("jobs")]
        [InlineData("materials")]
        public async Task GetSingleDataEntityInvalidApplicationName(string path)
        {
            var client = GetClient();
            var request = new RestRequest
            {
                Resource = "api/" + path + "/998877",
                Method = Method.GET
            };
            request.Parameters.Clear();
            request.AddHeader("Accept", "application/json");
            await Assert.ThrowsAsync<HttpRequestException>(() => client.Execute(request));
            client.Dispose();
        }
    }
}
