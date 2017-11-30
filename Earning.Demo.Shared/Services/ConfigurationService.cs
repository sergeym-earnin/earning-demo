using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace Earning.Demo.Shared.Services
{
    public class ConfigurationService: IConfigurationService
    {
        public string ApiRedisKey { get; set; }
        public string WorkerRedisKey { get; set; }
        public string WebRedisKey { get; set; }
        public string ApiUrl { get; set; }
        public string AbApiUrl { get; set; }
        public string RedisConnectionString { get; set; }
        public string ApplicationId { get; set; }
        public string NodeName { get; set; }

        public string RedisServer { get; set; }
        public bool IsAbTesting { get; set; }
        public string TestingCommand { get; set; }
        public string WorkerBusyKey { get; set; }

        public ConfigurationService()
        {
            var enviroment = Environment.GetEnvironmentVariable("DEMO_ENVIRONMENT");
            enviroment = string.IsNullOrEmpty(enviroment) ? string.Empty: $".{enviroment}";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings{enviroment}.json");

            IConfiguration Configuration = builder.Build();

            ApplicationId = Environment.GetEnvironmentVariable("HOSTNAME")?? Configuration["ApplicationId"];
            NodeName = Environment.GetEnvironmentVariable("NODE_NAME") ?? Configuration["NodeName"];
            TestingCommand = Configuration["TestingCommand"];
            IsAbTesting = bool.Parse(Environment.GetEnvironmentVariable("DEMO_AB") ?? "false");

            ApiRedisKey = Configuration["ApiRedisKey"];
            WorkerRedisKey = Configuration["WorkerRedisKey"];
            WebRedisKey = Configuration["WebRedisKey"];
            WorkerBusyKey = Configuration["WorkerBusyKey"];

            ApiUrl = Configuration["ApiUrl"];
            AbApiUrl = Configuration["AbApiUrl"];
            RedisServer = Configuration["RedisServer"];
            RedisConnectionString = Configuration["RedisConnectionString"];
        }
    }
}
