#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace RB.JobAssistant.Data
{
    public class Accessory
    {
        [Key] public int AccessoryId { get; set; }

        public string Name { get; set; }
        public string ModelNumber { get; set; }
        public string MaterialNumber { get; set; }
        public string Description { get; set; }
        public string Includes { get; set; }
        public decimal SocialRating { get; set; }
        public string Attributes { get; set; } // Simple JSON collection of key-value pairs

        public string DomainId { get; set; }

        public ICollection<ApplicationAccessoryRelationship> Applications { get; set; }

        public Accessory Clone()
        {
            return new Accessory
            {
                Name = Name,
                ModelNumber = ModelNumber,
                MaterialNumber = MaterialNumber,
                Description = Description,
                Includes = Includes,
                Attributes = Attributes
            };
        }

        public static Expression<Func<Accessory, bool>> IsValid()
        {
            return a => a.AccessoryId > 0 && !string.IsNullOrWhiteSpace(a.Name);
        }

        public static Expression<Func<Accessory, bool>> IsMatching(string tenantDomain)
        {
            return a => a.AccessoryId > 0 && !string.IsNullOrWhiteSpace(a.Name) && IsMatchingTenant(a, tenantDomain);
        }

        private static bool IsMatchingTenant(Accessory a, string tenantDomain)
        {
            return a.DomainId == tenantDomain;
        }

        /*

 Scaffolded code:

    public partial class Accessories
    {
        public int AccessoryId { get; set; }
        public string Name { get; set; }
        public int? ApplicationId { get; set; }
        public int? JobId { get; set; }

        public virtual Applications Application { get; set; }
        public virtual Jobs Job { get; set; }
    }

 Using this scaffold command:

    Scaffold-DbContext "Server=MTPNDSD03\SSE2012T;Database=SandboxT_DB;User Guid=sa;Password=I***" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

*/
    }
}