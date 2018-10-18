#pragma warning disable 1591
using System;
using System.Collections.Generic;

namespace RB.JobAssistant.Data.Samples
{
    public class SampleDremelToolsDataSet
    {
        private static readonly Tenant DremelDataTenant =
            new Tenant {Name = "Dremel Tools Tenant", DomainId = "Dremel", Description = "Dremel tools for DIY and home end-users", Guid = Guid.NewGuid()};

        public static int SeedDremelGraphData(JobAssistantContext jobContext)
        {
            #region Tenants defined

            // TODO: Tenants has already been initialized!
            jobContext.Tenants.AddRange(new List<Tenant> { DremelDataTenant });
            // TODO: jobContext.Tenants.Add(DremelDataTenant);
            jobContext.SaveChanges();

            #endregion

            var rotaryCategory = new Category {Name = "Rotary", DomainId = DremelDataTenant.DomainId};
            jobContext.Categories.Add(rotaryCategory);

            var sawsCategory = new Category {Name = "Saws", DomainId = DremelDataTenant.DomainId};
            jobContext.Categories.Add(sawsCategory);

            var multiPurposeCategory = new Category {Name = "Multi-Purpose", DomainId = DremelDataTenant.DomainId};
            jobContext.Categories.Add(multiPurposeCategory);

            var hatchProjectCategory = new Category {Name = "Hatch Project Kits", DomainId = DremelDataTenant.DomainId};
            jobContext.Categories.Add(hatchProjectCategory);

            var printer3DCategory = new Category {Name = "3D Printer", DomainId = DremelDataTenant.DomainId};
            jobContext.Categories.Add(printer3DCategory);


            var sandingJob = new Job {JobId = 95001, Name = "Sanding", DomainId = DremelDataTenant.DomainId};
            jobContext.Jobs.Add(sandingJob);

            var cuttingScrapingJob = new Job { JobId = 95002, Name = "Cutting & Scraping", DomainId = DremelDataTenant.DomainId};
            jobContext.Jobs.Add(cuttingScrapingJob);

            var etchingEngravingJob = new Job { JobId = 95003, Name = "Etching & Engraving", DomainId = DremelDataTenant.DomainId};
            jobContext.Jobs.Add(etchingEngravingJob);

            var routingDrillingJob = new Job { JobId = 95004, Name = "Routing & Drilling", DomainId = DremelDataTenant.DomainId};
            jobContext.Jobs.Add(routingDrillingJob);

            var grindingSharpeningJob = new Job { JobId = 95005, Name = "Grinding & Sharpening", DomainId = DremelDataTenant.DomainId};
            jobContext.Jobs.Add(grindingSharpeningJob);

            var cleaningPolishingJob = new Job { JobId = 95006, Name = "Cleaning & Polishing", DomainId = DremelDataTenant.DomainId};
            jobContext.Jobs.Add(cleaningPolishingJob);

            var accessoryKitCategory =
                new Category {Name = "Accessory Kits & Sets", DomainId = DremelDataTenant.DomainId};
            jobContext.Categories.Add(accessoryKitCategory);

            var attachmentPartKitCategory =
                new Category {Name = "Attachments & Parts", DomainId = DremelDataTenant.DomainId};
            jobContext.Categories.Add(attachmentPartKitCategory);

            var butaneTorchCategory = new Category {Name = "Butane Torch", DomainId = DremelDataTenant.DomainId};
            jobContext.Categories.Add(butaneTorchCategory);

            var accessories3dCategory = new Category {Name = "3D Accessories", DomainId = DremelDataTenant.DomainId};
            jobContext.Categories.Add(accessories3dCategory);

            #region Dremel Tools and Accessories defined

            var toolUS40_01 = new Tool
            {
                ToolId = 92101,
                Name = "Dremel US40 Ultra-Saw",
                ModelNumber = "US40-01",
                MaterialNumber = string.Empty,
                DomainId = DremelDataTenant.DomainId
            };
            jobContext.Tools.Add(toolUS40_01);

            var toolUS40_03 = new Tool
            {
                ToolId = 92102,
                Name = "Dremel US40 Ultra-Saw",
                ModelNumber = "US40-03",
                MaterialNumber = string.Empty,
                DomainId = DremelDataTenant.DomainId
            };
            jobContext.Tools.Add(toolUS40_03);

            var accessoryUS700 = new Accessory() { Name = "Dremel US700 Ultra-Saw 6-Piece Cutting Wheel Kit", ModelNumber = "US700", MaterialNumber = "B00JGB04RI ", DomainId = DremelDataTenant.DomainId };
            jobContext.Accessories.Add(accessoryUS700);

            sawsCategory.Tools = new List<Tool> {toolUS40_01, toolUS40_03};
            sawsCategory.Accessories = new List<Accessory>() { accessoryUS700 };

            var savedCount = jobContext.SaveChanges();
            return savedCount;

            #endregion
        }
    }
}