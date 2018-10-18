#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;

namespace RB.JobAssistant.Data.Samples
{
    public static class SampleBoschToolsDataSet
    {
        /**
         * 
         * Expand the below data setup to support product-specific data sets.
         * 
		 *  Jobs -> Materials -> Applications -> Tools
		 *  Jobs -> Materials -> Applications -> Accessories
		 *  Jobs -> Materials -> Tools
		 * 
         *  Trade -> Category -> Category -> Products (a.k.a. Tools)
         *  Materials -> Material -> Tools	
         *  Trade -> Tools
         *  
         *  For each collection, try to 3, 5, or 8 items (to include)
         *  
         *  At some point, a top-level (containing) "brand" will be needed!
         */

        private static readonly Tenant BoschToolsDataTenant = new Tenant
        {
            Name = "Bosch Tools Tenant",
            DomainId = "Bosch Blue",
            Guid = Guid.NewGuid(),
            Description = "Bosch professional (Blue) tools data domain (tenant)",
            CreatedAt = DateTimeOffset.Now
        };

        public static int SeedBoschToolsSubsetData(JobAssistantContext jobContext)
        {
            var hammerAndHammerDrillJob = new Job {JobId = 1, Name = "Hammer & Hammer Drill"};

            var drillAndDriveJob = new Job {JobId = 5, Name = "Drill & Drive"};

            var toolHD18_2 = new Tool
            {
                ToolId = 100001,
                Name = "HD18-2 (06011A2111) 1/2 In. Two-Speed Hammer Drill",
                ModelNumber = "HD18-2",
                MaterialNumber = "06011A2111"
            };
            var toolHDH181X_01L = new Tool
            {
                ToolId = 100002,
                Name =
                    "HDH181X-01L (06019D9311) 18 V Brute Tough™ 1/2 In. Hammer Drill/Driver with L-Boxx® Carrying Case",
                MaterialNumber = "06019D9311"
            };
            var toolHDS182_02L = new Tool
            {
                ToolId = 100003,
                Name = " (06019D7110) 18 V EC Brushless Compact Tough™ 1/2 In. Hammer Drill/Driver",
                MaterialNumber = "06019D7110"
            };
            var toolPS130_2A = new Tool {ToolId = 100004, Name = "PS130-2A (06019B6913) 12 V Max Hammer Drill Driver"};
            var toolIDH182_01 = new Tool
            {
                ToolId = 100005,
                Name = "IDH182-01 18 V EC Brushless 1/4 In. and 1/2 In. Socket-Ready Impact Driver",
                ModelNumber = "IDH182-01"
            };
            var toolIWMH182_01 = new Tool
            {
                ToolId = 100006,
                Name = "IWMH1082-01 18 V EC Brushless 1/2 In. Impact Wrench Kit with Ball Detent",
                ModelNumber = "IWMH182-01"
            };
            var toolPS41_2A = new Tool
            {
                ToolId = 100007,
                Name = "PS41-2A 12 V Max Impact Driver Kit",
                ModelNumber = "PS41-2A"
            };
            var tool24618_01 = new Tool
            {
                ToolId = 100008,
                Name = "24618-01 1/2 In. 18 V Impact Wrench",
                ModelNumber = "24618-01"
            };
            var accessoryDSB1013 = new Accessory
            {
                AccessoryId = 200001,
                Name = "DSB1013 1 In. x 6 In. Daredevil™ Standard Spade Bits",
                ModelNumber = "DSB1013"
            };

            var accessoryDSB5010 = new Accessory
            {
                AccessoryId = 200002,
                Name = "DSB5010 10 pc. Daredevil™ Standard Spade Bit Set",
                ModelNumber = "DSB5010"
            };

            var accessorySBID32 = new Accessory
            {
                AccessoryId = 200003,
                Name = "SBID32 32 pc. Impact Tough™ Screwdriving Bit Set",
                ModelNumber = "SBID32"
            };

            var hammerAndHammerDrill_concrete_hammerDrillApp = new Application
            {
                Name = "Hammer Drill",
                Tag = "Hammer Drill Job with Concrete Material"
            };
            var drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp = new Application
            {
                Name = "Medium Torque Drive & Fasten",
                Tag = "Drill & Drive Job with Chip Board Material"
            };

            var hammerAndHammerDrill_concreteMaterial = new Material
            {
                MaterialId = 108,
                Name = "Concrete"
            };
            hammerAndHammerDrillJob.Materials = new List<Material>
            {
                hammerAndHammerDrill_concreteMaterial
            };
            var drillAndDriveJob_chipboardOsbMaterial = new Material
            {
                MaterialId = 124,
                Name = "Chipboard / OSB"
            };
            drillAndDriveJob.Materials = new List<Material>
            {
                drillAndDriveJob_chipboardOsbMaterial
            };

            hammerAndHammerDrill_concreteMaterial.Applications = new List<Application>
            {
                hammerAndHammerDrill_concrete_hammerDrillApp
            };
            drillAndDriveJob_chipboardOsbMaterial.Applications = new List<Application>
            {
                drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp
            };

            var hammerAndHammerDrill_concrete_hammerDrillApp_Tools = new List<ApplicationToolRelationship>();
            hammerAndHammerDrill_concrete_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_concrete_hammerDrillApp.ApplicationId,
                ToolId = toolHD18_2.ToolId
            });
            hammerAndHammerDrill_concrete_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_concrete_hammerDrillApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            hammerAndHammerDrill_concrete_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_concrete_hammerDrillApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            hammerAndHammerDrill_concrete_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_concrete_hammerDrillApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            hammerAndHammerDrill_concrete_hammerDrillApp.ToolRelationships =
                hammerAndHammerDrill_concrete_hammerDrillApp_Tools;

