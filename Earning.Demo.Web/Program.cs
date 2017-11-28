using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Earning.Demo.Shared.Services;

namespace Earning.Demo.Web
{
    public class Program
    {
        static IConfigurationService Configuration = new ConfigurationService();
        public static void Main(string[] args)
        {
            (new EnviromentService(Configuration)).StartTracking(Configuration.WebRedisKey);
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }
    }
}
