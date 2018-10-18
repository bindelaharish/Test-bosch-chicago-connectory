#pragma warning disable 1591
using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using RB.JobAssistant.Data;

namespace RB.JobAssistant.Controllers
{
    public static class ApiQueryExpression
    {
        private const string QueryBy = "QueryBy";

        private const string CategoryName = "CategoryName";
        private const string JobName = "JobName";
        private const string MaterialName = "MaterialName";
        private const string ApplicationName = "ApplicationName";
        private const string ModelNumber = "ModelNumber";

        private const string DatabaseId = "DatabaseId";

        public static Expression<Func<Category, bool>> GenerateCategoryPredicate<T>(string id, HttpContext context) 
        {
            if (context != null && context.Request.Headers[QueryBy] == CategoryName)
                return c => c.Name == id;
            if (context != null && context.Request.Headers[QueryBy] == DatabaseId)
                return c => c.CategoryId == int.Parse(id);
            return c => c.Name == id;
        }

        public static Expression<Func<Job, bool>> GenerateJobPredicate(string id, string queryBy,
            string tenantDomain = null)
        {
            if (string.IsNullOrEmpty(tenantDomain))
                switch (queryBy)
                {
                    case JobName:
                        return j => j.Name == id;
                    case DatabaseId:
                        return j => j.JobId == int.Parse(id);
                }
            else
                switch (queryBy)
                {
                    case JobName:
                        return j => j.Name == id && IsMatchingTenant(tenantDomain, j);
                    case DatabaseId:
                        return j => j.JobId == int.Parse(id) && IsMatchingTenant(tenantDomain, j);
                }
            return j => j.Name == id;
        }

        public static Expression<Func<Job, bool>> GenerateJobPredicate(string id, HttpContext context)
        {
            if (context != null && context.Request.Headers[QueryBy] == JobName)
                return j => j.Name == id;
            if (context != null && context.Request.Headers[QueryBy] == DatabaseId)
                return j => j.JobId == int.Parse(id);
            return j => j.Name == id;
        }

        public static Expression<Func<Material, bool>> GenerateMaterialPredicate(string id, HttpContext context)
        {
            if (context != null && context.Request.Headers[QueryBy] == MaterialName)
                return m => m.Name == id;
            if (context != null && context.Request.Headers[QueryBy] == DatabaseId)
                return m => m.MaterialId == int.Parse(id);
            return m => m.MaterialId == int.Parse(id);
        }

        public static Expression<Func<Application, bool>> GenerateApplicationPredicate(string id, HttpContext context)
        {
            if (context != null && context.Request.Headers[QueryBy] == ApplicationName)
                return a => a.Name == id;
            if (context != null && context.Request.Headers[QueryBy] == DatabaseId)
                return a => a.ApplicationId == int.Parse(id);
            return a => a.Name == id;
        }

        internal static Expression<Func<Category, bool>> GenerateCategoryPredicate(string id, string queryBy, string tenantDomain = null)
        {
            if (string.IsNullOrEmpty(tenantDomain))
                switch (queryBy)
                {
                    case CategoryName:
                        return c => c.Name == id;
                    case DatabaseId:
                        return c => c.CategoryId == int.Parse(id);
                }
            else
                switch (queryBy)
                {
                    case CategoryName:
                        return m => m.Name == id && IsMatchingTenant(tenantDomain, m);
                    case DatabaseId:
                        return m => m.CategoryId == int.Parse(id) && IsMatchingTenant(tenantDomain, m);
                }
            return m => m.Name == id;
        }

        public static Expression<Func<Material, bool>> GenerateMaterialPredicate(string id, string queryBy,
            string tenantDomain = null)
        {
            if (string.IsNullOrEmpty(tenantDomain))
                switch (queryBy)
                {
                    case MaterialName:
                        return m => m.Name == id;
                    case DatabaseId:
                        return m => m.MaterialId == int.Parse(id);
                }
            else
                switch (queryBy)
                {
                    case MaterialName:
                        return m => m.Name == id && IsMatchingTenant(tenantDomain, m);
                    case DatabaseId:
                        return m => m.MaterialId == int.Parse(id) && IsMatchingTenant(tenantDomain, m);
                }
            return m => m.Name == id;
        }

