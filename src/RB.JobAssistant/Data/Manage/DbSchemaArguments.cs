#pragma warning disable 1591
using Microsoft.Extensions.Configuration;

namespace RB.JobAssistant.Data.Manage
{
    public static class DbSchemaArguments
    {
        public static bool ProcessArguments(IConfigurationRoot config)
        {
            if (HasCreate(config))
            {
                var manager = new DbSchemaManager();
                manager.CreateSchema();
                return true;
            }
            if (HasDrop(config))
            {
                var manager = new DbSchemaManager();
                manager.DropSchema();
                return true;
            }
            if (HasEraseAll(config))
            {
                var manager = new DbSchemaManager();
                manager.EraseAllData();
                return true;
            }
            if (HasLoadSampleData(config))
            {
                var manager = new DbSchemaManager();
                manager.LoadSampleData();
                return true;
            }
            return false;
        }

        public static bool HasCreate(IConfigurationRoot config)
        {
            var schemaCommand = GetSchemaCommand(config);
            return !string.IsNullOrWhiteSpace(schemaCommand) && schemaCommand.ToLower().StartsWith("c");
        }

        public static bool HasDrop(IConfigurationRoot config)
        {
            var schemaCommand = GetSchemaCommand(config);
            return !string.IsNullOrWhiteSpace(schemaCommand) && schemaCommand.ToLower().StartsWith("d");
        }

        public static bool HasLoadSampleData(IConfigurationRoot config)
        {
            var schemaCommand = GetSchemaCommand(config);
            return !string.IsNullOrWhiteSpace(schemaCommand) && schemaCommand.ToLower().StartsWith("s");
        }

        public static bool HasEraseAll(IConfigurationRoot config)
        {
            var schemaCommand = GetSchemaCommand(config);
            return !string.IsNullOrWhiteSpace(schemaCommand) && schemaCommand.ToLower().StartsWith("e");
        }

        private static string GetSchemaCommand(IConfigurationRoot config)
        {
            return config["Schema:Op"];
        }
    }
}