using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Earning.Demo.Shared;
using Earning.Demo.Shared.Services;

namespace Earning.Demo.Api
{
    public class Program
    {
        static IConfigurationProvider Configuration = new ConfigurationProvider();

        public static void Main(string[] args)
        {
            EnviromentService.LogVariables(Configuration.ApiRedisKey);
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls($"http://localhost:{Configuration.ApiHostPort}")
                .Build();
    }
}