            var drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = tool24618_01.ToolId
            });
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolIDH182_01.ToolId
            });
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolIWMH182_01.ToolId
            });
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolPS41_2A.ToolId
            });
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.ToolRelationships =
                drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Tools;

            var drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.ApplicationId,
                AccessoryId = accessoryDSB1013.AccessoryId
            });
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.ApplicationId,
                AccessoryId = accessoryDSB5010.AccessoryId
            });
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.ApplicationId,
                AccessoryId = accessorySBID32.AccessoryId
            });
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.AccessoryRelationships =
                drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Accessories;

            jobContext.Jobs.Add(hammerAndHammerDrillJob);
            jobContext.Jobs.Add(hammerAndHammerDrillJob);
            jobContext.Materials.Add(hammerAndHammerDrill_concreteMaterial);
            jobContext.Materials.Add(drillAndDriveJob_chipboardOsbMaterial);
            jobContext.Applications.Add(hammerAndHammerDrill_concrete_hammerDrillApp);
            jobContext.Applications.Add(drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp);
            jobContext.Tools.Add(toolHD18_2);
            jobContext.Tools.Add(toolHDH181X_01L);
            jobContext.Tools.Add(toolHDS182_02L);
            jobContext.Tools.Add(toolPS130_2A);
            jobContext.Tools.Add(toolIDH182_01);
            jobContext.Tools.Add(toolIDH182_01);
            jobContext.Tools.Add(toolPS41_2A);
            jobContext.Tools.Add(tool24618_01);
            jobContext.Accessories.Add(accessoryDSB1013);
            jobContext.Accessories.Add(accessoryDSB5010);
            jobContext.Accessories.Add(accessorySBID32);

            var savedCount = jobContext.SaveChanges();
            return savedCount;
        }

        public static int SeedBoschToolsGraphData(JobAssistantContext jobContext)
        {
            #region Tenants defined

            jobContext.Tenants.AddRange(new List<Tenant> {BoschToolsDataTenant});
            jobContext.SaveChanges();

            #endregion

            #region BoschTools Tools and Accessories defined

            var toolHD18_2 = new Tool
            {
                ToolId = 90101,
                Name = "HD18-2 (06011A2111) 1/2 In. Two-Speed Hammer Drill",
                ModelNumber = "HD18-2",
                MaterialNumber = "06011A2111",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolHD18_2);

            var toolHDH181X_01L = new Tool
            {
                ToolId = 90102,
                Name =
                    "HDH181X-01L (06019D9311) 18 V Brute Tough™ 1/2 In. Hammer Drill/Driver with L-Boxx® Carrying Case",
                ModelNumber = "HDH181X-01L",
                MaterialNumber = "06019D9311",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolHDH181X_01L);

            var toolHDS182_02L = new Tool
            {
                ToolId = 90103,
                Name = " (06019D7110) 18 V EC Brushless Compact Tough™ 1/2 In. Hammer Drill/Driver",
                ModelNumber = "HDS182-02L",
                MaterialNumber = "06019D7110",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolHDS182_02L);

            var toolPS130_2A = new Tool
            {
                ToolId = 90104,
                Name = "PS130-2A (06019B6913) 12 V Max Hammer Drill Driver",
                ModelNumber = "PS130-2A",
                MaterialNumber = "06019B6913",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolPS130_2A);

            var toolGSS20_40 = new Tool
            {
                ToolId = 90105,
                Name = "GSS20-40 (06012A2110) 1/4-Sheet Orbital Finishing Sander",
                ModelNumber = "GSS20-40",
                MaterialNumber = "06012A2110",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolGSS20_40);

            var tool_OS50VC = new Tool
            {
                ToolId = 90106,
                Name =
                    "OS50VC (0601292912) Half-Sheet Orbital Finishing Sander with Vibration Control and SheetLoc™ Supreme",
                ModelNumber = "OS50VC",
                MaterialNumber = "0601292912",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(tool_OS50VC);

            var toolPL1632 = new Tool
            {
                ToolId = 90107,
                Name = "PL1632 (06015A4010) 3-1/4 In. Planer",
                ModelNumber = "PL1632",
                MaterialNumber = "06015A4010",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolPL1632);

            var toolPL2632K = new Tool
            {
                ToolId = 90108,
                Name = "PL2632K (06015A4310) 3-1/4 In. Planer Kit",
                ModelNumber = "PL2632K",
                MaterialNumber = "06015A4310",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolPL2632K);

            var toolPLH181B = new Tool
            {
                ToolId = 90109,
                Name = "PLH181B (06015A0314) 18 V 3-1/4 In. Planer - Tool Only",
                ModelNumber = "PLH181B",
                MaterialNumber = "06015A0314",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolPLH181B);

            var toolCCS180BL = new Tool
            {
                ToolId = 90110,
                Name = "CCS180BL (060166H013) 18 V 6-1/2 In. Circular Saw with L-Boxx® Carrying Case",
                ModelNumber = "CCS180BL",
                MaterialNumber = "060166H013",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolCCS180BL);

            var toolCS10 = new Tool
            {
                ToolId = 90111,
                Name = "CS10 (0601672072) 7-1/4 In. 15 A Circular Saw",
                ModelNumber = "CS10",
                MaterialNumber = "0601672072",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolCS10);

            var toolCS5 = new Tool
            {
                ToolId = 90112,
                Name = "CS5 (0601672074) 7-1/4 In. 15 A Left Blade Circular Saw",
                ModelNumber = "CS5",
                MaterialNumber = "0601672074",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolCS5);

            var toolCSW41 = new Tool
            {
                ToolId = 90113,
                Name = "CSW41 (060166D010) 7-1/4 In. Worm Drive Saw",
                ModelNumber = "CSW41",
                MaterialNumber = "060166D010",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolCSW41);

            var toolJS470E = new Tool
            {
                ToolId = 90114,
                Name = "JS470E (0601513012) Top-Handle Jig Saw",
                ModelNumber = "JS470E",
                MaterialNumber = "0601513012",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolJS470E);

            var toolJS572EBL15 = new Tool
            {
                ToolId = 90115,
                Name = "JS572EBL (0601514010) 7.2A Barrel-Grip Jig Saw Kit",
                ModelNumber = "JS572EBL",
                MaterialNumber = "0601514010",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolJS572EBL15);

            var toolJS572EL = new Tool
            {
                ToolId = 90116,
                Name = "JS572EL (0601515010) Top-Handle Jig Saw",
                ModelNumber = "JS572EL",
                MaterialNumber = "0601515010"
            };
            jobContext.Tools.Add(toolJS572EL);

            var toolJSH180BL = new Tool
            {
                ToolId = 90117,
                Name = "JSH180BL (060158J31A) 18 V Lithium-Ion Cordless Jig Saw with L-Boxx-2™",
                ModelNumber = "JSH180BL",
                MaterialNumber = "060158J31A",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolJSH180BL);

            var tool1617EVSPK = new Tool
            {
                ToolId = 90118,
                Name = "1617EVSPK (0601617577) 2.25 HP Combination Plunge- and Fixed-Base Router",
                ModelNumber = "1617EVSPK",
                MaterialNumber = "0601617577",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(tool1617EVSPK);

            var toolMRC23EVSK45 = new Tool
            {
                ToolId = 90119,
                Name = "MRC23EVSK (0601624012) 2.3 HP Electronic Modular Router System",
                ModelNumber = "MRC23EVSK",
                MaterialNumber = "0601624012",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolMRC23EVSK45);

            var toolPR20EVSPK = new Tool
            {
                ToolId = 90120,
                Name = "PR20EVSPK (060160A717) 1 HP Colt™ Variable Speed Electronic Palm Router Combination Kit",
                ModelNumber = "PR20EVSPK",
                MaterialNumber = "060160A717",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolPR20EVSPK);

            var tool4100_09 = new Tool
            {
                ToolId = 90121,
                Name = "4100 - 09 10 In. Worksite Table Saw with Gravity-Rise™ Wheeled Stand",
                ModelNumber = "4100",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(tool4100_09);

            var toolGTS1031 = new Tool
            {
                ToolId = 90122,
                Name = "GTS1031 10 In. Portable Jobsite Table Saw",
                ModelNumber = "GTS1031"
            };
            jobContext.Tools.Add(toolGTS1031);

            var toolTS2100 = new Tool
            {
                ToolId = 90123,
                Name = "TS2100 Gravity-Rise™ Table Saw Stand | Bosch Power Tools",
                ModelNumber = "TS2100",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolTS2100);

            var tool5313 = new Tool
            {
                ToolId = 90124,
                Name = "5313",
                ModelNumber = "5313",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(tool5313);

            var toolCM12SD = new Tool
            {
                ToolId = 90125,
                Name = "CM12SD 12 In.Dual - Bevel Slide Miter Saw",
                ModelNumber = "CM12SD",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolCM12SD);

            var toolGCM12SD = new Tool
            {
                ToolId = 90126,
                Name = "GCM12SD 12 In. Dual-Bevel Glide Miter Saw",
                ModelNumber = "GCM12SD",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolGCM12SD);

            var toolT4B = new Tool
            {
                ToolId = 90127,
                Name = "T4B Gravity-Rise Wheeled Miter Saw Stand",
                ModelNumber = "T4B",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolT4B);

            var toolHTH181_01 = new Tool
            {
                ToolId = 90128,
                Name = "HTH181-01 (06019B1J14) 18 V High Torque Impact Wrench with Pin Detent",
                MaterialNumber = "06019B1J14",
                ModelNumber = "HTH181-01",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolHTH181_01);

            var toolIWHT180_01 = new Tool
            {
                ToolId = 90129,
                Name = "IWHT180-01 (06019B1310) 18 V High Torque Impact Wrench with Friction Ring",
                MaterialNumber = "06019B1310",
                ModelNumber = "IWHT180-01",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolIWHT180_01);

            var tool24618_01 = new Tool
            {
                ToolId = 90130,
                Name = "24618-01 1/2 In. 18 V Impact Wrench",
                ModelNumber = "24618-01",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(tool24618_01);

            var toolIDH182_01 = new Tool
            {
                ToolId = 90131,
                Name = "IDH182-01 18 V EC Brushless 1/4 In. and 1/2 In. Socket-Ready Impact Driver",
                ModelNumber = "IDH182-01",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolIDH182_01);

            var toolIWMH182_01 = new Tool
            {
                ToolId = 90132,
                Name = "IWMH182-01 18 V EC Brushless 1/2 In. Impact Wrench Kit with Ball Detent",
                ModelNumber = "IWMH182-01",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolIWMH182_01);

            var toolPS41_2A = new Tool
            {
                ToolId = 90133,
                Name = "PS41-2A 12 V Max Impact Driver Kit",
                ModelNumber = "PS41-2A",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolPS41_2A);

            var tool1033VSR = new Tool
            {
                ToolId = 90134,
                Name = "1033VSR 1/2 In. High-Speed Drill",
                ModelNumber = "1033VSR",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(tool1033VSR);

            var toolADS181BL = new Tool
            {
                ToolId = 90135,
                Name = "ADS181BL 18 V 1/2 In. Right Angle Drill",
                ModelNumber = "ADS181BL",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolADS181BL);

            var toolDDH181X_01L = new Tool
            {
                ToolId = 90136,
                Name = "DDH181X-01L 18 V Brute Tough™ 1/2 In. Drill/Driver Kit with L-Boxx® Carrying Case",
                ModelNumber = "DDH181X-01L",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolDDH181X_01L);

            var toolDDS182_02L = new Tool
            {
                ToolId = 90137,
                Name = "DDS182-02L 18 V EC Brushless Compact Tough™ 1/2 In. Drill/Driver Kit",
                ModelNumber = "DDS182-02L",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolDDS182_02L);

            var toolDDS182 = new Tool
            {
                ToolId = 90138,
                Name = "DDS182-02L 18 V EC Brushless Compact Tough™ 1/2 In. Drill/Driver Kit",
                ModelNumber = "DDS182-02L",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolDDS182);

            var toolSG250 = new Tool
            {
                ToolId = 90139,
                Name = "SG250 2,500 RPM TEK Screwgun",
                ModelNumber = "SG250",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolSG250);

            var toolSG450 = new Tool
            {
                ToolId = 90140,
                Name = "SG450 4,500 RPM Drywall Screwgun",
                ModelNumber = "SG450",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolSG450);

            var toolSGH182BL = new Tool
            {
                ToolId = 90141,
                Name = "SGH182BL 18 V EC Brushless Screwgun with L-Boxx Carrying Case",
                ModelNumber = "SGH182BL",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolSGH182BL);

            var toolMRC23EVSK = new Tool
            {
                ToolId = 90142,
                Name = "2.3 HP Electronic Modular Router System",
                ModelNumber = "MRC23EVSK",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Tools.Add(toolMRC23EVSK);

            var savedCount = jobContext.SaveChanges();

            var accessoryDCB1244 = new Accessory
            {
                Name = "DCB1244 12 In. 44 Tooth Daredevil Table and Miter Saw Blade General Purpose",
                ModelNumber = "DCB1244",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryDCB1244);

            var accessoryDCB1280 = new Accessory
            {
                Name = "DCB1280 12 In. 80 Tooth Edge Circular Saw Blade for Extra-Fine Finish",
                ModelNumber = "DCB1280",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryDCB1280);

            var accessoryPS1260GP = new Accessory
            {
                Name = "PS1260GP 12 In. 60 Tooth Precision Series Circular Saw Blade",
                ModelNumber = "PS1260GP",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryPS1260GP);

            var accessoryDSB1013 = new Accessory
            {
                Name = "DSB1013 1 In. x 6 In. Daredevil™ Standard Spade Bits",
                ModelNumber = "DSB1013",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryDSB1013);

            var accessoryDSB5010 = new Accessory
            {
                Name = "DSB5010 10 pc. Daredevil™ Standard Spade Bit Set",
                ModelNumber = "DSB5010",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryDSB5010);

            var accessoryHMD200 =
                new Accessory
                {
                    Name = "HMD200 2 In. Daredevil Carbide Hole Saw",
                    ModelNumber = "HMD200",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Accessories.Add(accessoryHMD200);

            var accessoryT4047 = new Accessory
            {
                Name = "T4047 Multi-Size Screwdriver Bit Set",
                ModelNumber = "T4047",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryT4047);

            var accessory27275 = new Accessory
            {
                Name = "27275 (2610010647) 1 / 2 In.Impact Tough Deep Well Socket, 1 / 2 In.Shank",
                ModelNumber = "27275",
                MaterialNumber = "2610010647",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessory27275);

            var accessory27281 = new Accessory
            {
                Name = "27281 (2610010651) 3/4 In. Impact Tough Deep Well Socket, 1/2 In. Shank",
                ModelNumber = "27281",
                MaterialNumber = "2610010651",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessory27281);

            var accessory27286 = new Accessory
            {
                Name = "27286 (2610012444) 9 pc. Impact Tough Socket Set for 1/2 In. Drive",
                ModelNumber = "27286",
                MaterialNumber = "2610012444",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessory27286);

            var accessory24618_01 =
                new Accessory
                {
                    Name = "24618-01 1/2 In. 18 V Impact Wrench",
                    ModelNumber = "24618-01",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Accessories.Add(accessory24618_01);

            var accessorySBID21 = new Accessory
            {
                Name = "BL2143 1/4 In. x 4 In. Fractional Jobber Black Oxide Drill Bit",
                ModelNumber = "BL2143",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Add(accessorySBID21);

            var accessorySBID32 = new Accessory
            {
                Name = "SBID32 32 pc. Impact Tough™ Screwdriving Bit Set",
                ModelNumber = "SBID32",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessorySBID32);

            var accessoryFB016 = new Accessory
            {
                Name = "FB016 1 In. Forstner Bit",
                ModelNumber = "FB016",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryFB016);

            var accessoryMP02 =
                new Accessory
                {
                    Name = "MP02 5/32 In. x 4 In. x 6 In. Daredevil® Multipurpose Drill Bit",
                    ModelNumber = "MP02",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Accessories.Add(accessoryMP02);

            var accessoryMP03 =
                new Accessory
                {
                    Name = "MP03 3/16 In. x 4 In. x 6 In. Daredevil® Multipurpose Drill Bit",
                    ModelNumber = "MP03",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Accessories.Add(accessoryMP03);

            var accessoryMP06 = new Accessory
            {
                Name = "MP06 1/4 In. x 4 In. x 6 In. Daredevil® Multipurpose Drill Bit",
                ModelNumber = "MP06",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryMP06);

            var accessoryMP500T = new Accessory
            {
                Name = "MP500T 5 pc. Multipurpose Drill Bit Set",
                ModelNumber = "MP500T",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryMP500T);

            var accessoryITP2102 = new Accessory
            {
                Name = "ITP2102 Power Screwdriver Bit",
                ModelNumber = "ITP2102",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryITP2102);

            var accessoryITP2205 = new Accessory
            {
                Name = "ITP2205 Power Screwdriver Bit",
                ModelNumber = "ITP2205",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryITP2205);

            var accessorySBID39 = new Accessory
            {
                Name = "SBID39 39 pc. Impact Tough Screwdriving Bit Set",
                ModelNumber = "SBID39",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessorySBID39);

            var accessoryHCBG04T = new Accessory
            {
                Name = "HCBG04T 3/16 In. x 6 In. BlueGranite Turbo™ Carbide Hammer Drill Bit",
                ModelNumber = "HCBG04T",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryHCBG04T);

            var accessoryHCBG06T = new Accessory
            {
                Name = "HCBG06T 1/4 In. x 6 In. BlueGranite Turbo™ Carbide Hammer Drill Bit",
                ModelNumber = "HCBG06T",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryHCBG06T);

            var accessoryHCBG501T = new Accessory
            {
                Name = "HCBG501T 5 pc. BlueGranite™ Turbo Carbide Hammer Drill Bits Set",
                ModelNumber = "HCBG501T",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryHCBG501T);

            var accessoryIMD5007 = new Accessory
            {
                Name = "IMD5007 7-pc. Hex Shank Impact Tough Drill Bit Set, Black Oxide",
                ModelNumber = "IMD5007",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryIMD5007);

            var accessoryP2115TCB = new Accessory
            {
                Name = "P2115TCB 1 In. Impact Tough Phillips Insert Bit, 15 pc.",
                ModelNumber = "P2115TCB",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryP2115TCB);

            var accessoryP2R22205 = new Accessory
            {
                Name = "P2R22205 2 In. P2+R2 Power Bit (5 pc.)",
                ModelNumber = "P2R22205",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryP2R22205);

            var accessoryBL2143 = new Accessory
            {
                Name = "BL2143 1/4 In. x 4 In. Fractional Jobber Black Oxide Drill Bit",
                ModelNumber = "BL2143",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryBL2143);

            var accessoryTI2143 = new Accessory
            {
                Name = "TI2143 1/4 In. x 4 In. Titanium-Coated Drill Bit",
                ModelNumber = "TI2143",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryTI2143);

            var accessoryIT2490 = new Accessory
            {
                Name = "IT2490 3-pc. Impact Tough Nutsetter Bits Set",
                ModelNumber = "IT2490",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryIT2490);

            var accessoryDSB1009 = new Accessory
            {
                Name = "DSB1009 5 pc. 3/4 In. x 6 In. Daredevil™ Standard Spade Bits",
                ModelNumber = "DSB1009",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryDSB1009);

            var accessoryDSB5006 = new Accessory
            {
                Name = "DSB5006 6 pc. Daredevil™ Standard Spade Bit Set",
                ModelNumber = "DSB5006",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryDSB5006);

            var accessoryTI21 = new Accessory
            {
                Name = "TI21 21 pc. Titanium-Coated Drill Bit Set",
                ModelNumber = "TI21",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryTI21);

            var accessoryD60498 = new Accessory
            {
                Name = "D60498 Dimpler drywall screw setter",
                ModelNumber = "D60498",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryD60498);

            var accessoryDWS2 = new Accessory
            {
                Name = "DWS2 Power Screwdriver Bit",
                ModelNumber = "DWS2",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryDWS2);

            var accessoryDWS60497 = new Accessory
            {
                Name = "DWS60497 No. 2 Phillips Drywall Screw Setter",
                ModelNumber = "DWS60497",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryDWS60497);

            var accessoryP2D105 = new Accessory
            {
                Name = "P2D105 1 In., Phillips Insert Bit, Pt. P2R Reduced, Shank 1/4 In.",
                ModelNumber = "P2D105",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessoryP2D105);

            var accessory85445M = new Accessory
            {
                Name = "85445M 3/16 In. x 3/8 In. Carbide Tipped Core Box Bit",
                ModelNumber = "85445M",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessory85445M);

            var accessory85446M = new Accessory
            {
                Name = "85446M 1/4 In. x 1/2 In. Carbide Tipped Core Box Bit",
                ModelNumber = "85446M",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessory85446M);

            var accessory85448M = new Accessory
            {
                Name = "85448M 3/8 In. x 3/4 In. Carbide Tipped Core Box Bit",
                ModelNumber = "854458",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessory85448M);

            var accessory85268M = new Accessory
            {
                Name = "85268M 3/8 In. x 1 In. Carbide Tipped 2-Flute Flush Trim Bit",
                ModelNumber = "85268M",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessory85268M);

            var accessory85911M = new Accessory
            {
                Name = "85911M 1/4 In. x 1 In. Solid Carbide 2-Flute Upcut Spiral Bit",
                ModelNumber = "85911M",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessory85911M);

            var accessory85213M = new Accessory
            {
                Name = "85213M 1/8 In. x 1/2 In. Solid Carbide 2-Flute Straight Bit",
                ModelNumber = "85213M",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessory85213M);

            var accessory85248M = new Accessory
            {
                Name = "85248M 3/4 In. x 3/4 In. Carbide Tipped Hinge Mortising Bit",
                ModelNumber = "85248M",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessory85248M);

            var accessory85613M = new Accessory
            {
                Name = "85613M 1/4 In. x 1 In. Carbide Tipped 2-Flute Straight Bit",
                ModelNumber = "85613M",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessory85613M);

            var accessory85682M = new Accessory
            {
                Name = "85682M 3/4 In. x 1 In. Carbide Tipped 2-Flute Top Bearing Straight Trim Bit",
                ModelNumber = "85682M",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessory85682M);

            var accessory84624M = new Accessory
            {
                Name = "84624M 1-7/8 In. x 1/4 In. Carbide Tipped Tongue and Groove Bit",
                ModelNumber = "84624M",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessory84624M);

            var accessory84703M = new Accessory
            {
                Name = "84703M 14° x 1/2 In. Carbide Tipped Dovetail Bit",
                ModelNumber = "84703M",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessory84703M);

            var accessory85218M = new Accessory
            {
                Name = "85218M 3/8 In. x 1/2 In. Carbide Tipped Rabbeting Bit",
                ModelNumber = "85218M",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessory85218M);

            var accessory85614M = new Accessory
            {
                Name = "85614M 1/2 In. x 1/2 In. Carbide Tipped Rabbeting Bit",
                ModelNumber = "85614M",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Accessories.Add(accessory85614M);

            savedCount = jobContext.SaveChanges();

            #endregion

            #region BoschTools Applications defined 

            var hammerAndHammerDrill_laminatedWoodMaterial_hammerDrillApp = new Application
            {
                Name = "Hammer Drill",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(hammerAndHammerDrill_laminatedWoodMaterial_hammerDrillApp);

            var hammerAndHammerDrill_plywoodMaterial_hammerDrillApp = new Application
            {
                Name = "Hammer Drill",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(hammerAndHammerDrill_plywoodMaterial_hammerDrillApp);

            var hammerAndHammerDrill_chipboardMaterial_hammerDrillApp = new Application
            {
                Name = "Hammer Drill",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(hammerAndHammerDrill_chipboardMaterial_hammerDrillApp);

            var hammerAndHammerDrill_solidSurfaceMaterial_hammerDrillApp = new Application
            {
                Name = "Hammer Drill",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(hammerAndHammerDrill_solidSurfaceMaterial_hammerDrillApp);

            var hammerAndHammerDrill_woodComposities_hammerDrillApp = new Application
            {
                Name = "Hammer Drill",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(hammerAndHammerDrill_woodComposities_hammerDrillApp);

            var hammerAndHammerDrill_metal_hammerDrillApp = new Application
            {
                Name = "Hammer Drill",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(hammerAndHammerDrill_metal_hammerDrillApp);

            var hammerAndHammerDrill_fiberCement_hammerDrillApp = new Application
            {
                Name = "Hammer Drill",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(hammerAndHammerDrill_fiberCement_hammerDrillApp);

            var hammerAndHammerDrill_brickMortar_hammerDrillApp = new Application
            {
                Name = "Hammer Drill",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(hammerAndHammerDrill_brickMortar_hammerDrillApp);

            var hammerAndHammerDrill_concrete_hammerDrillApp = new Application
            {
                Name = "Hammer Drill",
                Tag = "Hammer Drill Job with Concrete Material",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(hammerAndHammerDrill_concrete_hammerDrillApp);

            var sandAndPolish_paintAndVarnishMaterial_finishSandAndPolishApp =
                new Application
                {
                    Name = "Finish Sand and Polish",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Applications.Add(sandAndPolish_paintAndVarnishMaterial_finishSandAndPolishApp);

            var sandAndPolish_paintAndVarnishMaterial_sandAndPolishApp = new Application
            {
                Name = "Sand and Polish",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(sandAndPolish_paintAndVarnishMaterial_sandAndPolishApp);

            var sandAndPolish_paintAndVarnishMaterial_detailSandApp = new Application
            {
                Name = "Sand and Polish",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(sandAndPolish_paintAndVarnishMaterial_detailSandApp);

            var plane_woodCompositesMaterial_planeApp = new Application
            {
                Name = "Plane",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(plane_woodCompositesMaterial_planeApp);

            var cut_laminatedWoodMaterial_ripCrossAndBevelCutsApp = new Application
            {
                Name = "Rip, Cross and Bevel",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(cut_laminatedWoodMaterial_ripCrossAndBevelCutsApp);

            var cut_laminatedWoodMaterial_ripCrossAndMiterCutsApp = new Application
            {
                Name = "Rip, Cross and Miter",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(cut_laminatedWoodMaterial_ripCrossAndMiterCutsApp);

            var cut_laminatedWoodMaterial_crossAndMiterCutsApp = new Application
            {
                Name = "Cross and Miter",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(cut_laminatedWoodMaterial_ripCrossAndMiterCutsApp);

            var cut_plasticMaterial_straightCurveTightCurveScrollCutsApp =
                new Application
                {
                    Name = "Straight, Curve, Tight Curve & Scroll Cuts",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Applications.Add(cut_plasticMaterial_straightCurveTightCurveScrollCutsApp);

            var cut_plywoodMaterial_ripCrossAndBevelCutsApp = new Application
            {
                Name = "Rip, Cross & Bevel Cuts",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(cut_plywoodMaterial_ripCrossAndBevelCutsApp);

            var cut_plywoodMaterial_ripCrossAndMiterCutsApp = new Application
            {
                Name = "Rip, Cross & Miter Cuts",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(cut_plywoodMaterial_ripCrossAndMiterCutsApp);

            var cut_plywoodMaterial_crossAndMiterCutsApp = new Application
            {
                Name = "Cross & Miter Cuts",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(cut_plywoodMaterial_crossAndMiterCutsApp);

            var cut_chipBoardMaterial_ripCrossAndBevelCutsApp = new Application
            {
                Name = "Rip, Cross & Bevel Cuts",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(cut_chipBoardMaterial_ripCrossAndBevelCutsApp);

            var cut_chipBoardMaterial_ripCrossAndMiterCutsApp = new Application
            {
                Name = "Rip, Cross & Miter Cuts",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(cut_chipBoardMaterial_ripCrossAndMiterCutsApp);

            var cut_chipBoardMaterial_crossAndMitersCutsApp = new Application
            {
                Name = "Cross & Miter Cuts",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(cut_chipBoardMaterial_crossAndMitersCutsApp);

            var cut_solidSurfaceMaterial_ripCrossAndBevelCutsApp =
                new Application
                {
                    Name = "Rip, Cross & Bevel Cuts",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Applications.Add(cut_solidSurfaceMaterial_ripCrossAndBevelCutsApp);

            var cut_solidSurfaceMaterial_ripCrossAndMiterCutsApp = new Application
            {
                Name = "Rip, Cross & Miter Cuts",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(cut_solidSurfaceMaterial_ripCrossAndMiterCutsApp);

            var cut_solidSurfaceMaterial_crossAndMitersCutsApp = new Application
            {
                Name = "Cross & Miter Cuts",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(cut_solidSurfaceMaterial_crossAndMitersCutsApp);

            var cut_woodCompositeMaterial_straightCurveTightCurveScrollCutsApp =
                new Application
                {
                    Name = "Straight, Curve, Tight Curve & Scroll Cuts",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Applications.Add(cut_woodCompositeMaterial_straightCurveTightCurveScrollCutsApp);

            var cut_woodCompositeMaterial_straightPlungeRaspCutsApp =
                new Application
                {
                    Name = "Straight, Plunge & Rasp Cuts",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Applications.Add(cut_woodCompositeMaterial_straightPlungeRaspCutsApp);

            var cut_woodCompositeMaterial_straightRipCrossBevelCutsApp =
                new Application
                {
                    Name = "Rip, Cross & Bevel Cuts",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Applications.Add(cut_woodCompositeMaterial_straightRipCrossBevelCutsApp);

            var cut_woodCompositeMaterial_straightCurvePlungeCurveRaspCutsApp =
                new Application
                {
                    Name = "Straight, Plunge & Rasp Cuts",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Applications.Add(cut_woodCompositeMaterial_straightCurvePlungeCurveRaspCutsApp);

            var cut_woodCompositeMaterial_straightCurveCutsApp = new Application
            {
                Name = "Straight & Curved Cuts",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(cut_woodCompositeMaterial_straightCurveCutsApp);

            var cut_metalMaterial_straightCurveTightCurveAndScrollCutsApp =
                new Application
                {
                    Name = "Straight, Curve, Tight Curve & Scroll Cuts",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Applications.Add(cut_metalMaterial_straightCurveTightCurveAndScrollCutsApp);

            var cut_metalMaterial_straightPlungeRaspCutsApp = new Application
            {
                Name = "Straight, Plunge & Rasp Cuts",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(cut_metalMaterial_straightPlungeRaspCutsApp);

            var cut_metalMaterial_straightCurvedCutsApp = new Application
            {
                Name = "Straight & Curved Cuts",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(cut_metalMaterial_straightCurvedCutsApp);

            var cut_metalMaterial_grindRoughCutsApp = new Application
            {
                Name = "Grind & Rough Cuts",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(cut_metalMaterial_grindRoughCutsApp);

            var drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp =
                new Application
                {
                    Name = "High Torque Drive & Fasten",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Applications.Add(drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp);

            var drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp =
                new Application
                {
                    Name = "Medium Torque Drive & Fasten",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Applications.Add(drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp);

            var drillAndDrive_laminatedWoodMaterial_drillApp = new Application
            {
                Name = "Drill & Drive",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(drillAndDrive_laminatedWoodMaterial_drillApp);

            var drillAndDrive_plywoodMaterial_highTorqueDriveApp =
                new Application
                {
                    Name = "High Torque Drive & Fasten",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Applications.Add(drillAndDrive_plywoodMaterial_highTorqueDriveApp);

            var drillAndDrive_plywoodMaterial_mediumTorqueDriveApp =
                new Application
                {
                    Name = "Medium Torque Drive & Fasten",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Applications.Add(drillAndDrive_plywoodMaterial_mediumTorqueDriveApp);

            var drillAndDrive_plywoodMaterial_drillAndDriveApp = new Application
            {
                Name = "Drill & Drive",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(drillAndDrive_plywoodMaterial_drillAndDriveApp);

            var drillAndDrive_chipBoardMaterial_highTorqueDriveApp =
                new Application
                {
                    Name = "High Torque Drive & Fasten",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Applications.Add(drillAndDrive_chipBoardMaterial_highTorqueDriveApp);

            var drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp =
                new Application
                {
                    Name = "Medium Torque Drive & Fasten",
                    Tag = "Drill & Drive Job with Chip Board Material",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Applications.Add(drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp);

            var drillAndDrive_chipBoardMaterial_drillAndDriveApp = new Application
            {
                Name = "Drill & Drive",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(drillAndDrive_chipBoardMaterial_drillAndDriveApp);

            var drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp =
                new Application {Name = "High Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp);

            var drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp =
                new Application {Name = "Medium Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp);

            var drillAndDrive_solidSurfaceMaterial_drillAndDriveApp =
                new Application {Name = "Drill & Drive", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(drillAndDrive_solidSurfaceMaterial_drillAndDriveApp);

            var drillAndDrive_woodCompositeMaterial_highTorqueDriveApp =
                new Application {Name = "High Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(drillAndDrive_woodCompositeMaterial_highTorqueDriveApp);

            var drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp =
                new Application {Name = "Medium Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp);

            var drillAndDrive_woodCompositeMaterial_drillAndDriveApp =
                new Application {Name = "Drill & Drive", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(drillAndDrive_woodCompositeMaterial_drillAndDriveApp);

            var drillAndDrive_metalMaterial_highTorqueDriveApp = new Application
            {
                Name = "High Torque Drive & Fasten",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(drillAndDrive_metalMaterial_highTorqueDriveApp);

            var drillAndDrive_metalMaterial_mediumTorqueDriveApp =
                new Application {Name = "Medium Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(drillAndDrive_metalMaterial_mediumTorqueDriveApp);

            var drillAndDrive_metalMaterial_drillAndDriveApp =
                new Application {Name = "Drill & Drive", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(drillAndDrive_metalMaterial_drillAndDriveApp);

            var drillAndDrive_fiberCementMaterial_highTorqueDriveApp =
                new Application {Name = "Drill & Drive", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(drillAndDrive_fiberCementMaterial_highTorqueDriveApp);

            var drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp =
                new Application {Name = "Drill & Drive", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp);

            var drillAndDrive_fiberCementMaterial_drillAndDriveApp =
                new Application {Name = "Drill & Drive", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(drillAndDrive_fiberCementMaterial_drillAndDriveApp);

            var fastenJob_laminatedWoodMaterial_hammerDrillAndRotateApp =
                new Application {Name = "Hammer Drill & Rotate Only", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_laminatedWoodMaterial_hammerDrillAndRotateApp);

            var fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp =
                new Application {Name = "High Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp);

            var fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp =
                new Application {Name = "Medium Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp);

            var fastenJob_laminatedWoodMaterial_drillAndDriveApp =
                new Application {Name = "Drill & Drive", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_laminatedWoodMaterial_drillAndDriveApp);

            var fastenJob_plywoodMaterial_hammerDrillAndRotateApp =
                new Application {Name = "Hammer Drill & Rotate Only", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_plywoodMaterial_hammerDrillAndRotateApp);

            var fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp =
                new Application {Name = "High Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp);

            var fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp =
                new Application {Name = "Medium Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp);

            var fastenJob_plywoodMaterial_drillAndDriveApp =
                new Application {Name = "Drill & Drive", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_plywoodMaterial_drillAndDriveApp);

            var fastenJob_chipBoardMaterial_hammerDrillAndRotateApp =
                new Application {Name = "Hammer Drill & Rotate Only", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_chipBoardMaterial_hammerDrillAndRotateApp);

            var fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp =
                new Application {Name = "High Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp);

            var fastenJob_chipBoardMaterial_mediumTorqueDriveAndFastenApp =
                new Application {Name = "Medium Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_chipBoardMaterial_mediumTorqueDriveAndFastenApp);

            var fastenJob_chipBoardMaterial_drillAndDriveApp =
                new Application {Name = "Drill & Drive", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_chipBoardMaterial_drillAndDriveApp);

            var fastenJob_solidSurfaceMaterial_hammerDrillAndRotateApp =
                new Application {Name = "Hammer Drill & Rotate Only", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_solidSurfaceMaterial_hammerDrillAndRotateApp);

            var fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp =
                new Application {Name = "High Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp);

            var fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp =
                new Application {Name = "Medium Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp);

            var fastenJob_solidSurfaceMaterial_drillAndDriveApp =
                new Application {Name = "Drill & Drive", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_solidSurfaceMaterial_drillAndDriveApp);

            var fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp =
                new Application {Name = "Hammer Drill & Rotate Only", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp);

            var fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp =
                new Application {Name = "High Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp);

            var fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp =
                new Application {Name = "Medium Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp);

            var fastenJob_woodCompositesMaterial_drillAndDriveApp =
                new Application {Name = "Drill & Drive", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_woodCompositesMaterial_drillAndDriveApp);

            var fastenJob_woodCompositesMaterial_driveApp =
                new Application {Name = "Drive", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_woodCompositesMaterial_driveApp);

            var fastenJob_woodCompositesMaterial_fastenApp =
                new Application {Name = "Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_woodCompositesMaterial_fastenApp);

            var fastenJob_metalMaterial_hammerDrillAndRotateApp = new Application
            {
                Name = "Hammer Drill & Rotate Only",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(fastenJob_metalMaterial_hammerDrillAndRotateApp);

            var fastenJob_metalMaterial_highTorqueDriveAndFastenApp =
                new Application {Name = "High Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_metalMaterial_highTorqueDriveAndFastenApp);

            var fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp =
                new Application {Name = "Medium Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp);

            var fastenJob_metalMaterial_drillAndDriveApp =
                new Application {Name = "Drill & Drive", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_metalMaterial_drillAndDriveApp);

            var fastenJob_metalMaterial_driveApp =
                new Application {Name = "Drive", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_metalMaterial_driveApp);

            var fastenJob_fiberCementMaterial_hammerDrillAndRotateApp =
                new Application {Name = "Hammer Drill & Rotate Only", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_fiberCementMaterial_hammerDrillAndRotateApp);

            var fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp =
                new Application {Name = "High Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp);

            var fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp =
                new Application {Name = "Medium Torque Drive & Fasten", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp);

            var fastenJob_fiberCementMaterial_drillAndDriveApp =
                new Application {Name = "Drill & Drive", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(fastenJob_fiberCementMaterial_drillAndDriveApp);

            var routeJob_laminatesMaterial_surfaceFormingApp = new Application
            {
                Name = "Surface Forming App",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(routeJob_laminatesMaterial_surfaceFormingApp);

            var routeJob_laminatesMaterial_straightRoutingAndMorticingApp =
                new Application {Name = "Straight Routing / Morticing", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(routeJob_laminatesMaterial_straightRoutingAndMorticingApp);

            var routeJob_laminatesMaterial_trimmingCutOutApp = new Application
            {
                Name = "Trimming / Cut-Out",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(routeJob_laminatesMaterial_trimmingCutOutApp);

            var routeJob_plasticsMaterial_surfaceFormingApp = new Application
            {
                Name = "Surface Forming App",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(routeJob_plasticsMaterial_surfaceFormingApp);

            var routeJob_plasticsMaterial_straightRoutingAndMorticingApp =
                new Application {Name = "Straight Routing / Morticing", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(routeJob_plasticsMaterial_straightRoutingAndMorticingApp);

            var routeJob_plasticsMaterial_trimmingCutOutApp = new Application
            {
                Name = "Trimming / Cut-Out",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(routeJob_plasticsMaterial_trimmingCutOutApp);

            var routeJob_aluminiumMaterial_surfaceFormingApp = new Application
            {
                Name = "Surface Forming App",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(routeJob_aluminiumMaterial_surfaceFormingApp);

            var routeJob_aluminiumMaterial_straightRoutingAndMorticingApp =
                new Application {Name = "Straight Routing / Morticing", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(routeJob_aluminiumMaterial_straightRoutingAndMorticingApp);

            var routeJob_aluminiumMaterial_trimmingCutOutApp = new Application
            {
                Name = "Trimming / Cut-Out",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(routeJob_aluminiumMaterial_trimmingCutOutApp);

            var routeJob_solidSurfaceMaterial_surfaceFormingApp = new Application
            {
                Name = "Surface Forming App",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(routeJob_solidSurfaceMaterial_surfaceFormingApp);

            var routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp =
                new Application {Name = "Straight Routing / Morticing", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp);

            var routeJob_solidSurfaceMaterial_trimmingCutOutApp = new Application
            {
                Name = "Trimming / Cut-Out",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(routeJob_solidSurfaceMaterial_trimmingCutOutApp);

            var routeJob_solidSurfaceMaterial_jointMakingApp =
                new Application {Name = "Joint-Making", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(routeJob_solidSurfaceMaterial_jointMakingApp);

            var routeJob_solidSurfaceMaterial_edgeFormingApp =
                new Application {Name = "Edge-Forming", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(routeJob_solidSurfaceMaterial_edgeFormingApp);

            var routeJob_woodCompositesMaterial_surfaceFormingApp = new Application
            {
                Name = "Surface Forming App",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(routeJob_woodCompositesMaterial_surfaceFormingApp);

            var routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp =
                new Application {Name = "Straight Routing / Morticing", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp);

            var routeJob_woodCompositesMaterial_trimmingCutOutApp = new Application
            {
                Name = "Trimming / Cut-Out",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Applications.Add(routeJob_woodCompositesMaterial_trimmingCutOutApp);

            var routeJob_woodCompositesMaterial_jointMakingApp =
                new Application {Name = "Joint-Making", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(routeJob_woodCompositesMaterial_jointMakingApp);

            var routeJob_woodCompositesMaterial_edgeFormingApp =
                new Application {Name = "Edge-Forming", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Applications.Add(routeJob_woodCompositesMaterial_edgeFormingApp);

            savedCount = jobContext.SaveChanges();

            #endregion

            #region BoschTools Materials defined along with Jobs-to-Materials relationships

            var hammerAndHammerDrillJob_laminatedWoodMaterial =
                new Material
                {
                    MaterialId = 100,
                    Name = "Laminated Wood / Composite Materials",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Materials.Add(hammerAndHammerDrillJob_laminatedWoodMaterial);

            var hammerAndHammerDrillJob_plywoodMaterial = new Material
            {
                MaterialId = 101,
                Name = "Plywood",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(hammerAndHammerDrillJob_plywoodMaterial);

            var hammerAndHammerDrillJob_chipboardMaterial = new Material
            {
                MaterialId = 102,
                Name = "Chipboard / OSB",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(hammerAndHammerDrillJob_chipboardMaterial);

            var hammerAndHammerDrillJob_solidSurfaceMaterial = new Material
            {
                MaterialId = 103,
                Name = "Solid Surface",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(hammerAndHammerDrillJob_solidSurfaceMaterial);

            var hammerAndHammerDrillJob_woodCompositiesMaterial =
                new Material
                {
                    MaterialId = 104,
                    Name = "Wood / Wood Composites",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Materials.Add(hammerAndHammerDrillJob_woodCompositiesMaterial);

            var hammerAndHammerDrillJob_metalMaterial = new Material
            {
                MaterialId = 105,
                Name = "Metal",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(hammerAndHammerDrillJob_metalMaterial);

            var hammerAndHammerDrillJob_fiberCementMaterial = new Material
            {
                MaterialId = 106,
                Name = "Fiber / Cement",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(hammerAndHammerDrillJob_fiberCementMaterial);

            var hammerAndHammerDrillJob_brickMortarMaterial = new Material
            {
                MaterialId = 107,
                Name = "Brick / Mortar",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(hammerAndHammerDrillJob_brickMortarMaterial);

            var hammerAndHammerDrillJob_concreteMaterial = new Material
            {
                MaterialId = 108,
                Name = "Concrete",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(hammerAndHammerDrillJob_concreteMaterial);

            var sandAndPolishJob_paintAndVarnishMaterial = new Material
            {
                MaterialId = 109,
                Name = "Paint / Varnish",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(sandAndPolishJob_paintAndVarnishMaterial);

            var sandAndPolishJob_woodCompositesMaterial =
                new Material
                {
                    MaterialId = 110,
                    Name = "Wood / Wood Composites",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Materials.Add(sandAndPolishJob_woodCompositesMaterial);

            var planeJob_woodCompositesMaterial = new Material
            {
                MaterialId = 111,
                Name = "Wood / Wood Composites",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(planeJob_woodCompositesMaterial);

            var cutJob_laminatedWoodMaterial =
                new Material
                {
                    MaterialId = 112,
                    Name = "Laminated Wood / Composite Material",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Materials.Add(cutJob_laminatedWoodMaterial);

            var cutJob_plasticMaterial =
                new Material {MaterialId = 113, Name = "Plastic", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Materials.Add(cutJob_plasticMaterial);

            var cutJob_plywoodMaterial =
                new Material {MaterialId = 114, Name = "Plywood", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Materials.Add(cutJob_plywoodMaterial);

            var cutJob_chipboardOsbMaterial = new Material
            {
                MaterialId = 115,
                Name = "Chipboard / OSB",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(cutJob_chipboardOsbMaterial);

            var cutJob_solidSurfaceMaterial = new Material
            {
                MaterialId = 116,
                Name = "Solid Surface",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(cutJob_solidSurfaceMaterial);

            var cutJob_woodCompositeMaterial = new Material
            {
                MaterialId = 117,
                Name = "Wood / Wood Composites",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(cutJob_woodCompositeMaterial);

            var cutJob_metalMaterial =
                new Material {MaterialId = 118, Name = "Metal", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Materials.Add(cutJob_metalMaterial);

            var cutJob_fiberCementMaterial = new Material
            {
                MaterialId = 119,
                Name = "Fiber / Cement",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(cutJob_fiberCementMaterial);

            var cutJob_woodWithNailsMaterial = new Material
            {
                MaterialId = 120,
                Name = "Wood with Nails",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(cutJob_woodCompositeMaterial);

            var cutJob_foamMaterial =
                new Material {MaterialId = 121, Name = "Foam", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Materials.Add(cutJob_foamMaterial);

            var drillAndDriveJob_laminatedWoodMaterial = new Material
            {
                MaterialId = 122,
                Name = "Laminated Wood",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(drillAndDriveJob_laminatedWoodMaterial);

            var drillandDriveJob_plywoodMaterial = new Material
            {
                MaterialId = 123,
                Name = "Plywood",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(drillandDriveJob_plywoodMaterial);

            var drillAndDriveJob_chipboardOsbMaterial = new Material
            {
                MaterialId = 124,
                Name = "Chipboard / OSB",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(drillAndDriveJob_chipboardOsbMaterial);

            var drillAndDriveJob_solidSurfaceMaterial = new Material
            {
                MaterialId = 125,
                Name = "Solid Surface",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(drillAndDriveJob_solidSurfaceMaterial);

            var drillAndDriveJob_woodCompositionMaterial =
                new Material
                {
                    MaterialId = 127,
                    Name = "Wood / Wood Composites",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Materials.Add(drillAndDriveJob_woodCompositionMaterial);

            var drillAndDriveJob_metalMaterial = new Material
            {
                MaterialId = 128,
                Name = "Metal",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(drillAndDriveJob_metalMaterial);

            var drillAndDriveJob_fiberCementMaterial = new Material
            {
                MaterialId = 129,
                Name = "Fiber Cement",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(drillAndDriveJob_fiberCementMaterial);

            var fastenJob_laminatedWoodMaterial =
                new Material
                {
                    MaterialId = 130,
                    Name = "Laminated Wood/Composite Materials",
                    DomainId = BoschToolsDataTenant.DomainId
                };
            jobContext.Materials.Add(fastenJob_laminatedWoodMaterial);

            var fastenJob_plywoodMaterial = new Material
            {
                MaterialId = 131,
                Name = "Plywood",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(fastenJob_plywoodMaterial);

            var fastenJob_chipBoardMaterial = new Material
            {
                MaterialId = 132,
                Name = "Chipboard / OSB",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(fastenJob_chipBoardMaterial);

            var fastenJob_solidSurfaceMaterial = new Material
            {
                MaterialId = 133,
                Name = "Solid Surface",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(fastenJob_solidSurfaceMaterial);

            var fastenJob_woodCompositesMaterial = new Material
            {
                MaterialId = 134,
                Name = "Wood / Wood Composites",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(fastenJob_woodCompositesMaterial);

            var fastenJob_metalMaterial =
                new Material {MaterialId = 135, Name = "Metal", DomainId = BoschToolsDataTenant.DomainId};
            jobContext.Materials.Add(fastenJob_metalMaterial);

            var fastenJob_fiberCementMaterial = new Material
            {
                MaterialId = 136,
                Name = "Fiber Cement",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(fastenJob_fiberCementMaterial);

            var routeJob_laminatesMaterial = new Material
            {
                MaterialId = 137,
                Name = "Laminates",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(routeJob_laminatesMaterial);

            var routeJob_plasticMaterial = new Material
            {
                MaterialId = 138,
                Name = "Plastics",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(routeJob_plasticMaterial);

            var routeJob_aluminiumMaterial = new Material
            {
                MaterialId = 139,
                Name = "Aluminium",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(routeJob_aluminiumMaterial);

            var routeJob_solidSurfaceMaterial = new Material
            {
                MaterialId = 140,
                Name = "Solid Surface",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(routeJob_solidSurfaceMaterial);

            var routeJob_woodCompositesMaterial = new Material
            {
                MaterialId = 141,
                Name = "Wood / Wood Composites",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Materials.Add(routeJob_woodCompositesMaterial);

            savedCount = jobContext.SaveChanges();

            #endregion

            #region Bosch Tools Jobs defined

            var hammerAndHammerDrillJob = new Job
            {
                JobId = 1,
                Name = "Hammer & Hammer Drill",
                DomainId = BoschToolsDataTenant.DomainId
            };
            var sandAndPolishJob = new Job
            {
                JobId = 2,
                Name = "Sand & Polish",
                DomainId = BoschToolsDataTenant.DomainId
            };
            var planeJob = new Job
            {
                JobId = 3,
                Name = "Plane",
                DomainId = BoschToolsDataTenant.DomainId
            };
            var cutJob = new Job
            {
                JobId = 4,
                Name = "Cut",
                DomainId = BoschToolsDataTenant.DomainId
            };
            var drillAndDriveJob = new Job
            {
                JobId = 5,
                Name = "Drill & Drive",
                DomainId = BoschToolsDataTenant.DomainId
            };
            var fastenJob = new Job
            {
                JobId = 6,
                Name = "Fasten",
                DomainId = BoschToolsDataTenant.DomainId
            };
            var routeJob = new Job
            {
                JobId = 7,
                Name = "Route",
                DomainId = BoschToolsDataTenant.DomainId
            };
            var grindJob = new Job
            {
                JobId = 8,
                Name = "Grind",
                DomainId = BoschToolsDataTenant.DomainId
            };
            var collectDustJob = new Job
            {
                JobId = 9,
                Name = "Collect Dust",
                DomainId = BoschToolsDataTenant.DomainId
            };
            var measureJob = new Job
            {
                JobId = 10,
                Name = "Measure",
                DomainId = BoschToolsDataTenant.DomainId
            };
            var detectJob = new Job
            {
                JobId = 11,
                Name = "Detect",
                DomainId = BoschToolsDataTenant.DomainId
            };
            var levelJob = new Job
            {
                JobId = 12,
                Name = "Level",
                DomainId = BoschToolsDataTenant.DomainId
            };
            jobContext.Jobs.AddRange(new List<Job>
            {
                hammerAndHammerDrillJob,
                sandAndPolishJob,
                planeJob,
                cutJob,
                drillAndDriveJob,
                fastenJob,
                routeJob,
                grindJob,
                collectDustJob,
                measureJob,
                detectJob,
                levelJob
            });

            #endregion

            #region Bosch Power Tools Job-to-Material Relationships

            hammerAndHammerDrillJob.Materials = new List<Material>
            {
                hammerAndHammerDrillJob_brickMortarMaterial,
                hammerAndHammerDrillJob_laminatedWoodMaterial,
                hammerAndHammerDrillJob_plywoodMaterial,
                hammerAndHammerDrillJob_chipboardMaterial,
                hammerAndHammerDrillJob_solidSurfaceMaterial,
                hammerAndHammerDrillJob_woodCompositiesMaterial,
                hammerAndHammerDrillJob_metalMaterial,
                hammerAndHammerDrillJob_fiberCementMaterial,
                hammerAndHammerDrillJob_concreteMaterial
            };
            sandAndPolishJob.Materials = new List<Material>
            {
                sandAndPolishJob_paintAndVarnishMaterial,
                sandAndPolishJob_woodCompositesMaterial
            };
            planeJob.Materials = new List<Material> {planeJob_woodCompositesMaterial};
            cutJob.Materials = new List<Material>
            {
                cutJob_laminatedWoodMaterial,
                cutJob_plasticMaterial,
                cutJob_plywoodMaterial,
                cutJob_chipboardOsbMaterial,
                cutJob_solidSurfaceMaterial,
                cutJob_woodCompositeMaterial,
                cutJob_metalMaterial,
                cutJob_fiberCementMaterial,
                cutJob_woodWithNailsMaterial,
                cutJob_foamMaterial
            };
            drillAndDriveJob.Materials = new List<Material>
            {
                drillAndDriveJob_laminatedWoodMaterial,
                drillandDriveJob_plywoodMaterial,
                drillAndDriveJob_chipboardOsbMaterial,
                drillAndDriveJob_solidSurfaceMaterial,
                drillAndDriveJob_woodCompositionMaterial,
                drillAndDriveJob_metalMaterial,
                drillAndDriveJob_fiberCementMaterial
            };
            fastenJob.Materials = new List<Material>
            {
                fastenJob_laminatedWoodMaterial,
                fastenJob_plywoodMaterial,
                fastenJob_chipBoardMaterial,
                fastenJob_solidSurfaceMaterial,
                fastenJob_woodCompositesMaterial,
                fastenJob_metalMaterial,
                fastenJob_fiberCementMaterial
            };
            routeJob.Materials = new List<Material>
            {
                routeJob_laminatesMaterial,
                routeJob_plasticMaterial,
                routeJob_aluminiumMaterial,
                routeJob_solidSurfaceMaterial,
                routeJob_woodCompositesMaterial
            };

            #endregion

            #region Bosch Power Tools Materials-to-Applications Relationships

            hammerAndHammerDrillJob_brickMortarMaterial.Applications =
                new List<Application> {hammerAndHammerDrill_brickMortar_hammerDrillApp};
            hammerAndHammerDrillJob_chipboardMaterial.Applications =
                new List<Application> {hammerAndHammerDrill_chipboardMaterial_hammerDrillApp};
            hammerAndHammerDrillJob_concreteMaterial.Applications =
                new List<Application> {hammerAndHammerDrill_concrete_hammerDrillApp };
            hammerAndHammerDrillJob_fiberCementMaterial.Applications =
                new List<Application> {hammerAndHammerDrill_fiberCement_hammerDrillApp};
            hammerAndHammerDrillJob_laminatedWoodMaterial.Applications =
                new List<Application> {hammerAndHammerDrill_laminatedWoodMaterial_hammerDrillApp};
            hammerAndHammerDrillJob_metalMaterial.Applications =
                new List<Application> {hammerAndHammerDrill_metal_hammerDrillApp};
            hammerAndHammerDrillJob_plywoodMaterial.Applications =
                new List<Application> {hammerAndHammerDrill_plywoodMaterial_hammerDrillApp};
            hammerAndHammerDrillJob_solidSurfaceMaterial.Applications =
                new List<Application> {hammerAndHammerDrill_solidSurfaceMaterial_hammerDrillApp};
            hammerAndHammerDrillJob_woodCompositiesMaterial.Applications =
                new List<Application> {hammerAndHammerDrill_woodComposities_hammerDrillApp};

            sandAndPolishJob_paintAndVarnishMaterial.Applications = new List<Application>
            {
                sandAndPolish_paintAndVarnishMaterial_finishSandAndPolishApp,
                sandAndPolish_paintAndVarnishMaterial_sandAndPolishApp,
                sandAndPolish_paintAndVarnishMaterial_detailSandApp
            };

            cutJob_laminatedWoodMaterial.Applications = new List<Application>
            {
                cut_laminatedWoodMaterial_crossAndMiterCutsApp,
                cut_laminatedWoodMaterial_ripCrossAndBevelCutsApp,
                cut_laminatedWoodMaterial_ripCrossAndMiterCutsApp
            };
            cutJob_plasticMaterial.Applications =
                new List<Application> {cut_plasticMaterial_straightCurveTightCurveScrollCutsApp};
            cutJob_plywoodMaterial.Applications =
                new List<Application>
                {
                    cut_plywoodMaterial_crossAndMiterCutsApp,
                    cut_plywoodMaterial_ripCrossAndBevelCutsApp,
                    cut_plywoodMaterial_ripCrossAndMiterCutsApp
                };
            cutJob_chipboardOsbMaterial.Applications = new List<Application>
            {
                cut_chipBoardMaterial_crossAndMitersCutsApp,
                cut_chipBoardMaterial_ripCrossAndBevelCutsApp,
                cut_chipBoardMaterial_ripCrossAndMiterCutsApp
            };
            cutJob_solidSurfaceMaterial.Applications = new List<Application>
            {
                cut_solidSurfaceMaterial_crossAndMitersCutsApp,
                cut_solidSurfaceMaterial_ripCrossAndBevelCutsApp,
                cut_solidSurfaceMaterial_ripCrossAndMiterCutsApp,
            };
            cutJob_woodCompositeMaterial.Applications = new List<Application>
            {
                cut_woodCompositeMaterial_straightCurveCutsApp,
                cut_woodCompositeMaterial_straightCurvePlungeCurveRaspCutsApp,
                cut_woodCompositeMaterial_straightCurveTightCurveScrollCutsApp,
                cut_woodCompositeMaterial_straightPlungeRaspCutsApp,
                cut_woodCompositeMaterial_straightRipCrossBevelCutsApp
            };
            cutJob_metalMaterial.Applications =
                new List<Application>
                {
                    cut_metalMaterial_straightCurveTightCurveAndScrollCutsApp,
                    cut_metalMaterial_straightPlungeRaspCutsApp,
                    cut_metalMaterial_straightCurvedCutsApp,
                    cut_metalMaterial_grindRoughCutsApp
                };

            drillAndDriveJob_laminatedWoodMaterial.Applications = new List<Application>
            {
                drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp,
                drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp,
                drillAndDrive_laminatedWoodMaterial_drillApp
            };

            drillandDriveJob_plywoodMaterial.Applications = new List<Application> {
                drillAndDrive_plywoodMaterial_drillAndDriveApp,
                drillAndDrive_plywoodMaterial_mediumTorqueDriveApp,
                drillAndDrive_plywoodMaterial_highTorqueDriveApp
            };

            drillAndDriveJob_chipboardOsbMaterial.Applications = new List<Application>
            {
                drillAndDrive_chipBoardMaterial_highTorqueDriveApp,
                drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp,
                drillAndDrive_chipBoardMaterial_drillAndDriveApp
            };
            drillAndDriveJob_solidSurfaceMaterial.Applications = new List<Application>
            {
                drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp,
                drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp,
                drillAndDrive_solidSurfaceMaterial_drillAndDriveApp
            };
            drillAndDriveJob_woodCompositionMaterial.Applications = new List<Application>
            {
                drillAndDrive_woodCompositeMaterial_highTorqueDriveApp,
                drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp,
                drillAndDrive_woodCompositeMaterial_drillAndDriveApp
            };
            drillAndDriveJob_metalMaterial.Applications = new List<Application>
            {
                drillAndDrive_metalMaterial_highTorqueDriveApp,
                drillAndDrive_metalMaterial_mediumTorqueDriveApp,
                drillAndDrive_metalMaterial_drillAndDriveApp
            };
            drillAndDriveJob_fiberCementMaterial.Applications = new List<Application>
            {
                drillAndDrive_fiberCementMaterial_highTorqueDriveApp,
                drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp,
                drillAndDrive_fiberCementMaterial_drillAndDriveApp
            };

            fastenJob_laminatedWoodMaterial.Applications = new List<Application>
            {
                fastenJob_laminatedWoodMaterial_hammerDrillAndRotateApp,
                fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp,
                fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp,
                fastenJob_laminatedWoodMaterial_drillAndDriveApp
            };
            fastenJob_plywoodMaterial.Applications = new List<Application>
            {
                fastenJob_plywoodMaterial_hammerDrillAndRotateApp,
                fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp,
                fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp,
                fastenJob_plywoodMaterial_drillAndDriveApp
            };
            fastenJob_chipBoardMaterial.Applications = new List<Application>
            {
                fastenJob_chipBoardMaterial_hammerDrillAndRotateApp,
                fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp,
                fastenJob_chipBoardMaterial_mediumTorqueDriveAndFastenApp,
                fastenJob_chipBoardMaterial_drillAndDriveApp
            };
            fastenJob_solidSurfaceMaterial.Applications = new List<Application>
            {
                fastenJob_solidSurfaceMaterial_hammerDrillAndRotateApp,
                fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp,
                fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp,
                fastenJob_solidSurfaceMaterial_drillAndDriveApp
            };
            fastenJob_woodCompositesMaterial.Applications = new List<Application>
            {
                fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp,
                fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp,
                fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp,
                fastenJob_woodCompositesMaterial_drillAndDriveApp,
                fastenJob_woodCompositesMaterial_driveApp,
                fastenJob_woodCompositesMaterial_fastenApp
            };
            fastenJob_metalMaterial.Applications = new List<Application>
            {
                fastenJob_metalMaterial_hammerDrillAndRotateApp,
                fastenJob_metalMaterial_highTorqueDriveAndFastenApp,
                fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp,
                fastenJob_metalMaterial_drillAndDriveApp,
                fastenJob_metalMaterial_driveApp
            };
            fastenJob_fiberCementMaterial.Applications = new List<Application>
            {
                fastenJob_fiberCementMaterial_hammerDrillAndRotateApp,
                fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp,
                fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp,
                fastenJob_fiberCementMaterial_drillAndDriveApp
            };

            planeJob_woodCompositesMaterial.Applications = new List<Application>
            {
                plane_woodCompositesMaterial_planeApp
            };

            routeJob_laminatesMaterial.Applications = new List<Application>
            {
                routeJob_laminatesMaterial_surfaceFormingApp,
                routeJob_laminatesMaterial_straightRoutingAndMorticingApp,
                routeJob_laminatesMaterial_trimmingCutOutApp
            };
            routeJob_plasticMaterial.Applications = new List<Application>
            {
                routeJob_plasticsMaterial_surfaceFormingApp,
                routeJob_plasticsMaterial_straightRoutingAndMorticingApp,
                routeJob_plasticsMaterial_trimmingCutOutApp
            };
            routeJob_aluminiumMaterial.Applications = new List<Application>
            {
                routeJob_aluminiumMaterial_surfaceFormingApp,
                routeJob_aluminiumMaterial_straightRoutingAndMorticingApp,
                routeJob_aluminiumMaterial_trimmingCutOutApp
            };
            routeJob_solidSurfaceMaterial.Applications = new List<Application>
            {
                routeJob_solidSurfaceMaterial_surfaceFormingApp,
                routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp,
                routeJob_solidSurfaceMaterial_trimmingCutOutApp,
                routeJob_solidSurfaceMaterial_edgeFormingApp,
                routeJob_solidSurfaceMaterial_jointMakingApp
            };
            routeJob_woodCompositesMaterial.Applications = new List<Application>
            {
                routeJob_woodCompositesMaterial_surfaceFormingApp,
                routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp,
                routeJob_woodCompositesMaterial_trimmingCutOutApp,
                routeJob_woodCompositesMaterial_edgeFormingApp,
                routeJob_woodCompositesMaterial_jointMakingApp
            };

            savedCount = jobContext.SaveChanges();

            #endregion

            #region Bosch Tools Applications-to-Tools and Application-to-Accessories Relationships

            /*
            Technical notes & next steps:

                values work with id of 1, 76, .. (from Postman)
                        write new unit tests to exercise this
                also, bring the unit tests back to passing, re-structure the unit tests as necessary
                round out the issues with data-model mapping and late data loading
                make the EF models clean and ok, take advantage of 2.0 features when possible
                check boschtools.com and validate mappings, also bring some tool info updates

            continue to re-build the schema and test on SQL Server (ndsd03)

            steps to rebuild and test:

                set | grep RB_
                mysql -h localhost -u root -p   (drop DB and re-create)
                dotnet run -op=create
                dotnet run -op=sample
                mysql -h localhost -u root -p   (verify)
                dotnet run
            */

            // 1: Hammer and Hammer Drill Job
            var hammerAndHammerDrill_laminatedWoodMaterial_hammerDrillApp_Tools =
                new List<ApplicationToolRelationship>();
            hammerAndHammerDrill_laminatedWoodMaterial_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_laminatedWoodMaterial_hammerDrillApp.ApplicationId,
                ToolId = toolHD18_2.ToolId
            });
            hammerAndHammerDrill_laminatedWoodMaterial_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_laminatedWoodMaterial_hammerDrillApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            hammerAndHammerDrill_laminatedWoodMaterial_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_laminatedWoodMaterial_hammerDrillApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            hammerAndHammerDrill_laminatedWoodMaterial_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_laminatedWoodMaterial_hammerDrillApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            hammerAndHammerDrill_laminatedWoodMaterial_hammerDrillApp.ToolRelationships =
                hammerAndHammerDrill_laminatedWoodMaterial_hammerDrillApp_Tools;

            var hammerAndHammerDrill_plywoodMaterial_hammerDrillApp_Tools = new List<ApplicationToolRelationship>();
            hammerAndHammerDrill_plywoodMaterial_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_plywoodMaterial_hammerDrillApp.ApplicationId,
                ToolId = toolHD18_2.ToolId
            });
            hammerAndHammerDrill_plywoodMaterial_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_plywoodMaterial_hammerDrillApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            hammerAndHammerDrill_plywoodMaterial_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_plywoodMaterial_hammerDrillApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            hammerAndHammerDrill_plywoodMaterial_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_plywoodMaterial_hammerDrillApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            hammerAndHammerDrill_plywoodMaterial_hammerDrillApp.ToolRelationships =
                hammerAndHammerDrill_plywoodMaterial_hammerDrillApp_Tools;

            var hammerAndHammerDrill_plywoodMaterial_hammerDrillApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            hammerAndHammerDrill_plywoodMaterial_hammerDrillApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = hammerAndHammerDrill_plywoodMaterial_hammerDrillApp.ApplicationId,
                AccessoryId = accessoryMP02.AccessoryId
            });
            hammerAndHammerDrill_plywoodMaterial_hammerDrillApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = hammerAndHammerDrill_plywoodMaterial_hammerDrillApp.ApplicationId,
                AccessoryId = accessoryMP03.AccessoryId
            });
            hammerAndHammerDrill_plywoodMaterial_hammerDrillApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = hammerAndHammerDrill_plywoodMaterial_hammerDrillApp.ApplicationId,
                AccessoryId = accessoryMP06.AccessoryId
            });
            hammerAndHammerDrill_plywoodMaterial_hammerDrillApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = hammerAndHammerDrill_plywoodMaterial_hammerDrillApp.ApplicationId,
                AccessoryId = accessoryMP500T.AccessoryId
            });
            hammerAndHammerDrill_plywoodMaterial_hammerDrillApp.AccessoryRelationships =
                hammerAndHammerDrill_plywoodMaterial_hammerDrillApp_Accessories;

            var hammerAndHammerDrill_chipboardMaterial_hammerDrillApp_Tools = new List<ApplicationToolRelationship>();
            hammerAndHammerDrill_chipboardMaterial_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_chipboardMaterial_hammerDrillApp.ApplicationId,
                ToolId = toolHD18_2.ToolId
            });
            hammerAndHammerDrill_chipboardMaterial_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_chipboardMaterial_hammerDrillApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            hammerAndHammerDrill_chipboardMaterial_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_chipboardMaterial_hammerDrillApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            hammerAndHammerDrill_chipboardMaterial_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_chipboardMaterial_hammerDrillApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            hammerAndHammerDrill_chipboardMaterial_hammerDrillApp.ToolRelationships =
                hammerAndHammerDrill_chipboardMaterial_hammerDrillApp_Tools;

            var hammerAndHammerDrill_chipboardMaterial_hammerDrillApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            hammerAndHammerDrill_chipboardMaterial_hammerDrillApp.AccessoryRelationships =
                hammerAndHammerDrill_chipboardMaterial_hammerDrillApp_Accessories;

            var hammerAndHammerDrill_solidSurfaceMaterial_hammerDrillApp_Tools =
                new List<ApplicationToolRelationship>();
            hammerAndHammerDrill_solidSurfaceMaterial_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_solidSurfaceMaterial_hammerDrillApp.ApplicationId,
                ToolId = toolHD18_2.ToolId
            });
            hammerAndHammerDrill_solidSurfaceMaterial_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_solidSurfaceMaterial_hammerDrillApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            hammerAndHammerDrill_solidSurfaceMaterial_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_solidSurfaceMaterial_hammerDrillApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            hammerAndHammerDrill_solidSurfaceMaterial_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_solidSurfaceMaterial_hammerDrillApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            hammerAndHammerDrill_solidSurfaceMaterial_hammerDrillApp.ToolRelationships =
                hammerAndHammerDrill_solidSurfaceMaterial_hammerDrillApp_Tools;

            var hammerAndHammerDrill_woodComposities_hammerDrillApp_Tools = new List<ApplicationToolRelationship>();
            hammerAndHammerDrill_woodComposities_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_woodComposities_hammerDrillApp.ApplicationId,
                ToolId = toolHD18_2.ToolId
            });
            hammerAndHammerDrill_woodComposities_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_woodComposities_hammerDrillApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            hammerAndHammerDrill_woodComposities_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_woodComposities_hammerDrillApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            hammerAndHammerDrill_woodComposities_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_woodComposities_hammerDrillApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            hammerAndHammerDrill_woodComposities_hammerDrillApp.ToolRelationships =
                hammerAndHammerDrill_woodComposities_hammerDrillApp_Tools;

            var hammerAndHammerDrill_metal_hammerDrillApp_Tools = new List<ApplicationToolRelationship>();
            hammerAndHammerDrill_metal_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_metal_hammerDrillApp.ApplicationId,
                ToolId = toolHD18_2.ToolId
            });
            hammerAndHammerDrill_metal_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_metal_hammerDrillApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            hammerAndHammerDrill_metal_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_metal_hammerDrillApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            hammerAndHammerDrill_metal_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_metal_hammerDrillApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            hammerAndHammerDrill_metal_hammerDrillApp.ToolRelationships =
                hammerAndHammerDrill_metal_hammerDrillApp_Tools;

            var hammerAndHammerDrill_fiberCement_hammerDrillApp_Tools = new List<ApplicationToolRelationship>();
            hammerAndHammerDrill_fiberCement_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_fiberCement_hammerDrillApp.ApplicationId,
                ToolId = toolHD18_2.ToolId
            });
            hammerAndHammerDrill_fiberCement_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_fiberCement_hammerDrillApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            hammerAndHammerDrill_fiberCement_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_fiberCement_hammerDrillApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            hammerAndHammerDrill_fiberCement_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_fiberCement_hammerDrillApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            hammerAndHammerDrill_fiberCement_hammerDrillApp.ToolRelationships =
                hammerAndHammerDrill_fiberCement_hammerDrillApp_Tools;

            var hammerAndHammerDrill_brickMortar_hammerDrillApp_Tools = new List<ApplicationToolRelationship>();
            hammerAndHammerDrill_brickMortar_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_brickMortar_hammerDrillApp.ApplicationId,
                ToolId = toolHD18_2.ToolId
            });
            hammerAndHammerDrill_brickMortar_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_brickMortar_hammerDrillApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            hammerAndHammerDrill_brickMortar_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_brickMortar_hammerDrillApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            hammerAndHammerDrill_brickMortar_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_brickMortar_hammerDrillApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            hammerAndHammerDrill_brickMortar_hammerDrillApp.ToolRelationships =
                hammerAndHammerDrill_brickMortar_hammerDrillApp_Tools;

            var hammerAndHammerDrill_concrete_hammerDrillApp_Tools = new List<ApplicationToolRelationship>();
            hammerAndHammerDrill_concrete_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_concrete_hammerDrillApp.ApplicationId,
                ToolId = toolHD18_2.ToolId
            });
            hammerAndHammerDrill_concrete_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_concrete_hammerDrillApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            hammerAndHammerDrill_concrete_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_concrete_hammerDrillApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            hammerAndHammerDrill_concrete_hammerDrillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = hammerAndHammerDrill_concrete_hammerDrillApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            hammerAndHammerDrill_concrete_hammerDrillApp.ToolRelationships =
                hammerAndHammerDrill_concrete_hammerDrillApp_Tools;

            // 2: Sand and Polish Job
            var sandAndPolish_paintAndVarnishMaterial_sandAndPolishApp_Tools = new List<ApplicationToolRelationship>();
            sandAndPolish_paintAndVarnishMaterial_sandAndPolishApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = sandAndPolish_paintAndVarnishMaterial_sandAndPolishApp.ApplicationId,
                ToolId = toolGSS20_40.ToolId
            });
            sandAndPolish_paintAndVarnishMaterial_sandAndPolishApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = sandAndPolish_paintAndVarnishMaterial_sandAndPolishApp.ApplicationId,
                ToolId = tool_OS50VC.ToolId
            });
            sandAndPolish_paintAndVarnishMaterial_sandAndPolishApp.ToolRelationships =
                sandAndPolish_paintAndVarnishMaterial_sandAndPolishApp_Tools;

            // 3: Plane Job
            var plane_woodCompositesMaterial_planeApp_Tools = new List<ApplicationToolRelationship>();
            plane_woodCompositesMaterial_planeApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = plane_woodCompositesMaterial_planeApp.ApplicationId,
                ToolId = toolPL1632.ToolId
            });
            plane_woodCompositesMaterial_planeApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = plane_woodCompositesMaterial_planeApp.ApplicationId,
                ToolId = toolPL2632K.ToolId
            });
            plane_woodCompositesMaterial_planeApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = plane_woodCompositesMaterial_planeApp.ApplicationId,
                ToolId = toolPLH181B.ToolId
            });
            plane_woodCompositesMaterial_planeApp.ToolRelationships = plane_woodCompositesMaterial_planeApp_Tools;

            // 4: Cut Job
            var cut_laminatedWoodMaterial_ripCrossAndBevelCutsApp_Tools = new List<ApplicationToolRelationship>();
            cut_laminatedWoodMaterial_ripCrossAndBevelCutsApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = cut_laminatedWoodMaterial_ripCrossAndBevelCutsApp.ApplicationId,
                ToolId = toolCCS180BL.ToolId
            });
            cut_laminatedWoodMaterial_ripCrossAndBevelCutsApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = cut_laminatedWoodMaterial_ripCrossAndBevelCutsApp.ApplicationId,
                ToolId = toolCS10.ToolId
            });
            cut_laminatedWoodMaterial_ripCrossAndBevelCutsApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = cut_laminatedWoodMaterial_ripCrossAndBevelCutsApp.ApplicationId,
                ToolId = toolCS5.ToolId
            });
            cut_laminatedWoodMaterial_ripCrossAndBevelCutsApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = cut_laminatedWoodMaterial_ripCrossAndBevelCutsApp.ApplicationId,
                ToolId = toolCSW41.ToolId
            });
            cut_laminatedWoodMaterial_ripCrossAndBevelCutsApp.ToolRelationships =
                cut_laminatedWoodMaterial_ripCrossAndBevelCutsApp_Tools;

            var cut_laminatedWoodMaterial_ripCrossAndMiterCutsApp_Tools = new List<ApplicationToolRelationship>();
            cut_laminatedWoodMaterial_ripCrossAndMiterCutsApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = cut_laminatedWoodMaterial_ripCrossAndMiterCutsApp.ApplicationId,
                ToolId = tool4100_09.ToolId
            });
            cut_laminatedWoodMaterial_ripCrossAndMiterCutsApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = cut_laminatedWoodMaterial_ripCrossAndMiterCutsApp.ApplicationId,
                ToolId = toolGTS1031.ToolId
            });
            cut_laminatedWoodMaterial_ripCrossAndMiterCutsApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = cut_laminatedWoodMaterial_ripCrossAndMiterCutsApp.ApplicationId,
                ToolId = toolTS2100.ToolId
            });
            cut_laminatedWoodMaterial_ripCrossAndBevelCutsApp.ToolRelationships =
                cut_laminatedWoodMaterial_ripCrossAndMiterCutsApp_Tools;

            var cut_laminatedWoodMaterial_ripCrossAndMiterCutsApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            cut_laminatedWoodMaterial_ripCrossAndBevelCutsApp.AccessoryRelationships =
                cut_laminatedWoodMaterial_ripCrossAndMiterCutsApp_Accessories;

            var cut_laminatedWoodMaterial_crossAndMiterCutsApp_Tools = new List<ApplicationToolRelationship>();
            cut_laminatedWoodMaterial_crossAndMiterCutsApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = cut_laminatedWoodMaterial_crossAndMiterCutsApp.ApplicationId,
                ToolId = tool5313.ToolId
            });
            cut_laminatedWoodMaterial_crossAndMiterCutsApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = cut_laminatedWoodMaterial_crossAndMiterCutsApp.ApplicationId,
                ToolId = toolCM12SD.ToolId
            });
            cut_laminatedWoodMaterial_crossAndMiterCutsApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = cut_laminatedWoodMaterial_crossAndMiterCutsApp.ApplicationId,
                ToolId = toolGCM12SD.ToolId
            });
            cut_laminatedWoodMaterial_crossAndMiterCutsApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = cut_laminatedWoodMaterial_crossAndMiterCutsApp.ApplicationId,
                ToolId = toolT4B.ToolId
            });
            cut_laminatedWoodMaterial_crossAndMiterCutsApp.ToolRelationships =
                cut_laminatedWoodMaterial_crossAndMiterCutsApp_Tools;

            var cut_laminatedWoodMaterial_crossAndMiterCutsApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            cut_laminatedWoodMaterial_crossAndMiterCutsApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = cut_laminatedWoodMaterial_crossAndMiterCutsApp.ApplicationId,
                AccessoryId = accessoryDCB1244.AccessoryId
            });
            cut_laminatedWoodMaterial_crossAndMiterCutsApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = cut_laminatedWoodMaterial_crossAndMiterCutsApp.ApplicationId,
                AccessoryId = accessoryDCB1280.AccessoryId
            });
            cut_laminatedWoodMaterial_crossAndMiterCutsApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = cut_laminatedWoodMaterial_crossAndMiterCutsApp.ApplicationId,
                AccessoryId = accessoryPS1260GP.AccessoryId
            });
            cut_laminatedWoodMaterial_crossAndMiterCutsApp.AccessoryRelationships =
                cut_laminatedWoodMaterial_crossAndMiterCutsApp_Accessories;

            var cut_plasticMaterial_straightCurveTightCurveScrollCutsApp_Tools =
                new List<ApplicationToolRelationship>();
            cut_plasticMaterial_straightCurveTightCurveScrollCutsApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = cut_plasticMaterial_straightCurveTightCurveScrollCutsApp.ApplicationId,
                ToolId = toolJS470E.ToolId
            });
            cut_plasticMaterial_straightCurveTightCurveScrollCutsApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = cut_plasticMaterial_straightCurveTightCurveScrollCutsApp.ApplicationId,
                ToolId = toolJS572EBL15.ToolId
            });
            cut_plasticMaterial_straightCurveTightCurveScrollCutsApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = cut_plasticMaterial_straightCurveTightCurveScrollCutsApp.ApplicationId,
                ToolId = toolJS572EL.ToolId
            });
            cut_plasticMaterial_straightCurveTightCurveScrollCutsApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = cut_plasticMaterial_straightCurveTightCurveScrollCutsApp.ApplicationId,
                ToolId = toolJSH180BL.ToolId
            });
            cut_plasticMaterial_straightCurveTightCurveScrollCutsApp.ToolRelationships =
                cut_plasticMaterial_straightCurveTightCurveScrollCutsApp_Tools;

            // 5: Drill and Drive Job
            var drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp.ApplicationId,
                ToolId = toolHTH181_01.ToolId
            });
            drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp.ApplicationId,
                ToolId = toolIWHT180_01.ToolId
            });
            drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp.ToolRelationships =
                drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp_Tools;

            var drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp.ApplicationId,
                    AccessoryId = accessory27275.AccessoryId
                });
            drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp.ApplicationId,
                    AccessoryId = accessory27281.AccessoryId
                });
            drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp.ApplicationId,
                    AccessoryId = accessory27286.AccessoryId
                });
            drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp.AccessoryRelationships =
                drillAndDrive_laminatedWoodMaterial_highTorqueDriveApp_Accessories;

            var drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp_Tools =
                new List<ApplicationToolRelationship>();
            drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = tool24618_01.ToolId
            });
            drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolIDH182_01.ToolId
            });
            drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolIWMH182_01.ToolId
            });
            drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolPS41_2A.ToolId
            });
            drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp.ToolRelationships =
                drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp_Tools;

            var drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp.ApplicationId,
                    AccessoryId = accessory24618_01.AccessoryId
                });
            drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp.ApplicationId,
                    AccessoryId = accessoryDSB1013.AccessoryId
                });
            drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp.ApplicationId,
                    AccessoryId = accessoryDSB5010.AccessoryId
                });
            drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp.ApplicationId,
                    AccessoryId = accessorySBID32.AccessoryId
                });
            drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp.AccessoryRelationships =
                drillAndDrive_laminatedWoodMaterial_mediumTorqueDriveApp_Accessories;

            var drillAndDrive_laminatedWoodMaterial_drillApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_laminatedWoodMaterial_drillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_laminatedWoodMaterial_drillApp.ApplicationId,
                ToolId = tool1033VSR.ToolId
            });
            drillAndDrive_laminatedWoodMaterial_drillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_laminatedWoodMaterial_drillApp.ApplicationId,
                ToolId = toolADS181BL.ToolId
            });
            drillAndDrive_laminatedWoodMaterial_drillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_laminatedWoodMaterial_drillApp.ApplicationId,
                ToolId = toolDDH181X_01L.ToolId
            });
            drillAndDrive_laminatedWoodMaterial_drillApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_laminatedWoodMaterial_drillApp.ApplicationId,
                ToolId = toolDDS182_02L.ToolId
            });
            drillAndDrive_laminatedWoodMaterial_drillApp.ToolRelationships =
                drillAndDrive_laminatedWoodMaterial_drillApp_Tools;

            var drillAndDrive_laminatedWoodMaterial_drillApp_Accessories = new List<ApplicationAccessoryRelationship>();
            drillAndDrive_laminatedWoodMaterial_drillApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_laminatedWoodMaterial_drillApp.ApplicationId,
                AccessoryId = accessoryDSB1013.AccessoryId
            });
            drillAndDrive_laminatedWoodMaterial_drillApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_laminatedWoodMaterial_drillApp.ApplicationId,
                AccessoryId = accessoryDSB5010.AccessoryId
            });
            drillAndDrive_laminatedWoodMaterial_drillApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_laminatedWoodMaterial_drillApp.ApplicationId,
                AccessoryId = accessoryFB016.AccessoryId
            });
            drillAndDrive_laminatedWoodMaterial_drillApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_laminatedWoodMaterial_drillApp.ApplicationId,
                AccessoryId = accessoryT4047.AccessoryId
            });
            drillAndDrive_laminatedWoodMaterial_drillApp.AccessoryRelationships =
                drillAndDrive_laminatedWoodMaterial_drillApp_Accessories;

            var drillAndDrive_plywoodMaterial_highTorqueDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_plywoodMaterial_highTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_highTorqueDriveApp.ApplicationId,
                ToolId = toolHTH181_01.ToolId
            });
            drillAndDrive_plywoodMaterial_highTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_highTorqueDriveApp.ApplicationId,
                ToolId = toolIWHT180_01.ToolId
            });
            drillAndDrive_plywoodMaterial_highTorqueDriveApp.ToolRelationships =
                drillAndDrive_plywoodMaterial_highTorqueDriveApp_Tools;

            var drillAndDrive_plywoodMaterial_highTorqueDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_plywoodMaterial_highTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_highTorqueDriveApp.ApplicationId,
                AccessoryId = accessory27275.AccessoryId
            });
            drillAndDrive_plywoodMaterial_highTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_highTorqueDriveApp.ApplicationId,
                AccessoryId = accessory27281.AccessoryId
            });
            drillAndDrive_plywoodMaterial_highTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_highTorqueDriveApp.ApplicationId,
                AccessoryId = accessory27286.AccessoryId
            });
            drillAndDrive_plywoodMaterial_highTorqueDriveApp.AccessoryRelationships =
                drillAndDrive_plywoodMaterial_highTorqueDriveApp_Accessories;

            var drillAndDrive_plywoodMaterial_mediumTorqueDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_plywoodMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = tool24618_01.ToolId
            });
            drillAndDrive_plywoodMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolIDH182_01.ToolId
            });
            drillAndDrive_plywoodMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolIWMH182_01.ToolId
            });
            drillAndDrive_plywoodMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolPS41_2A.ToolId
            });
            drillAndDrive_plywoodMaterial_mediumTorqueDriveApp.ToolRelationships =
                drillAndDrive_plywoodMaterial_mediumTorqueDriveApp_Tools;

            var drillAndDrive_plywoodMaterial_mediumTorqueDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_plywoodMaterial_mediumTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_mediumTorqueDriveApp.ApplicationId,
                AccessoryId = accessoryMP02.AccessoryId
            });
            drillAndDrive_plywoodMaterial_mediumTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_mediumTorqueDriveApp.ApplicationId,
                AccessoryId = accessoryMP03.AccessoryId
            });
            drillAndDrive_plywoodMaterial_mediumTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_mediumTorqueDriveApp.ApplicationId,
                AccessoryId = accessoryMP06.AccessoryId
            });
            drillAndDrive_plywoodMaterial_mediumTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_mediumTorqueDriveApp.ApplicationId,
                AccessoryId = accessoryMP500T.AccessoryId
            });
            drillAndDrive_plywoodMaterial_mediumTorqueDriveApp.AccessoryRelationships =
                drillAndDrive_plywoodMaterial_mediumTorqueDriveApp_Accessories;

            var drillAndDrive_plywoodMaterial_drillAndDriveApp_Tool = new List<ApplicationToolRelationship>();
            drillAndDrive_plywoodMaterial_drillAndDriveApp_Tool.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_drillAndDriveApp.ApplicationId,
                ToolId = tool1033VSR.ToolId
            });
            drillAndDrive_plywoodMaterial_drillAndDriveApp_Tool.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolADS181BL.ToolId
            });
            drillAndDrive_plywoodMaterial_drillAndDriveApp_Tool.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDH181X_01L.ToolId
            });
            drillAndDrive_plywoodMaterial_drillAndDriveApp_Tool.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDS182.ToolId
            });

            drillAndDrive_plywoodMaterial_mediumTorqueDriveApp.ToolRelationships =
                drillAndDrive_plywoodMaterial_drillAndDriveApp_Tool;

            var drillAndDrive_plywoodMaterial_drillAndDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_plywoodMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryDSB1013.AccessoryId
            });
            drillAndDrive_plywoodMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryDSB5010.AccessoryId
            });
            drillAndDrive_plywoodMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryHMD200.AccessoryId
            });
            drillAndDrive_plywoodMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_plywoodMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryT4047.AccessoryId
            });
            drillAndDrive_plywoodMaterial_mediumTorqueDriveApp.AccessoryRelationships =
                drillAndDrive_plywoodMaterial_drillAndDriveApp_Accessories;

            var drillAndDrive_chipBoardMaterial_highTorqueDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_chipBoardMaterial_highTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_highTorqueDriveApp.ApplicationId,
                ToolId = toolHTH181_01.ToolId
            });
            drillAndDrive_chipBoardMaterial_highTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_highTorqueDriveApp.ApplicationId,
                ToolId = toolIWHT180_01.ToolId
            });
            drillAndDrive_chipBoardMaterial_highTorqueDriveApp.ToolRelationships =
                drillAndDrive_chipBoardMaterial_highTorqueDriveApp_Tools;

            var drillAndDrive_chipBoardMaterial_highTorqueDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_chipBoardMaterial_highTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_highTorqueDriveApp.ApplicationId,
                AccessoryId = accessory27275.AccessoryId
            });
            drillAndDrive_chipBoardMaterial_highTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_highTorqueDriveApp.ApplicationId,
                AccessoryId = accessory27281.AccessoryId
            });
            drillAndDrive_chipBoardMaterial_highTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_highTorqueDriveApp.ApplicationId,
                AccessoryId = accessory27286.AccessoryId
            });
            drillAndDrive_chipBoardMaterial_highTorqueDriveApp.AccessoryRelationships =
                drillAndDrive_chipBoardMaterial_highTorqueDriveApp_Accessories;

            var drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = tool24618_01.ToolId
            });
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolIDH182_01.ToolId
            });
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolIWMH182_01.ToolId
            });
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolPS41_2A.ToolId
            });
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.ToolRelationships =
                drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Tools;

            var drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.ApplicationId,
                AccessoryId = accessoryDSB1013.AccessoryId
            });
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.ApplicationId,
                AccessoryId = accessoryDSB5010.AccessoryId
            });
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.ApplicationId,
                AccessoryId = accessorySBID32.AccessoryId
            });
            drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp.AccessoryRelationships =
                drillAndDrive_chipBoardMaterial_mediumTorqueDriveApp_Accessories;

            var drillAndDrive_chipBoardMaterial_drillAndDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_chipBoardMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_drillAndDriveApp.ApplicationId,
                ToolId = tool1033VSR.ToolId
            });
            drillAndDrive_chipBoardMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolADS181BL.ToolId
            });
            drillAndDrive_chipBoardMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDH181X_01L.ToolId
            });
            drillAndDrive_chipBoardMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDS182_02L.ToolId
            });
            drillAndDrive_chipBoardMaterial_drillAndDriveApp.ToolRelationships =
                drillAndDrive_chipBoardMaterial_drillAndDriveApp_Tools;

            var drillAndDrive_chipBoardMaterial_drillAndDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_chipBoardMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryDSB1013.AccessoryId
            });
            drillAndDrive_chipBoardMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryDSB5010.AccessoryId
            });
            drillAndDrive_chipBoardMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryHMD200.AccessoryId
            });
            drillAndDrive_chipBoardMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_chipBoardMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryT4047.AccessoryId
            });
            drillAndDrive_chipBoardMaterial_drillAndDriveApp.AccessoryRelationships =
                drillAndDrive_chipBoardMaterial_drillAndDriveApp_Accessories;

            var drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp.ApplicationId,
                ToolId = toolHTH181_01.ToolId
            });
            drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp.ApplicationId,
                ToolId = tool1033VSR.ToolId
            });
            drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp.ToolRelationships =
                drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp_Tools;

            var drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp.ApplicationId,
                AccessoryId = accessory27275.AccessoryId
            });
            drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp.ApplicationId,
                AccessoryId = accessory27281.AccessoryId
            });
            drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp.ApplicationId,
                AccessoryId = accessory27286.AccessoryId
            });
            drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp.AccessoryRelationships =
                drillAndDrive_solidSurfaceMaterial_highTorqueDriveApp_Accessories;

            var drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = tool24618_01.ToolId
            });
            drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolIDH182_01.ToolId
            });
            drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolIWMH182_01.ToolId
            });
            drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolPS41_2A.ToolId
            });
            drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp.ToolRelationships =
                drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp_Tools;

            var drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp.ApplicationId,
                    AccessoryId = accessoryITP2102.AccessoryId
                });
            drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp.ApplicationId,
                    AccessoryId = accessoryITP2205.AccessoryId
                });
            drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp.ApplicationId,
                    AccessoryId = accessorySBID32.AccessoryId
                });
            drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp.ApplicationId,
                    AccessoryId = accessorySBID39.AccessoryId
                });
            drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp.AccessoryRelationships =
                drillAndDrive_solidSurfaceMaterial_mediumTorqueDriveApp_Accessories;

            var drillAndDrive_solidSurfaceMaterial_drillAndDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_solidSurfaceMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_drillAndDriveApp.ApplicationId,
                ToolId = tool1033VSR.ToolId
            });
            drillAndDrive_solidSurfaceMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolADS181BL.ToolId
            });
            drillAndDrive_solidSurfaceMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDH181X_01L.ToolId
            });
            drillAndDrive_solidSurfaceMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDS182_02L.ToolId
            });
            drillAndDrive_solidSurfaceMaterial_drillAndDriveApp.ToolRelationships =
                drillAndDrive_solidSurfaceMaterial_drillAndDriveApp_Tools;

            var drillAndDrive_solidSurfaceMaterial_drillAndDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_solidSurfaceMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryFB016.AccessoryId
            });
            drillAndDrive_solidSurfaceMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryP2115TCB.AccessoryId
            });
            drillAndDrive_solidSurfaceMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryP2R22205.AccessoryId
            });
            drillAndDrive_solidSurfaceMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_solidSurfaceMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryT4047.AccessoryId
            });
            drillAndDrive_solidSurfaceMaterial_drillAndDriveApp.AccessoryRelationships =
                drillAndDrive_solidSurfaceMaterial_drillAndDriveApp_Accessories;

            var drillAndDrive_woodCompositeMaterial_highTorqueDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_woodCompositeMaterial_highTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_woodCompositeMaterial_highTorqueDriveApp.ApplicationId,
                ToolId = toolHTH181_01.ToolId
            });
            drillAndDrive_woodCompositeMaterial_highTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_woodCompositeMaterial_highTorqueDriveApp.ApplicationId,
                ToolId = toolIWHT180_01.ToolId
            });
            drillAndDrive_woodCompositeMaterial_highTorqueDriveApp.ToolRelationships =
                drillAndDrive_woodCompositeMaterial_highTorqueDriveApp_Tools;

            var drillAndDrive_woodCompositeMaterial_highTorqueDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_woodCompositeMaterial_highTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_woodCompositeMaterial_highTorqueDriveApp.ApplicationId,
                    AccessoryId = accessory27275.AccessoryId
                });
            drillAndDrive_woodCompositeMaterial_highTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_woodCompositeMaterial_highTorqueDriveApp.ApplicationId,
                    AccessoryId = accessory27281.AccessoryId
                });
            drillAndDrive_woodCompositeMaterial_highTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_woodCompositeMaterial_highTorqueDriveApp.ApplicationId,
                    AccessoryId = accessory27286.AccessoryId
                });
            drillAndDrive_woodCompositeMaterial_highTorqueDriveApp.AccessoryRelationships =
                drillAndDrive_woodCompositeMaterial_highTorqueDriveApp_Accessories;

            var drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp_Tools =
                new List<ApplicationToolRelationship>();
            drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = tool24618_01.ToolId
            });
            drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolIDH182_01.ToolId
            });
            drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolIWMH182_01.ToolId
            });
            drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolPS41_2A.ToolId
            });
            drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp.ToolRelationships =
                drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp_Tools;

            var drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp.ApplicationId,
                    AccessoryId = accessoryIMD5007.AccessoryId
                });
            drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp.ApplicationId,
                    AccessoryId = accessorySBID32.AccessoryId
                });
            drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp.AccessoryRelationships =
                drillAndDrive_woodCompositeMaterial_mediumTorqueDriveApp_Accessories;

            var drillAndDrive_woodCompositeMaterial_drillAndDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_woodCompositeMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_woodCompositeMaterial_drillAndDriveApp.ApplicationId,
                ToolId = tool1033VSR.ToolId
            });
            drillAndDrive_woodCompositeMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_woodCompositeMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolADS181BL.ToolId
            });
            drillAndDrive_woodCompositeMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_woodCompositeMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDH181X_01L.ToolId
            });
            drillAndDrive_woodCompositeMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_woodCompositeMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDS182_02L.ToolId
            });
            drillAndDrive_woodCompositeMaterial_drillAndDriveApp.ToolRelationships =
                drillAndDrive_woodCompositeMaterial_drillAndDriveApp_Tools;

            var drillAndDrive_woodCompositeMaterial_drillAndDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_woodCompositeMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_woodCompositeMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryDSB1013.AccessoryId
            });
            drillAndDrive_woodCompositeMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_woodCompositeMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryFB016.AccessoryId
            });
            drillAndDrive_woodCompositeMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_woodCompositeMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryT4047.AccessoryId
            });
            drillAndDrive_woodCompositeMaterial_drillAndDriveApp.AccessoryRelationships =
                drillAndDrive_woodCompositeMaterial_drillAndDriveApp_Accessories;

            var drillAndDrive_metalMaterial_highTorqueDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_metalMaterial_highTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_highTorqueDriveApp.ApplicationId,
                ToolId = toolHTH181_01.ToolId
            });
            drillAndDrive_metalMaterial_highTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_highTorqueDriveApp.ApplicationId,
                ToolId = toolIWHT180_01.ToolId
            });
            drillAndDrive_metalMaterial_highTorqueDriveApp.ToolRelationships =
                drillAndDrive_metalMaterial_highTorqueDriveApp_Tools;

            var drillAndDrive_metalMaterial_highTorqueDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_metalMaterial_highTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_highTorqueDriveApp.ApplicationId,
                AccessoryId = accessory27275.AccessoryId
            });
            drillAndDrive_metalMaterial_highTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_highTorqueDriveApp.ApplicationId,
                AccessoryId = accessory27281.AccessoryId
            });
            drillAndDrive_metalMaterial_highTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_highTorqueDriveApp.ApplicationId,
                AccessoryId = accessory27286.AccessoryId
            });
            drillAndDrive_metalMaterial_highTorqueDriveApp.AccessoryRelationships =
                drillAndDrive_metalMaterial_highTorqueDriveApp_Accessories;

            var drillAndDrive_metalMaterial_mediumTorqueDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_metalMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = tool24618_01.ToolId
            });
            drillAndDrive_metalMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolIDH182_01.ToolId
            });
            drillAndDrive_metalMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolIWMH182_01.ToolId
            });
            drillAndDrive_metalMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolPS41_2A.ToolId
            });
            drillAndDrive_metalMaterial_mediumTorqueDriveApp.ToolRelationships =
                drillAndDrive_metalMaterial_mediumTorqueDriveApp_Tools;

            var drillAndDrive_metalMaterial_mediumTorqueDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_metalMaterial_mediumTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_mediumTorqueDriveApp.ApplicationId,
                AccessoryId = accessoryIMD5007.AccessoryId
            });
            drillAndDrive_metalMaterial_mediumTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_mediumTorqueDriveApp.ApplicationId,
                AccessoryId = accessorySBID32.AccessoryId
            });
            drillAndDrive_metalMaterial_mediumTorqueDriveApp.AccessoryRelationships =
                drillAndDrive_metalMaterial_mediumTorqueDriveApp_Accessories;

            var drillAndDrive_metalMaterial_drillAndDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_metalMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_drillAndDriveApp.ApplicationId,
                ToolId = tool1033VSR.ToolId
            });
            drillAndDrive_metalMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolADS181BL.ToolId
            });
            drillAndDrive_metalMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDH181X_01L.ToolId
            });
            drillAndDrive_metalMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDS182_02L.ToolId
            });
            drillAndDrive_metalMaterial_drillAndDriveApp.ToolRelationships =
                drillAndDrive_metalMaterial_drillAndDriveApp_Tools;

            var drillAndDrive_metalMaterial_drillAndDriveApp_Accessories = new List<ApplicationAccessoryRelationship>();
            drillAndDrive_metalMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryBL2143.AccessoryId
            });
            drillAndDrive_metalMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_metalMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryTI2143.AccessoryId
            });
            drillAndDrive_metalMaterial_drillAndDriveApp.AccessoryRelationships =
                drillAndDrive_metalMaterial_drillAndDriveApp_Accessories;

            var drillAndDrive_fiberCementMaterial_highTorqueDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_fiberCementMaterial_highTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_highTorqueDriveApp.ApplicationId,
                ToolId = toolHTH181_01.ToolId
            });
            drillAndDrive_fiberCementMaterial_highTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_highTorqueDriveApp.ApplicationId,
                ToolId = toolIWHT180_01.ToolId
            });
            drillAndDrive_fiberCementMaterial_highTorqueDriveApp.ToolRelationships =
                drillAndDrive_fiberCementMaterial_highTorqueDriveApp_Tools;

            var drillAndDrive_fiberCementMaterial_highTorqueDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_fiberCementMaterial_highTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_highTorqueDriveApp.ApplicationId,
                AccessoryId = accessory27275.AccessoryId
            });
            drillAndDrive_fiberCementMaterial_highTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_highTorqueDriveApp.ApplicationId,
                AccessoryId = accessory27281.AccessoryId
            });
            drillAndDrive_fiberCementMaterial_highTorqueDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_highTorqueDriveApp.ApplicationId,
                AccessoryId = accessory27286.AccessoryId
            });
            drillAndDrive_fiberCementMaterial_highTorqueDriveApp.AccessoryRelationships =
                drillAndDrive_fiberCementMaterial_highTorqueDriveApp_Accessories;

            var drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = tool24618_01.ToolId
            });
            drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolIDH182_01.ToolId
            });
            drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolIWMH182_01.ToolId
            });
            drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp.ApplicationId,
                ToolId = toolPS41_2A.ToolId
            });
            drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp.AccessoryRelationships =
                new List<ApplicationAccessoryRelationship>();

            var drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp.ApplicationId,
                    AccessoryId = accessorySBID21.AccessoryId
                });
            drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp.ApplicationId,
                    AccessoryId = accessorySBID32.AccessoryId
                });
            drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp.ApplicationId,
                    AccessoryId = accessorySBID39.AccessoryId
                });
            drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp.AccessoryRelationships =
                drillAndDrive_fiberCementMaterial_mediumTorqueDriveApp_Accessories;

            var drillAndDrive_fiberCementMaterial_drillAndDriveApp_Tools = new List<ApplicationToolRelationship>();
            drillAndDrive_fiberCementMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_drillAndDriveApp.ApplicationId,
                ToolId = tool1033VSR.ToolId
            });
            drillAndDrive_fiberCementMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolADS181BL.ToolId
            });
            drillAndDrive_fiberCementMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDH181X_01L.ToolId
            });
            drillAndDrive_fiberCementMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDS182_02L.ToolId
            });
            drillAndDrive_fiberCementMaterial_drillAndDriveApp.ToolRelationships =
                drillAndDrive_fiberCementMaterial_drillAndDriveApp_Tools;

            var drillAndDrive_fiberCementMaterial_drillAndDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            drillAndDrive_fiberCementMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryHCBG04T.AccessoryId
            });
            drillAndDrive_fiberCementMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryHCBG06T.AccessoryId
            });
            drillAndDrive_fiberCementMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryHCBG501T.AccessoryId
            });
            drillAndDrive_fiberCementMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = drillAndDrive_fiberCementMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryT4047.AccessoryId
            });
            drillAndDrive_fiberCementMaterial_drillAndDriveApp.AccessoryRelationships =
                drillAndDrive_fiberCementMaterial_drillAndDriveApp_Accessories;

            // 6: Fasten Job
            var fastenJob_laminatedWoodMaterial_hammerDrillAndRotateApp_tools = new List<ApplicationToolRelationship>();
            fastenJob_laminatedWoodMaterial_hammerDrillAndRotateApp_tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_laminatedWoodMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            fastenJob_laminatedWoodMaterial_hammerDrillAndRotateApp_tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_laminatedWoodMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            fastenJob_laminatedWoodMaterial_hammerDrillAndRotateApp_tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_laminatedWoodMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            fastenJob_laminatedWoodMaterial_hammerDrillAndRotateApp.ToolRelationships =
                fastenJob_laminatedWoodMaterial_hammerDrillAndRotateApp_tools;

            var fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp_tools =
                new List<ApplicationToolRelationship>();
            fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp_tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolHTH181_01.ToolId
            });
            fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp_tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIWHT180_01.ToolId
            });
            fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp.ToolRelationships =
                fastenJob_laminatedWoodMaterial_hammerDrillAndRotateApp_tools;

            var fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp_accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp_accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessory27275.AccessoryId
                });
            fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp_accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessory27281.AccessoryId
                });
            fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp_accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessory27286.AccessoryId
                });
            fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp.AccessoryRelationships =
                fastenJob_laminatedWoodMaterial_highTorqueDriveAndFastenApp_accessories;

            var fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp_tools =
                new List<ApplicationToolRelationship>();
            fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp_tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = tool24618_01.ToolId
            });
            fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp_tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIDH182_01.ToolId
            });
            fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp_tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIWMH182_01.ToolId
            });
            fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp_tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolPS41_2A.ToolId
            });
            fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp.ToolRelationships =
                fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp_tools;

            var fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp_accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp_accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessoryIT2490.AccessoryId
                });
            fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp_accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessoryITP2205.AccessoryId
                });
            fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp_accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessorySBID32.AccessoryId
                });
            fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp.AccessoryRelationships =
                fastenJob_laminatedWoodMaterial_mediumTorqueDriveAndFastenApp_accessories;

            var fastenJob_laminatedWoodMaterial_drillAndDriveApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_laminatedWoodMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_laminatedWoodMaterial_drillAndDriveApp.ApplicationId,
                ToolId = tool1033VSR.ToolId
            });
            fastenJob_laminatedWoodMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_laminatedWoodMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolADS181BL.ToolId
            });
            fastenJob_laminatedWoodMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_laminatedWoodMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDH181X_01L.ToolId
            });
            fastenJob_laminatedWoodMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_laminatedWoodMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDS182_02L.ToolId
            });
            fastenJob_laminatedWoodMaterial_drillAndDriveApp.ToolRelationships =
                fastenJob_laminatedWoodMaterial_drillAndDriveApp_Tools;

            var fastenJob_laminatedWoodMaterial_drillAndDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_laminatedWoodMaterial_drillAndDriveApp.AccessoryRelationships =
                fastenJob_laminatedWoodMaterial_drillAndDriveApp_Accessories;

            var fastenJob_plywoodMaterial_hammerDrillAndRotateApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_plywoodMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_plywoodMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            fastenJob_plywoodMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_plywoodMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            fastenJob_plywoodMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_plywoodMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            fastenJob_plywoodMaterial_hammerDrillAndRotateApp.ToolRelationships =
                fastenJob_plywoodMaterial_hammerDrillAndRotateApp_Tools;

            var fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolHTH181_01.ToolId
            });
            fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIWHT180_01.ToolId
            });
            fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp.ToolRelationships =
                fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp_Tools;

            var fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                AccessoryId = accessory27275.AccessoryId
            });
            fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                AccessoryId = accessory27281.AccessoryId
            });
            fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                AccessoryId = accessory27286.AccessoryId
            });
            fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp.AccessoryRelationships =
                fastenJob_plywoodMaterial_highTorqueDriveAndFastenApp_Accessories;

            var fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = tool24618_01.ToolId
            });
            fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIDH182_01.ToolId
            });
            fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIWMH182_01.ToolId
            });
            fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp.ToolRelationships =
                fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp_Tools;

            var fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessoryMP02.AccessoryId
                });
            fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessoryMP03.AccessoryId
                });
            fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessoryMP06.AccessoryId
                });
            fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessoryMP500T.AccessoryId
                });
            fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp.AccessoryRelationships =
                fastenJob_plywoodMaterial_mediumTorqueDriveAndFastenApp_Accessories;

            var fastenJob_plywoodMaterial_drillAndDriveApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_plywoodMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_plywoodMaterial_drillAndDriveApp.ApplicationId,
                ToolId = tool24618_01.ToolId
            });
            fastenJob_plywoodMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_plywoodMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolIDH182_01.ToolId
            });
            fastenJob_plywoodMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_plywoodMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolIWMH182_01.ToolId
            });
            fastenJob_plywoodMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_plywoodMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolPS41_2A.ToolId
            });
            fastenJob_plywoodMaterial_drillAndDriveApp.ToolRelationships =
                fastenJob_plywoodMaterial_drillAndDriveApp_Tools;

            var fastenJob_chipBoardMaterial_hammerDrillAndRotateApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_chipBoardMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_chipBoardMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            fastenJob_chipBoardMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_chipBoardMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            fastenJob_chipBoardMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_chipBoardMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            fastenJob_chipBoardMaterial_hammerDrillAndRotateApp.ToolRelationships =
                fastenJob_chipBoardMaterial_hammerDrillAndRotateApp_Tools;

            var fastenJob_chipBoardMaterial_hammerDrillAndRotateApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_chipBoardMaterial_hammerDrillAndRotateApp.AccessoryRelationships =
                fastenJob_chipBoardMaterial_hammerDrillAndRotateApp_Accessories;

            var fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolHTH181_01.ToolId
            });
            fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIWHT180_01.ToolId
            });
            fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp.ToolRelationships =
                fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp_Tools;

            var fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessory27275.AccessoryId
                });
            fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessory27281.AccessoryId
                });
            fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_chipBoardMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessory27286.AccessoryId
                });

            var fastenJob_chipBoardMaterial_mediumTorqueDriveAndFastenApp_Tools =
                new List<ApplicationToolRelationship>();
            fastenJob_chipBoardMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_chipBoardMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = tool24618_01.ToolId
            });
            fastenJob_chipBoardMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_chipBoardMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIDH182_01.ToolId
            });
            fastenJob_chipBoardMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_chipBoardMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIWMH182_01.ToolId
            });
            fastenJob_chipBoardMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_chipBoardMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolPS41_2A.ToolId
            });
            fastenJob_chipBoardMaterial_mediumTorqueDriveAndFastenApp.ToolRelationships =
                fastenJob_chipBoardMaterial_mediumTorqueDriveAndFastenApp_Tools;

            var fastenJob_chipBoardMaterial_drillAndDriveApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_chipBoardMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_chipBoardMaterial_drillAndDriveApp.ApplicationId,
                ToolId = tool1033VSR.ToolId
            });
            fastenJob_chipBoardMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_chipBoardMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolADS181BL.ToolId
            });
            fastenJob_chipBoardMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_chipBoardMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDH181X_01L.ToolId
            });
            fastenJob_chipBoardMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_chipBoardMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDS182.ToolId
            });

            var fastenJob_solidSurfaceMaterial_hammerDrillAndRotateApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_solidSurfaceMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_solidSurfaceMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            fastenJob_solidSurfaceMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_solidSurfaceMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            fastenJob_solidSurfaceMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_solidSurfaceMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            fastenJob_solidSurfaceMaterial_hammerDrillAndRotateApp.ToolRelationships =
                fastenJob_solidSurfaceMaterial_hammerDrillAndRotateApp_Tools;

            var fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp_Tools =
                new List<ApplicationToolRelationship>();
            fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolHTH181_01.ToolId
            });
            fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIWHT180_01.ToolId
            });
            fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp.ToolRelationships =
                fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp_Tools;

            var fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessory27275.AccessoryId
                });
            fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessory27281.AccessoryId
                });
            fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessory27286.AccessoryId
                });
            fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp.AccessoryRelationships =
                fastenJob_solidSurfaceMaterial_highTorqueDriveAndFastenApp_Accessories;

            var fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp_Tools =
                new List<ApplicationToolRelationship>();
            fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = tool24618_01.ToolId
            });
            fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIDH182_01.ToolId
            });
            fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIWMH182_01.ToolId
            });
            fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolPS41_2A.ToolId
            });
            fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp.ToolRelationships =
                fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp_Tools;

            var fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessoryIT2490.AccessoryId
                });
            fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessoryITP2102.AccessoryId
                });
            fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessorySBID32.AccessoryId
                });
            fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp.AccessoryRelationships =
                fastenJob_solidSurfaceMaterial_mediumTorqueDriveAndFastenApp_Accessories;

            var fastenJob_solidSurfaceMaterial_drillAndDriveApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_solidSurfaceMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_solidSurfaceMaterial_drillAndDriveApp.ApplicationId,
                ToolId = tool1033VSR.ToolId
            });
            fastenJob_solidSurfaceMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_solidSurfaceMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolADS181BL.ToolId
            });
            fastenJob_solidSurfaceMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_solidSurfaceMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDH181X_01L.ToolId
            });
            fastenJob_solidSurfaceMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_solidSurfaceMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDS182.ToolId
            });
            fastenJob_solidSurfaceMaterial_drillAndDriveApp.ToolRelationships =
                fastenJob_solidSurfaceMaterial_drillAndDriveApp_Tools;

            var fastenJob_solidSurfaceMaterial_drillAndDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_solidSurfaceMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_solidSurfaceMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryP2115TCB.AccessoryId
            });
            fastenJob_solidSurfaceMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_solidSurfaceMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryP2R22205.AccessoryId
            });
            fastenJob_solidSurfaceMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_solidSurfaceMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryT4047.AccessoryId
            });
            fastenJob_solidSurfaceMaterial_drillAndDriveApp.AccessoryRelationships =
                fastenJob_solidSurfaceMaterial_drillAndDriveApp_Accessories;

            var fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp_Tools =
                new List<ApplicationToolRelationship>();
            fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp.ToolRelationships =
                fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp_Tools;

            var fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp.AccessoryRelationships =
                fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp_Accessories;

            var fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp_Tools =
                new List<ApplicationToolRelationship>();
            fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolHTH181_01.ToolId
            });
            fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_woodCompositesMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolIWHT180_01.ToolId
            });
            fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp.ToolRelationships =
                fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp_Tools;

            var fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessory27275.AccessoryId
                });
            fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessory27281.AccessoryId
                });
            fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessory27286.AccessoryId
                });
            fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp.AccessoryRelationships =
                fastenJob_woodCompositesMaterial_highTorqueDriveAndFastenApp_Accessories;

            var fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp_Tools =
                new List<ApplicationToolRelationship>();
            fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = tool24618_01.ToolId
            });
            fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIDH182_01.ToolId
            });
            fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIWMH182_01.ToolId
            });
            fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolPS41_2A.ToolId
            });
            fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp.ToolRelationships =
                fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp_Tools;

            var fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessorySBID21.AccessoryId
                });
            fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessorySBID32.AccessoryId
                });
            fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessorySBID39.AccessoryId
                });
            fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp.AccessoryRelationships =
                fastenJob_woodCompositesMaterial_mediumTorqueDriveAndFastenApp_Accessories;

            var fastenJob_woodCompositesMaterial_drillAndDriveApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_woodCompositesMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_woodCompositesMaterial_drillAndDriveApp.ApplicationId,
                ToolId = tool1033VSR.ToolId
            });
            fastenJob_woodCompositesMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_woodCompositesMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolADS181BL.ToolId
            });
            fastenJob_woodCompositesMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_woodCompositesMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDH181X_01L.ToolId
            });
            fastenJob_woodCompositesMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_woodCompositesMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDS182.ToolId
            });
            fastenJob_woodCompositesMaterial_drillAndDriveApp.ToolRelationships =
                fastenJob_woodCompositesMaterial_drillAndDriveApp_Tools;

            var fastenJob_woodCompositesMaterial_drillAndDriveApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_woodCompositesMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_woodCompositesMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryDSB1009.AccessoryId
            });
            fastenJob_woodCompositesMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_woodCompositesMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryDSB5006.AccessoryId
            });
            fastenJob_woodCompositesMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_woodCompositesMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryDSB5010.AccessoryId
            });
            fastenJob_woodCompositesMaterial_drillAndDriveApp.AccessoryRelationships =
                fastenJob_woodCompositesMaterial_drillAndDriveApp_Accessories;

            var fastenJob_metalMaterial_hammerDrillAndRotateApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_metalMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_metalMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            fastenJob_metalMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_metalMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            fastenJob_metalMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_metalMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            fastenJob_metalMaterial_hammerDrillAndRotateApp.ToolRelationships =
                fastenJob_metalMaterial_hammerDrillAndRotateApp_Tools;

            var fastenJob_metalMaterial_hammerDrillAndRotateApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_metalMaterial_hammerDrillAndRotateApp.AccessoryRelationships =
                fastenJob_metalMaterial_hammerDrillAndRotateApp_Accessories;

            var fastenJob_metalMaterial_highTorqueDriveAndFastenApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_metalMaterial_highTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_metalMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolHTH181_01.ToolId
            });
            fastenJob_metalMaterial_highTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_metalMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIWHT180_01.ToolId
            });
            fastenJob_metalMaterial_highTorqueDriveAndFastenApp.ToolRelationships =
                fastenJob_metalMaterial_highTorqueDriveAndFastenApp_Tools;

            var fastenJob_metalMaterial_highTorqueDriveAndFastenApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_metalMaterial_highTorqueDriveAndFastenApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_metalMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                AccessoryId = accessory27275.AccessoryId
            });
            fastenJob_metalMaterial_highTorqueDriveAndFastenApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_metalMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                AccessoryId = accessory27281.AccessoryId
            });
            fastenJob_metalMaterial_highTorqueDriveAndFastenApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_metalMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                AccessoryId = accessory27286.AccessoryId
            });
            fastenJob_metalMaterial_highTorqueDriveAndFastenApp.AccessoryRelationships =
                fastenJob_metalMaterial_highTorqueDriveAndFastenApp_Accessories;

            var fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = tool24618_01.ToolId
            });
            fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIDH182_01.ToolId
            });
            fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIWMH182_01.ToolId
            });
            fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolPS41_2A.ToolId
            });
            fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp.ToolRelationships =
                fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp_Tools;

            var fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp.AccessoryRelationships =
                fastenJob_metalMaterial_mediumTorqueDriveAndFastenApp_Accessories;

            var fastenJob_metalMaterial_drillAndDriveApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_metalMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_metalMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolADS181BL.ToolId
            });
            fastenJob_metalMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_metalMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDH181X_01L.ToolId
            });
            fastenJob_metalMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_metalMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDS182_02L.ToolId
            });
            fastenJob_metalMaterial_drillAndDriveApp.ToolRelationships = fastenJob_metalMaterial_drillAndDriveApp_Tools;

            var fastenJob_metalMaterial_drillAndDriveApp_Accessories = new List<ApplicationAccessoryRelationship>();
            fastenJob_metalMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_metalMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryBL2143.AccessoryId
            });
            fastenJob_metalMaterial_drillAndDriveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_metalMaterial_drillAndDriveApp.ApplicationId,
                AccessoryId = accessoryTI21.AccessoryId
            });
            fastenJob_metalMaterial_drillAndDriveApp.AccessoryRelationships =
                fastenJob_metalMaterial_drillAndDriveApp_Accessories;

            var fastenJob_metalMaterial_driveApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_metalMaterial_driveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_metalMaterial_driveApp.ApplicationId,
                ToolId = toolSG250.ToolId
            });
            fastenJob_metalMaterial_driveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_metalMaterial_driveApp.ApplicationId,
                ToolId = toolSG450.ToolId
            });
            fastenJob_metalMaterial_driveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_metalMaterial_driveApp.ApplicationId,
                ToolId = toolSGH182BL.ToolId
            });
            fastenJob_metalMaterial_driveApp.ToolRelationships = fastenJob_metalMaterial_driveApp_Tools;

            var fastenJob_metalMaterial_driveApp_Accessories = new List<ApplicationAccessoryRelationship>();
            fastenJob_metalMaterial_driveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_metalMaterial_driveApp.ApplicationId,
                AccessoryId = accessoryD60498.AccessoryId
            });
            fastenJob_metalMaterial_driveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_metalMaterial_driveApp.ApplicationId,
                AccessoryId = accessoryDWS2.AccessoryId
            });
            fastenJob_metalMaterial_driveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_metalMaterial_driveApp.ApplicationId,
                AccessoryId = accessoryDWS60497.AccessoryId
            });
            fastenJob_metalMaterial_driveApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = fastenJob_metalMaterial_driveApp.ApplicationId,
                AccessoryId = accessoryP2D105.AccessoryId
            });
            fastenJob_metalMaterial_driveApp.AccessoryRelationships = fastenJob_metalMaterial_driveApp_Accessories;

            var fastenJob_fiberCementMaterial_hammerDrillAndRotateApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_fiberCementMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_fiberCementMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolHDH181X_01L.ToolId
            });
            fastenJob_fiberCementMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_fiberCementMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolHDS182_02L.ToolId
            });
            fastenJob_fiberCementMaterial_hammerDrillAndRotateApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_fiberCementMaterial_hammerDrillAndRotateApp.ApplicationId,
                ToolId = toolPS130_2A.ToolId
            });
            fastenJob_fiberCementMaterial_hammerDrillAndRotateApp.ToolRelationships =
                fastenJob_fiberCementMaterial_hammerDrillAndRotateApp_Tools;

            var fastenJob_fiberCementMaterial_hammerDrillAndRotateApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_fiberCementMaterial_hammerDrillAndRotateApp.AccessoryRelationships =
                fastenJob_fiberCementMaterial_hammerDrillAndRotateApp_Accessories;

            var fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp_Tools =
                new List<ApplicationToolRelationship>();
            fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolHTH181_01.ToolId
            });
            fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIWHT180_01.ToolId
            });
            fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp.ToolRelationships =
                fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp_Tools;

            var fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessory27275.AccessoryId
                });
            fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessory27281.AccessoryId
                });
            fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessory27286.AccessoryId
                });
            fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp.AccessoryRelationships =
                fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp_Accessories;

            var fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp_Tools =
                new List<ApplicationToolRelationship>();
            fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                ToolId = tool24618_01.ToolId
            });
            fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIDH182_01.ToolId
            });
            fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolIWMH182_01.ToolId
            });
            fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_fiberCementMaterial_highTorqueDriveAndFastenApp.ApplicationId,
                ToolId = toolPS41_2A.ToolId
            });
            fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp.ToolRelationships =
                fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp_Tools;

            var fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessoryMP02.AccessoryId
                });
            fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessoryMP03.AccessoryId
                });
            fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessoryMP06.AccessoryId
                });
            fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp.ApplicationId,
                    AccessoryId = accessoryMP500T.AccessoryId
                });
            fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp.AccessoryRelationships =
                fastenJob_fiberCementMaterial_mediumTorqueDriveAndFastenApp_Accessories;

            var fastenJob_fiberCementMaterial_drillAndDriveApp_Tools = new List<ApplicationToolRelationship>();
            fastenJob_fiberCementMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_fiberCementMaterial_drillAndDriveApp.ApplicationId,
                ToolId = tool1033VSR.ToolId
            });
            fastenJob_fiberCementMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_fiberCementMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolADS181BL.ToolId
            });
            fastenJob_fiberCementMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_fiberCementMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDH181X_01L.ToolId
            });
            fastenJob_fiberCementMaterial_drillAndDriveApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = fastenJob_fiberCementMaterial_drillAndDriveApp.ApplicationId,
                ToolId = toolDDS182_02L.ToolId
            });
            fastenJob_fiberCementMaterial_drillAndDriveApp.ToolRelationships =
                fastenJob_fiberCementMaterial_drillAndDriveApp_Tools;

            var routeJob_laminatesMaterial_surfaceFormingApp_Tools = new List<ApplicationToolRelationship>();
            routeJob_laminatesMaterial_surfaceFormingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_laminatesMaterial_surfaceFormingApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_laminatesMaterial_surfaceFormingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_laminatesMaterial_surfaceFormingApp.ApplicationId,
                ToolId = toolMRC23EVSK45.ToolId
            });
            routeJob_laminatesMaterial_surfaceFormingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_laminatesMaterial_surfaceFormingApp.ApplicationId,
                ToolId = toolPR20EVSPK.ToolId
            });
            routeJob_laminatesMaterial_surfaceFormingApp.ToolRelationships =
                routeJob_laminatesMaterial_surfaceFormingApp_Tools;

            var routeJob_laminatesMaterial_surfaceFormingApp_Accessories = new List<ApplicationAccessoryRelationship>();
            routeJob_laminatesMaterial_surfaceFormingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_laminatesMaterial_surfaceFormingApp.ApplicationId,
                AccessoryId = accessory85445M.AccessoryId
            });
            routeJob_laminatesMaterial_surfaceFormingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_laminatesMaterial_surfaceFormingApp.ApplicationId,
                AccessoryId = accessory85446M.AccessoryId
            });
            routeJob_laminatesMaterial_surfaceFormingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_laminatesMaterial_surfaceFormingApp.ApplicationId,
                AccessoryId = accessory85448M.AccessoryId
            });
            routeJob_laminatesMaterial_surfaceFormingApp.AccessoryRelationships =
                routeJob_laminatesMaterial_surfaceFormingApp_Accessories;

            var routeJob_laminatesMaterial_straightRoutingAndMorticingApp_Tools =
                new List<ApplicationToolRelationship>();
            routeJob_laminatesMaterial_straightRoutingAndMorticingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_laminatesMaterial_straightRoutingAndMorticingApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_laminatesMaterial_straightRoutingAndMorticingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_laminatesMaterial_straightRoutingAndMorticingApp.ApplicationId,
                ToolId = toolMRC23EVSK.ToolId
            });
            routeJob_laminatesMaterial_straightRoutingAndMorticingApp.ToolRelationships =
                routeJob_laminatesMaterial_straightRoutingAndMorticingApp_Tools;

            var routeJob_laminatesMaterial_straightRoutingAndMorticingApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            routeJob_laminatesMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_laminatesMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85213M.AccessoryId
                });
            routeJob_laminatesMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_laminatesMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85248M.AccessoryId
                });
            routeJob_laminatesMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_laminatesMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85613M.AccessoryId
                });
            routeJob_laminatesMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_laminatesMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85682M.AccessoryId
                });
            routeJob_laminatesMaterial_straightRoutingAndMorticingApp.AccessoryRelationships =
                routeJob_laminatesMaterial_straightRoutingAndMorticingApp_Accessories;

            var routeJob_laminatesMaterial_trimmingCutOutApp_Tools = new List<ApplicationToolRelationship>();
            routeJob_laminatesMaterial_trimmingCutOutApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_laminatesMaterial_trimmingCutOutApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_laminatesMaterial_trimmingCutOutApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_laminatesMaterial_trimmingCutOutApp.ApplicationId,
                ToolId = toolMRC23EVSK.ToolId
            });
            routeJob_laminatesMaterial_trimmingCutOutApp.ToolRelationships =
                routeJob_laminatesMaterial_trimmingCutOutApp_Tools;

            var routeJob_laminatesMaterial_trimmingCutOutApp_Accessories = new List<ApplicationAccessoryRelationship>();
            routeJob_laminatesMaterial_trimmingCutOutApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_laminatesMaterial_trimmingCutOutApp.ApplicationId,
                AccessoryId = accessory85268M.AccessoryId
            });
            routeJob_laminatesMaterial_trimmingCutOutApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_laminatesMaterial_trimmingCutOutApp.ApplicationId,
                AccessoryId = accessory85911M.AccessoryId
            });
            routeJob_laminatesMaterial_trimmingCutOutApp.AccessoryRelationships =
                routeJob_laminatesMaterial_trimmingCutOutApp_Accessories;

            var routeJob_plasticsMaterial_surfaceFormingApp_Tools = new List<ApplicationToolRelationship>();
            routeJob_plasticsMaterial_surfaceFormingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_plasticsMaterial_surfaceFormingApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_plasticsMaterial_surfaceFormingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_plasticsMaterial_surfaceFormingApp.ApplicationId,
                ToolId = toolMRC23EVSK.ToolId
            });
            routeJob_plasticsMaterial_surfaceFormingApp.ToolRelationships =
                routeJob_plasticsMaterial_surfaceFormingApp_Tools;

            var routeJob_plasticsMaterial_surfaceFormingApp_Accessories = new List<ApplicationAccessoryRelationship>();
            routeJob_plasticsMaterial_surfaceFormingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_plasticsMaterial_surfaceFormingApp.ApplicationId,
                AccessoryId = accessory85445M.AccessoryId
            });
            routeJob_plasticsMaterial_surfaceFormingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_plasticsMaterial_surfaceFormingApp.ApplicationId,
                AccessoryId = accessory85446M.AccessoryId
            });
            routeJob_plasticsMaterial_surfaceFormingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_plasticsMaterial_surfaceFormingApp.ApplicationId,
                AccessoryId = accessory85448M.AccessoryId
            });
            routeJob_plasticsMaterial_surfaceFormingApp.AccessoryRelationships =
                routeJob_plasticsMaterial_surfaceFormingApp_Accessories;

            var routeJob_plasticsMaterial_straightRoutingAndMorticingApp_Tools =
                new List<ApplicationToolRelationship>();
            routeJob_plasticsMaterial_straightRoutingAndMorticingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_plasticsMaterial_straightRoutingAndMorticingApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_plasticsMaterial_straightRoutingAndMorticingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_plasticsMaterial_straightRoutingAndMorticingApp.ApplicationId,
                ToolId = toolMRC23EVSK.ToolId
            });
            routeJob_plasticsMaterial_straightRoutingAndMorticingApp.ToolRelationships =
                routeJob_plasticsMaterial_straightRoutingAndMorticingApp_Tools;

            var routeJob_plasticsMaterial_straightRoutingAndMorticingApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            routeJob_plasticsMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_plasticsMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85213M.AccessoryId
                });
            routeJob_plasticsMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_plasticsMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85613M.AccessoryId
                });
            routeJob_plasticsMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_plasticsMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85682M.AccessoryId
                });
            routeJob_plasticsMaterial_straightRoutingAndMorticingApp.AccessoryRelationships =
                routeJob_plasticsMaterial_straightRoutingAndMorticingApp_Accessories;

            var routeJob_plasticsMaterial_trimmingCutOutApp_Tools = new List<ApplicationToolRelationship>();
            routeJob_plasticsMaterial_trimmingCutOutApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_plasticsMaterial_trimmingCutOutApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_plasticsMaterial_trimmingCutOutApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_plasticsMaterial_trimmingCutOutApp.ApplicationId,
                ToolId = toolMRC23EVSK.ToolId
            });
            routeJob_plasticsMaterial_trimmingCutOutApp.ToolRelationships =
                routeJob_plasticsMaterial_trimmingCutOutApp_Tools;

            var routeJob_plasticsMaterial_trimmingCutOutApp_Accessories = new List<ApplicationAccessoryRelationship>();
            routeJob_plasticsMaterial_trimmingCutOutApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_plasticsMaterial_trimmingCutOutApp.ApplicationId,
                AccessoryId = accessory85268M.AccessoryId
            });
            routeJob_plasticsMaterial_trimmingCutOutApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_plasticsMaterial_trimmingCutOutApp.ApplicationId,
                AccessoryId = accessory85911M.AccessoryId
            });
            routeJob_plasticsMaterial_trimmingCutOutApp.AccessoryRelationships =
                routeJob_plasticsMaterial_trimmingCutOutApp_Accessories;

            var routeJob_aluminiumMaterial_surfaceFormingApp_Tools = new List<ApplicationToolRelationship>();
            routeJob_aluminiumMaterial_surfaceFormingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_aluminiumMaterial_surfaceFormingApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_aluminiumMaterial_surfaceFormingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_aluminiumMaterial_surfaceFormingApp.ApplicationId,
                ToolId = toolMRC23EVSK.ToolId
            });
            routeJob_aluminiumMaterial_surfaceFormingApp.ToolRelationships =
                routeJob_aluminiumMaterial_surfaceFormingApp_Tools;

            var routeJob_aluminiumMaterial_surfaceFormingApp_Accessories = new List<ApplicationAccessoryRelationship>();
            routeJob_aluminiumMaterial_surfaceFormingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_aluminiumMaterial_surfaceFormingApp.ApplicationId,
                AccessoryId = accessory85445M.AccessoryId
            });
            routeJob_aluminiumMaterial_surfaceFormingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_aluminiumMaterial_surfaceFormingApp.ApplicationId,
                AccessoryId = accessory85446M.AccessoryId
            });
            routeJob_aluminiumMaterial_surfaceFormingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_aluminiumMaterial_surfaceFormingApp.ApplicationId,
                AccessoryId = accessory85448M.AccessoryId
            });
            routeJob_aluminiumMaterial_surfaceFormingApp.AccessoryRelationships =
                routeJob_aluminiumMaterial_surfaceFormingApp_Accessories;

            var routeJob_aluminiumMaterial_trimmingCutOutApp_Tools = new List<ApplicationToolRelationship>();
            routeJob_aluminiumMaterial_trimmingCutOutApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_aluminiumMaterial_trimmingCutOutApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_aluminiumMaterial_trimmingCutOutApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_aluminiumMaterial_trimmingCutOutApp.ApplicationId,
                ToolId = toolMRC23EVSK.ToolId
            });
            routeJob_aluminiumMaterial_trimmingCutOutApp.ToolRelationships =
                routeJob_aluminiumMaterial_trimmingCutOutApp_Tools;

            var routeJob_aluminiumMaterial_trimmingCutOutApp_Accessories = new List<ApplicationAccessoryRelationship>();
            routeJob_aluminiumMaterial_trimmingCutOutApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_aluminiumMaterial_trimmingCutOutApp.ApplicationId,
                AccessoryId = accessory85268M.AccessoryId
            });
            routeJob_aluminiumMaterial_trimmingCutOutApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_aluminiumMaterial_trimmingCutOutApp.ApplicationId,
                AccessoryId = accessory85911M.AccessoryId
            });
            routeJob_aluminiumMaterial_trimmingCutOutApp.AccessoryRelationships =
                routeJob_aluminiumMaterial_trimmingCutOutApp_Accessories;

            var routeJob_aluminiumMaterial_straightRoutingAndMorticingApp_Tools =
                new List<ApplicationToolRelationship>();
            routeJob_aluminiumMaterial_straightRoutingAndMorticingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_aluminiumMaterial_straightRoutingAndMorticingApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_aluminiumMaterial_straightRoutingAndMorticingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_aluminiumMaterial_straightRoutingAndMorticingApp.ApplicationId,
                ToolId = toolMRC23EVSK.ToolId
            });
            routeJob_aluminiumMaterial_straightRoutingAndMorticingApp.ToolRelationships =
                routeJob_aluminiumMaterial_straightRoutingAndMorticingApp_Tools;

            var routeJob_aluminiumMaterial_straightRoutingAndMorticingApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            routeJob_aluminiumMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_aluminiumMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85213M.AccessoryId
                });
            routeJob_aluminiumMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_aluminiumMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85248M.AccessoryId
                });
            routeJob_aluminiumMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_aluminiumMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85613M.AccessoryId
                });
            routeJob_aluminiumMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_aluminiumMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85682M.AccessoryId
                });
            routeJob_aluminiumMaterial_straightRoutingAndMorticingApp.AccessoryRelationships =
                routeJob_aluminiumMaterial_straightRoutingAndMorticingApp_Accessories;

            var routeJob_solidSurfaceMaterial_surfaceFormingApp_Tools = new List<ApplicationToolRelationship>();
            routeJob_solidSurfaceMaterial_surfaceFormingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_surfaceFormingApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_solidSurfaceMaterial_surfaceFormingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_surfaceFormingApp.ApplicationId,
                ToolId = toolMRC23EVSK.ToolId
            });
            routeJob_solidSurfaceMaterial_surfaceFormingApp.ToolRelationships =
                routeJob_solidSurfaceMaterial_surfaceFormingApp_Tools;

            var routeJob_solidSurfaceMaterial_surfaceFormingApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            routeJob_solidSurfaceMaterial_surfaceFormingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_surfaceFormingApp.ApplicationId,
                AccessoryId = accessory85445M.AccessoryId
            });
            routeJob_solidSurfaceMaterial_surfaceFormingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_surfaceFormingApp.ApplicationId,
                AccessoryId = accessory85446M.AccessoryId
            });
            routeJob_solidSurfaceMaterial_surfaceFormingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_surfaceFormingApp.ApplicationId,
                AccessoryId = accessory85448M.AccessoryId
            });
            routeJob_solidSurfaceMaterial_surfaceFormingApp.AccessoryRelationships =
                routeJob_solidSurfaceMaterial_surfaceFormingApp_Accessories;

            var routeJob_solidSurfaceMaterial_trimmingCutOutApp_Tools = new List<ApplicationToolRelationship>();
            routeJob_solidSurfaceMaterial_trimmingCutOutApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_trimmingCutOutApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_solidSurfaceMaterial_trimmingCutOutApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_trimmingCutOutApp.ApplicationId,
                ToolId = toolMRC23EVSK.ToolId
            });
            routeJob_solidSurfaceMaterial_trimmingCutOutApp.ToolRelationships =
                routeJob_solidSurfaceMaterial_trimmingCutOutApp_Tools;

            var routeJob_solidSurfaceMaterial_trimmingCutOutApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            routeJob_solidSurfaceMaterial_trimmingCutOutApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_trimmingCutOutApp.ApplicationId,
                AccessoryId = accessory85268M.AccessoryId
            });
            routeJob_solidSurfaceMaterial_trimmingCutOutApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_trimmingCutOutApp.ApplicationId,
                AccessoryId = accessory85911M.AccessoryId
            });
            routeJob_solidSurfaceMaterial_trimmingCutOutApp.AccessoryRelationships =
                routeJob_solidSurfaceMaterial_trimmingCutOutApp_Accessories;

            var routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp_Tools =
                new List<ApplicationToolRelationship>();
            routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp.ApplicationId,
                ToolId = toolMRC23EVSK.ToolId
            });
            routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp.ToolRelationships =
                routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp_Tools;

            var routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85213M.AccessoryId
                });
            routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85248M.AccessoryId
                });
            routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85613M.AccessoryId
                });
            routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85682M.AccessoryId
                });
            routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp.AccessoryRelationships =
                routeJob_solidSurfaceMaterial_straightRoutingAndMorticingApp_Accessories;

            var routeJob_solidSurfaceMaterial_jointMakingApp_Tools = new List<ApplicationToolRelationship>();
            routeJob_solidSurfaceMaterial_jointMakingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_jointMakingApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_solidSurfaceMaterial_jointMakingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_jointMakingApp.ApplicationId,
                ToolId = toolMRC23EVSK.ToolId
            });
            routeJob_solidSurfaceMaterial_jointMakingApp.ToolRelationships =
                routeJob_solidSurfaceMaterial_jointMakingApp_Tools;

            var routeJob_solidSurfaceMaterial_jointMakingApp_Accessories = new List<ApplicationAccessoryRelationship>();
            routeJob_solidSurfaceMaterial_jointMakingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_jointMakingApp.ApplicationId,
                AccessoryId = accessory84624M.AccessoryId
            });
            routeJob_solidSurfaceMaterial_jointMakingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_jointMakingApp.ApplicationId,
                AccessoryId = accessory84703M.AccessoryId
            });
            routeJob_solidSurfaceMaterial_jointMakingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_jointMakingApp.ApplicationId,
                AccessoryId = accessory85218M.AccessoryId
            });
            routeJob_solidSurfaceMaterial_jointMakingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_solidSurfaceMaterial_jointMakingApp.ApplicationId,
                AccessoryId = accessory85614M.AccessoryId
            });
            routeJob_solidSurfaceMaterial_jointMakingApp.AccessoryRelationships =
                routeJob_solidSurfaceMaterial_jointMakingApp_Accessories;

            var routeJob_woodCompositesMaterial_surfaceFormingApp_List = new List<ApplicationToolRelationship>();
            routeJob_woodCompositesMaterial_surfaceFormingApp_List.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_surfaceFormingApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_woodCompositesMaterial_surfaceFormingApp_List.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_surfaceFormingApp.ApplicationId,
                ToolId = toolMRC23EVSK.ToolId
            });
            routeJob_woodCompositesMaterial_surfaceFormingApp.ToolRelationships =
                routeJob_woodCompositesMaterial_surfaceFormingApp_List;

            var routeJob_woodCompositesMaterial_surfaceFormingApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            routeJob_woodCompositesMaterial_surfaceFormingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_surfaceFormingApp.ApplicationId,
                AccessoryId = accessory85445M.AccessoryId
            });
            routeJob_woodCompositesMaterial_surfaceFormingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_surfaceFormingApp.ApplicationId,
                AccessoryId = accessory85446M.AccessoryId
            });
            routeJob_woodCompositesMaterial_surfaceFormingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_surfaceFormingApp.ApplicationId,
                AccessoryId = accessory85448M.AccessoryId
            });
            routeJob_woodCompositesMaterial_surfaceFormingApp.AccessoryRelationships =
                routeJob_woodCompositesMaterial_surfaceFormingApp_Accessories;

            var routeJob_woodCompositesMaterial_trimmingCutOutApp_Tools = new List<ApplicationToolRelationship>();
            routeJob_woodCompositesMaterial_trimmingCutOutApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_trimmingCutOutApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_woodCompositesMaterial_trimmingCutOutApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_trimmingCutOutApp.ApplicationId,
                ToolId = toolMRC23EVSK.ToolId
            });
            routeJob_woodCompositesMaterial_trimmingCutOutApp.ToolRelationships =
                routeJob_woodCompositesMaterial_trimmingCutOutApp_Tools;

            var routeJob_woodCompositesMaterial_trimmingCutOutApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            routeJob_woodCompositesMaterial_trimmingCutOutApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_trimmingCutOutApp.ApplicationId,
                AccessoryId = accessory85268M.AccessoryId
            });
            routeJob_woodCompositesMaterial_trimmingCutOutApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_trimmingCutOutApp.ApplicationId,
                AccessoryId = accessory85911M.AccessoryId
            });
            routeJob_woodCompositesMaterial_trimmingCutOutApp.AccessoryRelationships =
                routeJob_woodCompositesMaterial_trimmingCutOutApp_Accessories;

            var routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp_Tools =
                new List<ApplicationToolRelationship>();
            routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp.ApplicationId,
                ToolId = toolMRC23EVSK.ToolId
            });
            routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp.ToolRelationships =
                routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp_Tools;

            var routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85213M.AccessoryId
                });
            routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85248M.AccessoryId
                });
            routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85613M.AccessoryId
                });
            routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp_Accessories.Add(
                new ApplicationAccessoryRelationship
                {
                    ApplicationId = routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp.ApplicationId,
                    AccessoryId = accessory85682M.AccessoryId
                });
            routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp.AccessoryRelationships =
                routeJob_woodCompositesMaterial_straightRoutingAndMorticingApp_Accessories;

            var routeJob_woodCompositesMaterial_jointMakingApp_Tools = new List<ApplicationToolRelationship>();
            routeJob_woodCompositesMaterial_jointMakingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_jointMakingApp.ApplicationId,
                ToolId = tool1617EVSPK.ToolId
            });
            routeJob_woodCompositesMaterial_jointMakingApp_Tools.Add(new ApplicationToolRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_jointMakingApp.ApplicationId,
                ToolId = toolMRC23EVSK.ToolId
            });
            routeJob_woodCompositesMaterial_jointMakingApp.ToolRelationships =
                routeJob_woodCompositesMaterial_jointMakingApp_Tools;

            var routeJob_woodCompositesMaterial_jointMakingApp_Accessories =
                new List<ApplicationAccessoryRelationship>();
            routeJob_woodCompositesMaterial_jointMakingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_jointMakingApp.ApplicationId,
                AccessoryId = accessory84624M.AccessoryId
            });
            routeJob_woodCompositesMaterial_jointMakingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_jointMakingApp.ApplicationId,
                AccessoryId = accessory84703M.AccessoryId
            });
            routeJob_woodCompositesMaterial_jointMakingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_jointMakingApp.ApplicationId,
                AccessoryId = accessory85218M.AccessoryId
            });
            routeJob_woodCompositesMaterial_jointMakingApp_Accessories.Add(new ApplicationAccessoryRelationship
            {
                ApplicationId = routeJob_woodCompositesMaterial_jointMakingApp.ApplicationId,
                AccessoryId = accessory85614M.AccessoryId
            });
            routeJob_woodCompositesMaterial_jointMakingApp.AccessoryRelationships =
                routeJob_woodCompositesMaterial_jointMakingApp_Accessories;

            #endregion


            /***
                Measure ->

                (Material/Environment)

                Indoors & Outdoors
                Indoors
                Outdoors

                Distance

                GLM 100 C - Laser Measure with Bluetooth Wireless Technology
                GLM 15 - 50 Ft. Laser Measure
                GLM 20 - 65 Ft. Laser Measure
                GLM 40 - 135 Ft. Laser Measure
                GLM 50 C - 165 Ft. Laser Measure
                GLM 80 - Lithium-Ion Laser Distance Measurer
                GLR 225 - Laser Distance Measurer
             */

            savedCount = jobContext.SaveChanges();
            return savedCount;
        }

        public static int SeedBoschToolTradesGraphData(JobAssistantContext jobContext)
        {
            // TODO: Adjust to what BoschTools actually has.  Which is: Trade -> Categories -> Tools

            var agricultureFarmingTrade = new Trade {Name = "Agriculture and Farming", DomainId = BoschToolsDataTenant.DomainId };
            var architectTrade = new Trade {Name = "Architect", DomainId = BoschToolsDataTenant.DomainId };
            var automotiveTrade = new Trade {Name = "Automotive and Other Vehicle Maintenance", DomainId = BoschToolsDataTenant.DomainId };
            var concreteTrade = new Trade {Name = "Concrete and Masonry", DomainId = BoschToolsDataTenant.DomainId };
            var woodworkingTrade = new Trade {Name = "Carpentry and Woodworking", DomainId = BoschToolsDataTenant.DomainId };
            var doItYourselfTrade = new Trade {Name = "Do It Yourselfer", DomainId = BoschToolsDataTenant.DomainId };
            var electricalTrade = new Trade {Name = "Electrical (Indoors)", DomainId = BoschToolsDataTenant.DomainId };
            var engineeringTrade = new Trade {Name = "Engineering", DomainId = BoschToolsDataTenant.DomainId };
            var exteriorInteriorTrade = new Trade {Name = "Exterior/Interior Finishing", DomainId = BoschToolsDataTenant.DomainId };
            var maintenanceTrade = new Trade {Name = "Facility/Maintenance", DomainId = BoschToolsDataTenant.DomainId };
            var contractorTrade = new Trade {Name = "General Contractor", DomainId = BoschToolsDataTenant.DomainId };
            var industrialFabricationTrade = new Trade {Name = "Industrial Fabrication", DomainId = BoschToolsDataTenant.DomainId };
            var landscapingTrade = new Trade {Name = "Landscaping", DomainId = BoschToolsDataTenant.DomainId };
            var mechanicalTrade = new Trade {Name = "Mechanical (HVAC/R)", DomainId = BoschToolsDataTenant.DomainId };
            var metalWorkingTrade = new Trade {Name = "Metalworking", DomainId = BoschToolsDataTenant.DomainId };
            var plumbingTrade = new Trade {Name = "Plumbing", DomainId = BoschToolsDataTenant.DomainId };
            var relatorTrade = new Trade {Name = "Realtor", DomainId = BoschToolsDataTenant.DomainId };
            var retiredTrade = new Trade {Name = "Retired", DomainId = BoschToolsDataTenant.DomainId };
            var utilitiesTrade = new Trade {Name = "Utilities (Outdoors)", DomainId = BoschToolsDataTenant.DomainId };

            var toolA = new Tool() { Name = "Tool A", DomainId = BoschToolsDataTenant.DomainId };
            var toolB = new Tool() { Name = "Tool B", DomainId = BoschToolsDataTenant.DomainId };
            var toolC = new Tool() { Name = "Tool C", DomainId = BoschToolsDataTenant.DomainId };

            var benchTopCategory = new Category {Name = "Benchtop", DomainId = BoschToolsDataTenant.DomainId, Tools = new List<Tool>() { toolA }};
            var hammerCategory = new Category {Name = "Hammer", DomainId = BoschToolsDataTenant.DomainId, Tools = new List<Tool>() { toolB } };
            var drillCategory = new Category {Name = "Drill", DomainId = BoschToolsDataTenant.DomainId, Tools = new List<Tool>() { toolC } };

            agricultureFarmingTrade.Categories = new List<Category>
            {
                benchTopCategory,
                hammerCategory,
                drillCategory,
            };

            jobContext.Trades.AddRange(new List<Trade>
            {
                agricultureFarmingTrade,
                architectTrade,
                automotiveTrade,
                concreteTrade,
                woodworkingTrade,
                doItYourselfTrade,
                electricalTrade,
                engineeringTrade,
                exteriorInteriorTrade,
                maintenanceTrade,
                contractorTrade,
                industrialFabricationTrade,
                landscapingTrade,
                mechanicalTrade,
                metalWorkingTrade,
                plumbingTrade,
                relatorTrade,
                retiredTrade,
                utilitiesTrade
            });

            var savedCount = jobContext.SaveChanges();
            return savedCount;
        }
    }
}