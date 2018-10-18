#pragma warning disable 1591 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using RB.JobAssistant.Data;
using SaasKit.Multitenancy;

namespace RB.JobAssistant.MultiTenant
{
    public class CachingTenantResolver : MemoryCacheTenantResolver<Tenant>
    {
        private readonly JobAssistantContext _context;
        private readonly ILogger<CachingTenantResolver> _logger;

        // TODO: Identify the better approach - to pass in the logger factory OR the typed logger 
        public CachingTenantResolver(
            JobAssistantContext context, IMemoryCache cache, ILoggerFactory loggerFactory)
            : base(cache, loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CachingTenantResolver>();
        }

        // Resolver runs on cache misses
        protected override async Task<TenantContext<Tenant>> ResolveAsync(HttpContext context)
        {
            var subdomain = GetSubdomain(context);
            Tenant tenant = await _context.Tenants.FirstOrDefaultAsync(t => string.Equals(t.DomainId, subdomain, StringComparison.CurrentCultureIgnoreCase));
            if (tenant != null) return new TenantContext<Tenant>(tenant);
            tenant = Tenant.CreateSingleTenant();
            _logger.LogDebug("Created default tenant object " + tenant.Name + " (DomainId " + tenant.DomainId + ")");
            return new TenantContext<Tenant>(tenant);
        }

        protected override MemoryCacheEntryOptions CreateCacheEntryOptions()
        {
            return new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(2));
        }

        protected override string GetContextIdentifier(HttpContext context)
        {
            return GetSubdomain(context);
        }

        protected override IEnumerable<string> GetTenantIdentifiers(TenantContext<Tenant> context)
        {
            return new[] {context.Tenant.DomainId};
        }

        private string GetSubdomain(HttpContext context)
        {
            var headerDomainValue = context.Request.Headers[Tenant.DomainFieldId];
            var subdomain = headerDomainValue != StringValues.Empty ? headerDomainValue[0] : context.Request.Host.Host;
            return subdomain;
        }
    }
}