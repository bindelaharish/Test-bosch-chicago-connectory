using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;

namespace RB.JobAssistant.Core
{
    [UsedImplicitly]
    public static class EnvironmentExtensions
    {
        public static bool IsDevBcnUs(this IHostingEnvironment environment)
        {
            return environment.IsEnvironment("DEV-BCN-US");
        }
        public static bool IsQasBcnUs(this IHostingEnvironment environment)
        {
            return environment.IsEnvironment("QAS-BCN-US");
        }
    }
}