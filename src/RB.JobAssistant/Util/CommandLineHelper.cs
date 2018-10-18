#pragma warning disable 1591
using System.Collections.Generic;
using System.Linq;

namespace RB.JobAssistant.Util
{
    /**
     * Command line helper class
     * 
     *   Example usage: dotnet run -op=create
     *   Example usage: dotnet run -op=sample
     *   
     *   Example usage can also be: dotnet run --schema:op=create
     *   Example usage can also be: dotnet run --schema:op=sample
     *   
     *   Following publish command, usage can be:
     *
     *      dotnet RB.JobAssistant.dll -op=create
     *      dotnet RB.JobAssistant.dll -op=sample
     * 
     */
    public class CommandLineHelper
    {
        public static IDictionary<string, string> CliArguments => new Dictionary<string, string>
        {
            // TODO: Find a nice way to manage "project/program" specific command line arguments
            {"Schema:Op", string.Empty}
        };

        public static Dictionary<string, string> GetDefaultSwitchMappings()
        {
            return GetSwitchMappings(CliArguments);
        }

        public static Dictionary<string, string> GetSwitchMappings(
            IDictionary<string, string> configurationStrings)
        {
            return configurationStrings.Select(item =>
                    new KeyValuePair<string, string>(
                        "-" + item.Key.Substring(item.Key.LastIndexOf(':') + 1),
                        item.Key))
                .ToDictionary(
                    item => item.Key, item => item.Value);
        }
    }
}