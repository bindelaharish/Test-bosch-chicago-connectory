#pragma warning disable 1591
using System;
using System.Collections.Generic;

namespace RB.JobAssistant.Data.Samples
{
    public class SampleBoschGreenToolsDataSet
    {
        /**
         * Product data and related applications was scraped from the Bosch DIT -EU site:
         * https://www.bosch-do-it.com/za/en/diy/produktberater-iframe-diy-67855.jsp
         */

        private static readonly Tenant BoschGreenDataTenant =
            new Tenant
            {
                Name = "Bosch DIY Tools Tenant",
                DomainId = "Bosch Green",
                Description = "Bosch power tools for DIY and home end-users",
                Guid = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.Now
            };

        public static int SeedBoschDiyGreenToolsGraphData(JobAssistantContext jobContext)
        {
            #region Tenants defined

            jobContext.Tenants.AddRange(new List<Tenant> {BoschGreenDataTenant});
            jobContext.SaveChanges();

            #endregion

            #region Bosch Green Tools and Accessories defined

            var tool_IXO = new Tool()
            {
                ModelNumber = "IXO",
                Name = "Lithium-ion Cordless Screwdriver",
                Description = string.Empty,
                DomainId = BoschGreenDataTenant.DomainId,
                MaterialNumber = "06039A8000"
            };

            var tool_IXO_Family_Set = new Tool()
            {
                ModelNumber = "IXO Family Set",
                Name = "Lithium-ion Cordless Screwdriver",
                Description = string.Empty,
                DomainId = BoschGreenDataTenant.DomainId,
                MaterialNumber = "06039A800M"
            };

            var tool_PSRSelect = new Tool()
            {
                ModelNumber = "PSR Select",
                Name = "PSR Select Cordless Screwdriver",
                Description = "One cordless screwdriver with nine adapters.",
                DomainId = BoschGreenDataTenant.DomainId,
                MaterialNumber = "0603977000"
            };

            var tool_EasyDrill12 = new Tool()
            {
                ModelNumber = "EasyDrill 12",
                Name = "EasyDrill 12",
                Description = "Compact Lithium-ion Cordless Drill/Driver",
                DomainId = BoschGreenDataTenant.DomainId,
                MaterialNumber = "06039B3000"
            };
            
            var tool_EasyDrill1200 = new Tool()
            {
                ModelNumber =  "EasyDrill 1200",
                Name = "EasyDrill 1200",
                Description = "Lithium-ion Cordless Two-Speed Drill/Driver",
                DomainId = BoschGreenDataTenant.DomainId,
                MaterialNumber = "06039A210B"
            };

            var tool_PSB1800 = new Tool
            {
                ModelNumber = "PSB 1800",
                Name = "Bosch PSB 1800 LI-2 Lithium-ion Cordless Two-speed Combi",
                Description =
                    "Its low weight of 1.3 kilograms and compact dimensions make the PSB 1800 LI-2 from Bosch one of the handiest cordless combi drills of its kind. Thanks to its maximum torque of 38 newton metres, you can even drill and drive screws in hard materials without any problems.",
                DomainId = BoschGreenDataTenant.DomainId
            };

            var tool_EasyImpact12 = new Tool
            {
                ModelNumber = "EasyImpact 12",
                Name = "EasyImpact 12 (1 battery pack)",
                Description =
                    "The EasyImpact 12 cordless 2-speed combi drill – the universal talent in the compact class",
                DomainId = BoschGreenDataTenant.DomainId,
                MaterialNumber = "0603983974"
            };

            var tool_ImpactDrillPSB500RE = new Tool()
            {
                ModelNumber = "PSB 500",
                Name = "PSB 500 RE Hammer Drill [Energy Class A]",
                Description = "The PSB 500 RE is a lightweight impact drill that provides unbeatable user comfort thanks to its powerful 500 watt motor.",
                DomainId = BoschGreenDataTenant.DomainId,
            };

            var tool_ImpactDrillPSB750RE = new Tool()
            {
                ModelNumber = "PSB 750",
                Name = "PSB 750 RE Hammer Drill",
                Description = "The PSB 750 RCE is a compact universal impact drill that makes drilling jobs in concrete, stone, wood and steel, even at large diameters, easy for DIYers.",
                DomainId = BoschGreenDataTenant.DomainId,
            };

            var tool_ImpactDrillPSB850_2RE = new Tool()
            {
                ModelNumber = "PSB 850 RE",
                Name = "PSB 850 RE Hammer Drill",
                Description = "The powerful and sturdy PSB 850-2 RE impact drill is suitable for the most demanding DIYers. This impact drill also comes with exceptionally stable and well insulated aluminium housing. The keyless chuck allows you to change drill bits rapidly and simply.",
                DomainId = BoschGreenDataTenant.DomainId,
            };

            var tool_RotaryHammerPBH2100RE = new Tool()
            {
                ModelNumber = "PSB 2100 RE",
                Name = "PSB 2100 RE Hammer Drill",
                Description = "The PBH 2100 RE rotary hammer from Bosch is a handy and powerful tool for all common hammering, drilling or chiselling jobs. A high impact force is provided by its 550 watts of power and its pneumatic Bosch hammer mechanism with a single impact energy of 1.7 joules.",
                DomainId = BoschGreenDataTenant.DomainId,
            };

            var tool_JigsawPST650 = new Tool()
            {
                ModelNumber = "PST 650",
                Name = "Easy Jigsaw PST 650",
                Description = "The Easy Jigsaw PST 650 from Bosch: Easy Rider – for precise curves and straight lines",
                DomainId = BoschGreenDataTenant.DomainId,
            };
            var tool_Jigsaw_PST700E = new Tool()
            {
                ModelNumber = "PST 700E",
                Name = "Easy Jigsaw PST 700 E",
                Description = "The Easy Jigsaw PST 700 E from Bosch: Easy Rider – for precise curves and straight lines",
                DomainId = BoschGreenDataTenant.DomainId,
            };
            var tool_JigSawPST900PEL = new Tool()
            {
                ModelNumber = "PST 700 PEL",
                Name = "The Expert Jigsaw PST 900 PEL",
                Description = "The Expert Jigsaw PST 900 PEL from Bosch: With maximum precision through all materials",
                DomainId = BoschGreenDataTenant.DomainId,
            };

            var accessory_MiniXLineZ = new Accessory()
            {
                Name = "Mini-X-Line drill bit set, spirit level, hand screwdriver",
                Description = "Metal and masonry drill bit set, spirit level with screwdriver bits and hand screwdriver.",
                DomainId = BoschGreenDataTenant.DomainId,
                MaterialNumber = "2607017200"
            };

            #endregion

            #region Bosch Green Jobs defined

            var screwDrivingJob = new Job
            {
                JobId = 8100,
                Name = "Screw-driving",
                DomainId = BoschGreenDataTenant.DomainId
            };
            var drillingAndChisellingJob = new Job
            {
                JobId = 8101,
                Name = "Drilling and Chi-selling",
                DomainId = BoschGreenDataTenant.DomainId
            };
            var drillingAndChisellingMortisingJob = new Job
            {
                JobId = 8102,
                Name = "Drilling, Chi-selling and Mortising",
                DomainId = BoschGreenDataTenant.DomainId
            };
            var sawingJob = new Job()
            {
                JobId = 8103,
                Name = "Sawing",
                DomainId = BoschGreenDataTenant.DomainId
            };
            var sandingJob = new Job()
            {
                JobId = 8104,
                Name = "Sanding Wood Surfaces",
                DomainId = BoschGreenDataTenant.DomainId
            };
            var gridingAndCuttingJob = new Job()
            {
                JobId = 8105,
                Name = "Grinding and Cutting Metal",
                DomainId = BoschGreenDataTenant.DomainId
            };

            var plantingAndRoutingJob = new Job()
            {
                JobId = 8106,
                Name = "Planting and Routing",
                DomainId = BoschGreenDataTenant.DomainId
            };

            var measuringAndLevelingJob = new Job()
            {
                JobId = 8107,
                Name = "Measuring, Leveling and Detecting",
                DomainId = BoschGreenDataTenant.DomainId
            };

            var paintingJob = new Job()
            {
                JobId = 8108,
                Name = "Working with Paint and Wallpaper",
                DomainId = BoschGreenDataTenant.DomainId
            };

            var craftingJob = new Job()
            {
                JobId = 8109,
                Name = "Crafting, Repairing and Cleaning, Decorating",
                DomainId = BoschGreenDataTenant.DomainId
            };

            var layingTilesJob = new Job()
            {
                JobId = 8110,
                Name = "Laying Tiles",
                DomainId = BoschGreenDataTenant.DomainId
            };

            var layingFloorJob = new Job()
            {
                JobId = 8111,
                Name = "Laying a Floor",
                DomainId = BoschGreenDataTenant.DomainId
            };

            jobContext.Jobs.AddRange(new List<Job>
            {
                screwDrivingJob,
                drillingAndChisellingJob,
                drillingAndChisellingMortisingJob,
                sawingJob,
                sandingJob,
                gridingAndCuttingJob,
                plantingAndRoutingJob,
                measuringAndLevelingJob,
                paintingJob,
                craftingJob,
                layingTilesJob,
                layingFloorJob
            });

            #endregion

            var easyMaterialForScrewDriving = new Material() { Name = "Easy Material", Tools = new List<Tool>(), DomainId = BoschGreenDataTenant.DomainId };
            easyMaterialForScrewDriving.Tools.Add(tool_IXO);
            easyMaterialForScrewDriving.Tools.Add(tool_EasyImpact12);
            easyMaterialForScrewDriving.Tools.Add(tool_ImpactDrillPSB500RE);

            var mediumMaterialForScrewDriving = new Material() { Name = "Medium Material", Tools = new List<Tool>(), DomainId = BoschGreenDataTenant.DomainId };
            mediumMaterialForScrewDriving.Tools.Add(tool_ImpactDrillPSB750RE);

            var hardMaterialForScrewDriving = new Material() {Name = "Hard Material", Tools = new List<Tool>(), DomainId = BoschGreenDataTenant.DomainId };
            hardMaterialForScrewDriving.Tools.Add(tool_ImpactDrillPSB850_2RE);

            screwDrivingJob.Materials = new List<Material> { easyMaterialForScrewDriving, mediumMaterialForScrewDriving, hardMaterialForScrewDriving };

            var easyMaterialDrillingAndChiselling = new Material() {Name = "Easy Material", Tools = new List<Tool>(), DomainId = BoschGreenDataTenant.DomainId };
            easyMaterialDrillingAndChiselling.Tools.Add(tool_ImpactDrillPSB500RE);

            var mediumMaterialDrillingAndChiselling = new Material() { Name = "Medium Material", Tools = new List<Tool>(), DomainId = BoschGreenDataTenant.DomainId };
            mediumMaterialDrillingAndChiselling.Tools.Add(tool_ImpactDrillPSB750RE);
            drillingAndChisellingJob.Materials = new List<Material>
            {
                mediumMaterialDrillingAndChiselling
            };

            var hardMaterialDrillingAndChiselling = new Material() { Name = "Hard Material", Tools = new List<Tool>(), DomainId = BoschGreenDataTenant.DomainId };
            hardMaterialDrillingAndChiselling.Tools.Add(tool_ImpactDrillPSB850_2RE);

            drillingAndChisellingJob.Materials = new List<Material> { easyMaterialDrillingAndChiselling, mediumMaterialDrillingAndChiselling, hardMaterialDrillingAndChiselling };

            var easyMaterialForDrillingChisellingAndMortising = new Material() {Name = "Medium Material", Tools = new List<Tool>(), DomainId = BoschGreenDataTenant.DomainId };
            easyMaterialForDrillingChisellingAndMortising.Tools.Add(tool_RotaryHammerPBH2100RE);

            drillingAndChisellingMortisingJob.Materials = new List<Material>() { easyMaterialForDrillingChisellingAndMortising};

            // Create materials and associate sample tools to material(s).
            //
            var easyMaterialForSawing = new Material { Name = "Easy Material", Tools = new List<Tool> {tool_JigsawPST650, tool_Jigsaw_PST700E}, DomainId = BoschGreenDataTenant.DomainId };
            var hardMaterialForSawing = new Material() {Name = "Hard Material", Tools = new List<Tool> {tool_JigSawPST900PEL}, DomainId = BoschGreenDataTenant.DomainId };

            sawingJob.Materials = new List<Material>() { easyMaterialForSawing, hardMaterialForSawing };

            // Create materials and associate sample tools to material(s).
            //
            sandingJob.Materials = new List<Material>();

            // Create materials and associate sample tools to material(s).
            //
            gridingAndCuttingJob.Materials = new List<Material>();

            // Create materials and associate sample tools to material(s).
            //
            plantingAndRoutingJob.Materials = new List<Material>();

            // Create materials and associate sample tools to material(s).
            //
            measuringAndLevelingJob.Materials = new List<Material>();

            // Create materials and associate sample tools to material(s).
            //
            paintingJob.Materials = new List<Material>();

            // Create materials and associate sample tools to material(s).
            //
            craftingJob.Materials = new List<Material>();

            // Create materials and associate sample tools to material(s).
            //
            layingTilesJob.Materials = new List<Material>();

            // Create materials and associate sample tools to material(s).
            //
            layingFloorJob.Materials = new List<Material>();

            /***
             * Using the ".Materials" collection, ADD associate materials for the job.
             * Then, using the ".Tools" collection, ADD tools and associate materials for other jobs including:
             *
                drillingAndChisellingMortisingJob,
                sawingJob,
                sandingJob,
                gridingAndCuttingJob,
                plantingAndRoutingJob,
                measuringAndLevelingJob,
                paintingJob,
                craftingJob,
                layingTilesJob,
                layingFloorJob
             */

            var cordlessToolsCategory = new Category
            {
                Name = "Bosch - Cordless Tools Cordless Tools",
                DomainId = BoschGreenDataTenant.DomainId
            };
            jobContext.Categories.Add(cordlessToolsCategory);

            var impactDrillsCategory = new Category {Name = "Impact Drills", DomainId = BoschGreenDataTenant.DomainId};
            jobContext.Categories.Add(impactDrillsCategory);

            var rotaryHammersCategory =
                new Category {Name = "Rotary hammers", DomainId = BoschGreenDataTenant.DomainId};
            jobContext.Categories.Add(rotaryHammersCategory);

            var sawsCategory = new Category {Name = "Saws", DomainId = BoschGreenDataTenant.DomainId};
            jobContext.Categories.Add(sawsCategory);

            var benchtopToolsCategory =
                new Category {Name = "Benchtop tools", DomainId = BoschGreenDataTenant.DomainId};
            jobContext.Categories.Add(benchtopToolsCategory);

            cordlessToolsCategory.Tools = new List<Tool> { tool_IXO, tool_IXO_Family_Set, tool_PSRSelect, tool_EasyDrill12, tool_EasyDrill1200, tool_PSB1800 };
            cordlessToolsCategory.Accessories = new List<Accessory> {accessory_MiniXLineZ};

            var savedCount = jobContext.SaveChanges();
            return savedCount;
        }
    }
}