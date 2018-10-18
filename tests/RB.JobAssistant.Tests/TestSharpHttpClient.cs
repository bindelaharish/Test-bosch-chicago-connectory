using System;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using RestSharp.Portable.HttpClient;

namespace RB.JobAssistant.Tests
{
    public class TestSharpHttpClientApiKestrel
    {
        protected IWebHost _host;

        protected RestClient GetClient()
        {
            int randomPort = RandomNumberHelper.NextIntegerInRange(5120, 8191);
            string httpServerUrl = String.Format("http://*:{0}", randomPort);
            string httpClientUrl = String.Format("http://localhost:{0}", randomPort);

            _host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseUrls(httpServerUrl)
                .Build();

            _host.Start();

            Thread.Sleep(3000);

            return new RestClient(httpClientUrl);
        }
    }
}