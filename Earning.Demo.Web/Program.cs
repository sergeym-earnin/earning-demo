using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Earning.Demo.Shared;
using StackExchange.Redis;
using Earning.Demo.Shared.Services;

namespace Earning.Demo.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationService Configuration = new ConfigurationService();
            FabricService.LogVariables(Configuration.WebRedisKey);

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
