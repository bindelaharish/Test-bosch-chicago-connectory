#pragma warning disable 1591
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RB.JobAssistant.Models
{
    [JsonObject(Id = "Material")]
    public class MaterialModel
    {
        public int MaterialId { get; set; }
        public string Name { get; set; }
        public string MaterialImageUrl { get; set; }

        public virtual ICollection<ApplicationModel> Applications { get; set; }

        public virtual ICollection<ToolModel> Tools { get; set; }

        public virtual ICollection<AccessoryModel> Accessories { get; set; }

        public string TenantDomain { get; set; }
    }
}