        public static Expression<Func<Application, bool>> GenerateApplicationPredicate(string id, string queryBy,
            string tenantDomain = null)
        {
            if (string.IsNullOrEmpty(tenantDomain))
                switch (queryBy)
                {
                    case ApplicationName:
                        return a => a.Name == id;
                    case DatabaseId:
                        return a => a.ApplicationId == int.Parse(id);
                }
            else
                switch (queryBy)
                {
                    case ApplicationName:
                        return a => a.Name == id && IsMatchingTenant(tenantDomain, a);
                    case DatabaseId:
                        return a => a.ApplicationId == int.Parse(id) && IsMatchingTenant(tenantDomain, a);
                }
            return a => a.Name == id;
        }

        public static Expression<Func<Tool, bool>> GenerateToolPredicate(string id, HttpContext context)
        {
            if (context != null && context.Request.Headers[QueryBy] == ModelNumber)
                return t => t.ModelNumber == id;
            if (context != null && context.Request.Headers[QueryBy] == DatabaseId)
                return t => t.ToolId == int.Parse(id);
            return t => t.ModelNumber == id;
        }

        public static Expression<Func<Tool, bool>> GenerateToolPredicate(string id, string queryBy,
            string tenantDomain = null)
        {
            if (string.IsNullOrEmpty(tenantDomain))
                switch (queryBy)
                {
                    case ModelNumber:
                        return t => t.ModelNumber == id;
                    case DatabaseId:
                        return t => t.ToolId == int.Parse(id);
                }
            else
                switch (queryBy)
                {
                    case ModelNumber:
                        return t => t.ModelNumber == id && IsMatchingTenant(tenantDomain, t);
                    case DatabaseId:
                        return t => t.ToolId == int.Parse(id) && IsMatchingTenant(tenantDomain, t);
                }
            return t => t.ModelNumber == id;
        }

        public static Expression<Func<Accessory, bool>> GenerateAccessoryPredicate(string id, string queryBy,
            string tenantDomain = null)
        {
            if (string.IsNullOrEmpty(tenantDomain))
                switch (queryBy)
                {
                    case ModelNumber:
                        return a => a.ModelNumber == id;
                    case DatabaseId:
                        return a => a.AccessoryId == int.Parse(id);
                }
            else
                switch (queryBy)
                {
                    case ModelNumber:
                        return a => a.ModelNumber == id && IsMatchingTenant(tenantDomain, a);
                    case DatabaseId:
                        return a => a.AccessoryId == int.Parse(id) && IsMatchingTenant(tenantDomain, a);
                }
            return t => t.ModelNumber == id;
        }

        public static Expression<Func<Accessory, bool>> GenerateAccessoryPredicate(string id, HttpContext context)
        {
            if (context != null && context.Request.Headers[QueryBy] == ModelNumber)
                return a => a.ModelNumber == id;
            if (context != null && context.Request.Headers[QueryBy] == DatabaseId)
                return a => a.AccessoryId == int.Parse(id);
            return a => a.ModelNumber == id;
        }

        private static bool IsMatchingTenant(string tenantDomain, Category c)
        {
            return c.DomainId == tenantDomain;
        }

        private static bool IsMatchingTenant(string tenantDomain, Job j)
        {
            return j.DomainId == tenantDomain;
        }

        private static bool IsMatchingTenant(string tenantDomain, Material m)
        {
            return m.DomainId == tenantDomain;
        }

        private static bool IsMatchingTenant(string tenantDomain, Application a)
        {
            return a.DomainId == tenantDomain;
        }

        private static bool IsMatchingTenant(string tenantDomain, Tool t)
        {
            return t.DomainId == tenantDomain;
        }
        private static bool IsMatchingTenant(string tenantDomain, Accessory a)
        {
            return a.DomainId == tenantDomain;
        }
    }
}