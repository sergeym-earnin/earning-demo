using System;
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
        public string ApiHostPort { get; set; }
        public string AbApiHostPort { get; set; }
        public string WebHostPort { get; set; }
        public string RedisServer { get; set; }
        public bool IsAbTesting { get; set; }
        public string TestingCommand { get; set; }
        public string WorkerBusyKey { get; set; }

        public ConfigurationService()
        {
            ApplicationId = Environment.GetEnvironmentVariable("ApplicationId");
            NodeName = Environment.GetEnvironmentVariable("NodeName");
            TestingCommand = "abtesting";
            IsAbTesting = Environment.GetCommandLineArgs().Any(a => a == TestingCommand);

            ApiRedisKey = "API_KEY";
            WorkerRedisKey = "WORKER_KEY";
            WebRedisKey = "WEB_KEY";
            WorkerBusyKey = "WORKER_BUSY";

            ApiHostPort = "8080";
            AbApiHostPort = "8081";
            WebHostPort = "80";
            ApiUrl = "http://localhost:8080";
            AbApiUrl = "http://localhost:8081";
            RedisServer = "localhost:6379";
            RedisConnectionString = $"{RedisServer},abortConnect=false,syncTimeout=3000,allowAdmin=true";
        }
    }
}
