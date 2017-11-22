﻿using Microsoft.Extensions.Configuration;
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
        public string ApiHostPort { get; set; }
        public string AbApiHostPort { get; set; }
        public string WebHostPort { get; set; }
        public string RedisServer { get; set; }
        public bool IsAbTesting { get; set; }
        public string TestingCommand { get; set; }
        public string WorkerBusyKey { get; set; }

        public ConfigurationService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration Configuration = builder.Build();

            ApplicationId = Environment.GetEnvironmentVariable("ApplicationId")?? Configuration["ApplicationId"];
            NodeName = Environment.GetEnvironmentVariable("NodeName") ?? Configuration["NodeName"];
            TestingCommand = Configuration["TestingCommand"];
            IsAbTesting = Environment.GetCommandLineArgs().Any(a => a == TestingCommand);

            ApiRedisKey = Configuration["ApiRedisKey"];
            WorkerRedisKey = Configuration["WorkerRedisKey"];
            WebRedisKey = Configuration["WebRedisKey"];
            WorkerBusyKey = Configuration["WorkerBusyKey"];

            ApiHostPort = Configuration["ApiHostPort"];
            AbApiHostPort = Configuration["AbApiHostPort"];
            WebHostPort = Configuration["WebHostPort"];
            ApiUrl = Configuration["ApiUrl"];
            AbApiUrl = Configuration["AbApiUrl"];
            RedisServer = Configuration["RedisServer"];
            RedisConnectionString = Configuration["RedisConnectionString"];
        }
    }
}
