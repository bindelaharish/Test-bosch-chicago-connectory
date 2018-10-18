using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace RB.JobAssistant.Tests
{
    public class TestHttpClientApiInMemory
    {
        protected HttpClient GetClient()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<StartupTest>()
                .UseEnvironment("Testing");
            var server = new TestServer(builder);
            var client = server.CreateClient();

            // Client always expects JSON results
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        protected HttpClient GetRestSharpClient()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<StartupTest>()
                .UseEnvironment("Testing");
            var server = new TestServer(builder);
            var client = server.CreateClient();

            // Client always expects JSON results
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }

    public class TestHttpClientApiLiveData
    {
        protected HttpClient GetClient()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseEnvironment("Testing");
            var server = new TestServer(builder);
            var client = server.CreateClient();

            // Client always expects JSON results
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }

    public class TestHttpClientApiKestrel
    {
        protected HttpClient GetClient()
        {
            int randomPort = RandomNumberHelper.NextIntegerInRange(5120, 6144);
            string httpServerUrl = String.Format("http://*:{0}", randomPort);

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseUrls(httpServerUrl)
                .Build();

            host.Start();

            Thread.Sleep(3000);

            var client = new HttpClient {BaseAddress = new Uri(httpServerUrl)};
            client.DefaultRequestHeaders.Accept.Clear();
            // Client always expects JSON results
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // TODO: Switch over to using the RestSharp client
            return client;
        }
    }
}