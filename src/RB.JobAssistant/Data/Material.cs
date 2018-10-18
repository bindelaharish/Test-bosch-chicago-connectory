#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace RB.JobAssistant.Data
{
    public class Material
    {
        [Key] public int MaterialId { get; set; }

        [Required] public string Name { get; set; }

        public string MaterialImageUrl { get; set; }

        public ICollection<Application> Applications { get; set; }

        public ICollection<Tool> Tools { get; set; }

        public ICollection<Accessory> Accessories { get; set; }

        // Foreign key relationship - key id
        //
        public int? JobId { get; set; }

        //
        // Foreign key relationship - object
        //
        public virtual Job Job { get; set; }

        public string DomainId { get; set; }

        public static Expression<Func<Material, bool>> IsValid()
        {
            return m => m.MaterialId > 0 && !string.IsNullOrWhiteSpace(m.Name);
        }

        public static Expression<Func<Material, bool>> IsMatching(string tenantDomain)
        {
            return m => m.MaterialId > 0 && !string.IsNullOrWhiteSpace(m.Name) && IsMatchingTenant(m, tenantDomain);
        }

        private static bool IsMatchingTenant(Material m, string tenantDomain)
        {
            return m.DomainId == tenantDomain;
        }

        /*
         Scaffolded code:

                public Materials()
                {
                    Applications = new HashSet<Applications>();
                }

                public int MaterialId { get; set; }
                public string Name { get; set; }
                public int? JobId { get; set; }

                public virtual ICollection<Applications> Applications { get; set; }
                public virtual Jobs Job { get; set; }

         Using this scaffold command:

            Scaffold-DbContext "Server=MTPNDSD03\SSE2012T;Database=SandboxT_DB;User Guid=sa;Password=I***" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

        */
    }
}