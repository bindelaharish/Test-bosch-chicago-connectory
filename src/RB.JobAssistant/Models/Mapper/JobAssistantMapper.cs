#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using Omu.ValueInjecter;
using RB.JobAssistant.Data;
using Serilog;

namespace RB.JobAssistant.Models.Mapper
{
    public class JobAssistantMapper
    {
        internal static readonly MapperInstance MapperObject;

        static JobAssistantMapper()
        {
            MapperObject = new MapperInstance();

            DefineToolModelToToolMapping();
            DefineAccessoryModelToAccessoryMapping();
            // TODO: Add other types including category, trade, job, etc.

            DefineCategoryToCategoryModelMapping();
            DefineTradeToTradeModelMapping();
            DefineJobToJobModelMapping();
            DefineMaterialToMaterialModelMapping();
            DefineApplicationToApplicationModelMapping();
            DefineToolToToolModelMapping();
            DefineAccessoryToAccessoryModelMapping();

            DefineTenantToTenantModelMapping();
            DefineTenantModelToTenantMapping();
        }

        public static T Map<T>(object source)
        {
            Log.Debug("Starting to copy/map object: " + source);

            if (source == null)
                return default(T);
            var mappedObject = MapperObject.Map<T>(source);

            Log.Debug("Mapped objected is: " + source);
            return mappedObject;
        }

        public static ICollection<T> MapObjects<T>(ICollection<T> sourceObjects)
        {
            if (sourceObjects == null)
                return null;
            ICollection<T> copiedObjects = new List<T>();
            foreach (var sourceObject in sourceObjects)
                copiedObjects.Add(MapperObject.Map<T>(sourceObject));
            return copiedObjects;
        }

        public static ICollection<T> MapObjects<T>(IQueryable<object> sourceObjects)
        {
            Log.Debug("Starting to copy/map a collection of {0} objects.", sourceObjects.Count());
            ICollection<T> copiedObjects = new List<T>();
            // Use .ToList() to trigger loading of data
            var enumerator = sourceObjects.ToList().GetEnumerator();
            while (enumerator.MoveNext())
                copiedObjects.Add(MapperObject.Map<T>(enumerator.Current));
            enumerator.Dispose();
            return copiedObjects;
        }

        private static void DefineApplicationToApplicationModelMapping()
        {
            MapperObject.AddMap<Application, ApplicationModel>(src =>
            {
                var res = new ApplicationModel();
                res.InjectFrom(src);
                res.TenantDomain = src.DomainId;

                if (src.ToolRelationships != null) {
                    var tools = from t in src.ToolRelationships where t.ApplicationId == src.ApplicationId select new Tool() { ToolId =  t.ToolId };
                    res.Tools = tools.Select(x => MapperObject.Map<Tool, ToolModel>(x)).ToList();
                }

                if (src.AccessoryRelationships != null) { 
                    var accessories = from a in src.AccessoryRelationships where a.ApplicationId == src.ApplicationId select new Accessory() { AccessoryId =  a.AccessoryId };
                    res.Accessories = accessories.Select(x => MapperObject.Map<Accessory, AccessoryModel>(x)).ToList();
                }
                return res;
            });
        }

        private static void DefineCategoryToCategoryModelMapping()
        {
            MapperObject.AddMap<Category, CategoryModel>(src =>
            {
                var res = new CategoryModel();
                res.InjectFrom(src);
                res.TenantDomain = src.DomainId;
                if (src.Jobs != null)
                    res.Jobs = src.Jobs.Select(x => MapperObject.Map<Job, JobModel>(x)).ToList();
                if (src.Materials != null)
                    res.Materials = src.Materials.Select(x => MapperObject.Map<Material, MaterialModel>(x)).ToList();
                return res;
            });
        }

        private static void DefineTradeToTradeModelMapping()
        {
            MapperObject.AddMap<Trade, TradeModel>(src =>
            {
                var res = new TradeModel();
                res.InjectFrom(src);
                res.TenantDomain = src.DomainId;
                if (src.Categories != null)
                    res.Categories = src.Categories.Select(x => MapperObject.Map<Category, CategoryModel>(x)).ToList();
                if (src.Tools != null)
                    res.Tools = src.Tools.Select(x => MapperObject.Map<Tool, ToolModel>(x)).ToList();
                return res;
            });
        }

