namespace Earning.Demo.Shared.Services
{
    public interface IConfigurationService
    {
        string ApiRedisKey { get; set; }
        string WorkerRedisKey { get; set; }
        string WebRedisKey { get; set; }
        string ApiUrl { get; set; }
        string RedisConnectionString { get; set; }
        string ApplicationId { get; set; }
        string NodeName { get; set; }
        string RedisServer { get; set; }
        string TestingCommand { get; set; }
        bool IsAbTesting { get; set; }
        string WorkerBusyKey { get; set; }
        string AbApiUrl { get; set; }
    }
}
