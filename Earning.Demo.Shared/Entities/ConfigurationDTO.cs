using Earning.Demo.Shared.Services;

namespace Earning.Demo.Shared.Entities
{
    public class ConfigurationDTO: IConfigurationService
    {
        public string ApiRedisKey { get; set; }
        public string WorkerRedisKey { get; set; }
        public string WebRedisKey { get; set; }
        public string ApiUrl { get; set; }
        public string RedisConnectionString { get; set; }
        public string ApplicationId { get; set; }
        public string NodeName { get; set; }
        public string ApiHostPort { get; set; }
        public string WebHostPort { get; set; }
        public string RedisServer { get; set; }
        public bool IsAbTesting { get; set; }
        public string TestingCommand { get; set; }
        public string WorkerBusyKey { get; set; }
        public string AbApiUrl { get; set; }
    }
}
