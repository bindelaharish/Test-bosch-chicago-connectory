#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace RB.JobAssistant.Data
{
    public class Trade
    {
        [Key] public int TradeId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Tool> Tools { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public string DomainId { get; set; }

        public static Expression<Func<Trade, bool>> IsValid()
        {
            return t => t.TradeId > 0 && !string.IsNullOrWhiteSpace(t.Name);
        }

        public static Expression<Func<Trade, bool>> IsMatching(string tenantDomain)
        {
            return t => t.TradeId > 0 && !string.IsNullOrWhiteSpace(t.Name) && IsMatchingTenant(t, tenantDomain);
        }

        private static bool IsMatchingTenant(Trade t, string tenantDomain)
        {
            return t.DomainId == tenantDomain;
        }
    }
}