#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace RB.JobAssistant.Data
{
    public class Job
    {
        // TODO: Transition over to Fluent specifications entirely.
        [Key] public int JobId { get; set; }
        // TODO: Spike a Guid implementation of Id.

        public string Name { get; set; }

        public ICollection<Material> Materials { get; set; }

        public ICollection<JobToolRelationship> ToolRelationships { get; set; }

        public ICollection<JobAccessoryRelationship> AccessoryRelationships { get; set; }

        public string DomainId { get; set; }

        public static Expression<Func<Job, bool>> IsValid()
        {
            return j => j.JobId > 0 && !string.IsNullOrWhiteSpace(j.Name);
        }

        public static Expression<Func<Job, bool>> IsMatching(string tenantDomain)
        {
            return j => j.JobId > 0 && !string.IsNullOrWhiteSpace(j.Name) && IsMatchingTenant(j, tenantDomain);
        }

        private static bool IsMatchingTenant(Job j, string tenantDomain)
        {
            return j.DomainId == tenantDomain;
        }

        /*
         Code generation output:

                public Jobs()
                {
                    Accessories = new HashSet<Accessories>();
                    Materials = new HashSet<Materials>();
                    Tools = new HashSet<Tools>();
                }

                public int JobId { get; set; }
                public string Name { get; set; }

                public virtual ICollection<Accessories> Accessories { get; set; }
                public virtual ICollection<Materials> Materials { get; set; }
                public virtual ICollection<Tools> Tools { get; set; }

         Using this command:

            Scaffold-DbContext "Server=MTPNDSD03\SSE2012T;Database=SandboxT_DB;User Guid=sa;Password=I***" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

         */
    }
}