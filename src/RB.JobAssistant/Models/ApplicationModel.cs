#pragma warning disable 1591
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RB.JobAssistant.Models
{
    [JsonObject(Title = "Application")]
    public class ApplicationModel
    {
        public int ApplicationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ToolModel> Tools { get; set; }

        public virtual ICollection<AccessoryModel> Accessories { get; set; }

        public string TenantDomain { get; set; }
    }
}