        private static void DefineJobToJobModelMapping()
        {
            MapperObject.AddMap<Job, JobModel>(src =>
            {
                var res = new JobModel();
                res.InjectFrom(src);
                res.TenantDomain = src.DomainId;
                if (!string.IsNullOrEmpty(src.Name))
                    res.Name = "[ " + src.Name + " ]";
                else
                    res.Name = string.Empty;
                if (src.Materials != null && src.Materials.Count > 0)
                    res.Materials = src.Materials.Select(x => MapperObject.Map<Material, MaterialModel>(x)).ToList();
                if (src.ToolRelationships != null) {
                    var tools = from j in src.ToolRelationships where j.JobId == src.JobId select new Tool() { ToolId =  j.ToolId };
                    res.Tools = tools.Select(x => MapperObject.Map<Tool, ToolModel>(x)).ToList();
                }
                if (src.AccessoryRelationships != null) { 
                    var accessories = from j in src.AccessoryRelationships where j.JobId == src.JobId select new Accessory() { AccessoryId =  j.AccessoryId };
                    res.Accessories = accessories.Select(x => MapperObject.Map<Accessory, AccessoryModel>(x)).ToList();
                }
                return res;
            });
        }

        private static void DefineMaterialToMaterialModelMapping()
        {
            MapperObject.AddMap<Material, MaterialModel>(src =>
            {
                var res = new MaterialModel();
                res.InjectFrom(src);
                res.TenantDomain = src.DomainId;
                if (src.Applications != null)
                    res.Applications = src.Applications.Select(x => MapperObject.Map<Application, ApplicationModel>(x)).ToList();
                if (src.Tools != null)
                    res.Tools = src.Tools.Select(x => MapperObject.Map<Tool, ToolModel>(x)).ToList();
                if (src.Accessories != null)
                    res.Accessories = src.Accessories.Select(x => MapperObject.Map<Accessory, AccessoryModel>(x)).ToList();
                return res;
            });
        }

        private static void DefineToolToToolModelMapping()
        {
            MapperObject.AddMap<Tool, ToolModel>(src =>
            {
                var res = new ToolModel();
                res.InjectFrom(src);
                res.TenantDomain = src.DomainId;
                return res;
            });
        }

        private static void DefineToolModelToToolMapping()
        {
            MapperObject.AddMap<ToolModel, Tool>(src =>
            {
                var res = new Tool();
                res.InjectFrom(src);
                res.DomainId = src.TenantDomain;
                return res;
            });
        }

        private static void DefineAccessoryModelToAccessoryMapping()
        {
            MapperObject.AddMap<AccessoryModel, Accessory>(src =>
            {
                var res = new Accessory();
                res.InjectFrom(src);
                res.DomainId = src.TenantDomain;
                return res;
            });
        }

        private static void DefineAccessoryToAccessoryModelMapping()
        {
            MapperObject.AddMap<Accessory, AccessoryModel>(src =>
            {
                var res = new AccessoryModel();
                res.InjectFrom(src);
                res.TenantDomain = src.DomainId;
                return res;
            });
        }

        private static void DefineTenantToTenantModelMapping()
        {
            MapperObject.AddMap<Tenant, TenantModel>(src =>
            {
                var res = new TenantModel();
                res.InjectFrom(src);
                res.Domain = src.DomainId;
                return res;
            });
        }

        private static void DefineTenantModelToTenantMapping()
        {
            MapperObject.AddMap<TenantModel, Tenant>(src =>
            {
                var res = new Tenant();
                res.InjectFrom(src);
                res.DomainId = src.Domain;
                return res;
            });
        }

        private static void DefineJobToJobModelWithFilterMapping()
        {
            MapperObject.AddMap<Job, JobModel>(src =>
            {
                var res = new JobModel();
                res.InjectFrom<IgnoreProps>(src);
                if (!string.IsNullOrEmpty(src.Name))
                    res.Name = "[ " + src.Name + " ]";
                if (src.Materials != null && src.Materials.Count > 0)
                    res.Materials = src.Materials.Select(x => MapperObject.Map<Material, MaterialModel>(x)).ToList();
                return res;
            });
        }

        private object CreateObject<T>(object src)
        {
            var res = Activator.CreateInstance<T>();
            res.InjectFrom(src);
            // TODO: Create interface and then apply: myObject.TenantDomain = src.DomainId;
            return res;
        }
    }
}