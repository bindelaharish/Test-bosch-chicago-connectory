#pragma warning disable 1591
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RB.JobAssistant.Models
{
    [JsonObject(Id = "Trade")]
    public class TradeModel
    {
        public int TradeId { get; set; }

        [Required] public string Name { get; set; }

        public virtual ICollection<ToolModel> Tools { get; set; }

        public virtual ICollection<CategoryModel> Categories { get; set; }
        public string TenantDomain { get; set; }
    }
}