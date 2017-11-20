using System;
using System.Collections.Generic;

namespace Earning.Demo.Shared
{
    public class ConfigurationService: IConfigurationService
    {
        public string NodeId { get; set; }
        public string ServiceId { get; set; }
        public string ApiRedisKey { get; set; }
        public string WorkerRedisKey { get; set; }
        public string WebRedisKey { get; set; }
        public string ApiUrl { get; set; }
        public string RedisConnectionString { get; set; }
        public string ApplicationHostId { get; set; }
        public string ApplicationHostType { get; set; }
        public string ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public string CodePackageInstanceId { get; set; }
        public string CodePackageName { get; set; }
        public string NodeIPOrFQDN { get; set; }
        public string NodeName { get; set; }
        public string RuntimeConnectionAddress { get; set; }
        public string ServicePackageInstanceId { get; set; }
        public string ServicePackageName { get; set; }
        public string ServicePackageVersionInstance { get; set; }
        public string PackageFileName { get; set; }
        public string ApiHostPort { get; set; }
        public string WebHostPort { get; set; }

        public ConfigurationService()
        {
            NodeId = Environment.GetEnvironmentVariable("AZ_BATCH_NODE_ID");
            ServiceId = Environment.GetEnvironmentVariable("AZ_BATCH_JOB_ID");
            ApplicationHostId = Environment.GetEnvironmentVariable("Fabric_ApplicationHostId");
            ApplicationHostType = Environment.GetEnvironmentVariable("Fabric_ApplicationHostType");
            ApplicationId = Environment.GetEnvironmentVariable("Fabric_ApplicationId");
            ApplicationName = Environment.GetEnvironmentVariable("Fabric_ApplicationName");
            CodePackageInstanceId = Environment.GetEnvironmentVariable("Fabric_CodePackageInstanceId");
            CodePackageName = Environment.GetEnvironmentVariable("Fabric_CodePackageName");
            NodeIPOrFQDN = Environment.GetEnvironmentVariable("Fabric_NodeIPOrFQDN");
            NodeName = Environment.GetEnvironmentVariable("Fabric_NodeName");
            RuntimeConnectionAddress = Environment.GetEnvironmentVariable("Fabric_RuntimeConnectionAddress");
            ServicePackageInstanceId = Environment.GetEnvironmentVariable("Fabric_ServicePackageInstanceId");
            ServicePackageName = Environment.GetEnvironmentVariable("Fabric_ServicePackageName");
            ServicePackageVersionInstance = Environment.GetEnvironmentVariable("Fabric_ServicePackageVersionInstance");
            PackageFileName = Environment.GetEnvironmentVariable("FabricPackageFileName");

            ApiRedisKey = "API_KEY";
            WorkerRedisKey = "WORKER_KEY";
            WebRedisKey = "WEB_KEY";
            ApiHostPort = "8080";
            WebHostPort = "80";
            ApiUrl = "http://localhost:8080";
            RedisConnectionString = "earnindemo.redis.cache.windows.net:6380,password=aGxKWzVUlpzQLyvDOP8cXYC3MMl99zOsdMU8QtNqNi0=,ssl=True,abortConnect=False";
        }
    }
}
