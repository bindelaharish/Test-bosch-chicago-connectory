using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using RB.JobAssistant.Controllers;
using RB.JobAssistant.Util;
using RB.JobAssistant.Models;
using Xunit;

namespace RB.JobAssistant.Tests.Api
{
    public class CategoryApiIntegrationExceptionTests : IClassFixture<TestFixture<Startup>>, IDisposable
    {
        public CategoryApiIntegrationExceptionTests(TestFixture<Startup> fixture)
        {
            _fixture = fixture;
            _client = fixture.Client;
            _logger = ApplicationLogging.CreateTypeLogger<CategoriesController>();
        }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        private readonly TestFixture<Startup> _fixture;

        private readonly ILogger<CategoriesController> _logger;

        private readonly HttpClient _client;
        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _client.Dispose();
                    _fixture.Dispose();
                }

                _disposedValue = true;
            }
        }

        [Fact]
        [Trait("Category", "Api")]
        public async void CreateEmptyCategoryWithApiClient()
        {
            var emptyCategory = new CategoryModel();
            var response = await _client.PostAsJsonAsync("/api/categories/", emptyCategory);
            var ex = Record.Exception(() => response.EnsureSuccessStatusCode());
            Assert.NotNull(ex);
            _logger.LogError(ex.ToString());
            Assert.IsType<HttpRequestException>(ex);
        }
    }
}