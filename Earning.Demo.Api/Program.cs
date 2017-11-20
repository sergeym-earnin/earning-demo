using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Earning.Demo.Shared;
using Earning.Demo.Shared.Services;

namespace Earning.Demo.Api
{
    public class Program
    {
        static IConfigurationService Configuration = new ConfigurationService();

        public static void Main(string[] args)
        {
            FabricService.LogVariables(Configuration.ApiRedisKey);
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls($"http://localhost:{Configuration.ApiHostPort}")
                .Build();
    }
}
