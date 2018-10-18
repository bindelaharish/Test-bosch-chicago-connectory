#pragma warning disable 1587
#pragma warning disable 1591
using Newtonsoft.Json;

namespace RB.JobAssistant.Models
{
    [JsonObject(Id = "Accessory")]
    public class AccessoryModel
    {
        public int AccessoryId { get; set; }
        public string Name { get; set; }
        public string ModelNumber { get; set; }
        public string MaterialNumber { get; set; }
        public string TenantDomain { get; set; }
    }

    /**
     * In the future these additional object attributes will be realized:
     * 
        public string Description { get; set; }
        public string Includes { get; set; }
        public decimal SocialRating { get; set; }
        public string Attributes { get; set; } // Simple JSON collection of key-value pairs

     * 
     */
}