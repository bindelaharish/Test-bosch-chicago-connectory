#pragma warning disable 1591
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RB.JobAssistant.Models
{
    [JsonObject(Id = "Job")]
    public class JobModel
    {
        [Required] public int JobId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<MaterialModel> Materials { get; set; }

        public virtual ICollection<ToolModel> Tools { get; set; }

        public virtual ICollection<AccessoryModel> Accessories { get; set; }

        public string TenantDomain { get; set; }
    }
}