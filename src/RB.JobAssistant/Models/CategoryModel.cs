#pragma warning disable 1591
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RB.JobAssistant.Models
{
    [JsonObject(Id = "Category")]
    public class CategoryModel
    {
        [Required] public int CategoryId { get; set; }

        public string Name { get; set; }

        public int ParentCategoryId { get; set; }

        public CategoryModel ParentCategory { get; set; }

        public virtual ICollection<CategoryModel> Categories { get; set; }

        public virtual ICollection<JobModel> Jobs { get; set; }

        public virtual ICollection<MaterialModel> Materials { get; set; }

        public virtual ICollection<ToolModel> Tools { get; set; }

        public string TenantDomain { get; set; }
    }
}