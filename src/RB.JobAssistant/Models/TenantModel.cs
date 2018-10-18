#pragma warning disable 1591
using System;
using Newtonsoft.Json;
using RB.JobAssistant.Data;

namespace RB.JobAssistant.Models
{
    [JsonObject(Id = "Tenant")]
    public class TenantModel
    {
        public static string DomainField => Tenant.DomainFieldId;

        public Guid Guid { get; set; }

        public string Domain { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
