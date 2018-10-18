#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace RB.JobAssistant.Data
{
    public class Application
    {
        [Key] public int ApplicationId { get; set; }

        [Required] public string Name { get; set; }

        public string Tag { get; set; }

        public ICollection<ApplicationToolRelationship> ToolRelationships { get; set; }

        public ICollection<ApplicationAccessoryRelationship> AccessoryRelationships { get; set; }

        // Foreign key relationship - key id
        //
        public int? MaterialId { get; set; }

        //
        // Foreign key relationship - object
        //
        public virtual Material Material { get; set; }

        public string DomainId { get; set; }

        public static Expression<Func<Application, bool>> IsValid()
        {
            return a => a.ApplicationId > 0 && !string.IsNullOrWhiteSpace(a.Name);
        }

        public static Expression<Func<Application, bool>> IsMatching(string tenantDomain)
        {
            return a => a.ApplicationId > 0 && !string.IsNullOrWhiteSpace(a.Name) && IsMatchingTenant(a, tenantDomain);
        }

        private static bool IsMatchingTenant(Application a, string tenantDomain)
        {
            return a.DomainId == tenantDomain;
        }

        /*
         Scaffolded code:

            public partial class Applications
            {
                public Applications()
                {
                    Accessories = new HashSet<Accessories>();
                    Tools = new HashSet<Tools>();
                }

                public int ApplicationId { get; set; }
                public string Name { get; set; }
                public int? MaterialId { get; set; }

                public virtual ICollection<Accessories> Accessories { get; set; }
                public virtual ICollection<Tools> Tools { get; set; }
                public virtual Materials Material { get; set; }
            }

         Using this scaffold command:

            Scaffold-DbContext "Server=MTPNDSD03\SSE2012T;Database=SandboxT_DB;User Guid=sa;Password=I***" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

        */
    }
}