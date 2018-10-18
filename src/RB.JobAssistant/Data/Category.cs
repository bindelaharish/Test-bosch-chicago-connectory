#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace RB.JobAssistant.Data
{
    public class Category
    {
        [Key] public int CategoryId { get; set; }

        [Required] public string Name { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

        public virtual ICollection<Material> Materials { get; set; }

        public virtual ICollection<Tool> Tools { get; set; }

        public virtual ICollection<Accessory> Accessories { get; set; }

        public string DomainId { get; set; }

        public static Expression<Func<Category, bool>> IsValid()
        {
            return c => c.CategoryId > 0 && !string.IsNullOrWhiteSpace(c.Name);
        }

        public static Expression<Func<Category, bool>> IsMatching(string tenantDomain)
        {
            return c => c.CategoryId > 0 && !string.IsNullOrWhiteSpace(c.Name) && IsMatchingTenant(c, tenantDomain);
        }

        private static bool IsMatchingTenant(Category c, string tenantDomain)
        {
            return c.DomainId == tenantDomain;
        }
    }
}