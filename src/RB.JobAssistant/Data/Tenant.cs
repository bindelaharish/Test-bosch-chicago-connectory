#pragma warning disable 1591
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

/*
 * See example from Nate Barbettini from Citusdata. The Tenant implementation included in this project
 * was based on the example that Nate provided.
 * See blog posting https://www.citusdata.com/blog/2018/01/22/multi-tenant-web-apps-with-dot-net-core-and-postgres for details. 
 */

namespace RB.JobAssistant.Data
{
    public class Tenant
    {
        public const string DomainFieldId = "Domain";
        private const string SingleTenantGuidString = "9b95d0ee-e491-46c5-a0b7-f29fe145d9c5";
        private const string SingleTenantName = "Single Tenant";

        public static readonly string SingleTenantDomain = string.Empty;
        private static readonly Guid SingleTenantGuid = Guid.Parse(SingleTenantGuidString);

        public Guid Guid { get; set; }

        [Key] public string DomainId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public static Tenant CreateSingleTenant()
        {
            return new Tenant
            {
                Name = SingleTenantName,
                DomainId = SingleTenantDomain,
                Guid = SingleTenantGuid,
                CreatedAt = new DateTimeOffset(new DateTime(2010, 1, 1))
            };
        }

        public static Expression<Func<Tenant, bool>> IsValid()
        {
            return t => !string.IsNullOrWhiteSpace(t.DomainId);
        }
    }
}