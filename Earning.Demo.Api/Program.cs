using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Earning.Demo.Shared;
using Earning.Demo.Shared.Services;

namespace Earning.Demo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationService Configuration = new ConfigurationService();
            FabricService.LogVariables(Configuration.ApiRedisKey);

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
