using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Earning.Demo.Shared.Services;

namespace Earning.Demo.Web
{
    public class Program
    {
        static Shared.IConfigurationProvider Configuration = new Shared.ConfigurationProvider();
        public static void Main(string[] args)
        {
            EnviromentService.LogVariables(Configuration.WebRedisKey);
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls($"http://localhost:{Configuration.WebHostPort}")
                .Build();
        }


    }
}
