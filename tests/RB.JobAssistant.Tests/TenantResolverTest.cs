using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter.Utils;
using RB.JobAssistant.Data;
using RB.JobAssistant.MultiTenant;
using RB.JobAssistant.Tests.Data;
using RB.JobAssistant.Util;
using Xunit;

namespace RB.JobAssistant.Tests
{
    public class BoschTenants
    {
        public static string BoschBlueDomain => "Bosch Blue";
    }

    public class TenantResolverTest
    {
        private readonly ILoggerFactory _loggerFactory;

        public TenantResolverTest()
        {
            _loggerFactory = ApplicationLogging.LoggerFactory;
        }

        [Fact]
        public void CacheTenantSetupAndGetProperties()
        {
            var dbId = RandomNumberHelper.NextInteger();
            var helper = new TestContextHelper("test_in-memory_DB-" + dbId);
            var context = new JobAssistantContext(helper.Options);
            IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            var resolver = new CachingTenantResolver(context, cache, _loggerFactory);
            var properties = resolver.GetProps();
            Assert.NotNull(properties);
            Assert.Empty(properties);
        }
    }
}