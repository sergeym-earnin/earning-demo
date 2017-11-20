namespace Earning.Demo.Shared
{
    public interface IConfigurationService
    {
        string NodeId { get; set; }
        string ServiceId { get; set; }
        string ApiRedisKey { get; set; }
        string WorkerRedisKey { get; set; }
        string WebRedisKey { get; set; }
        string ApiUrl { get; set; }
        string RedisConnectionString { get; set; }
        string ApplicationHostId { get; set; }
        string ApplicationHostType { get; set; }
        string ApplicationId { get; set; }
        string ApplicationName { get; set; }
        string CodePackageInstanceId { get; set; }
        string CodePackageName { get; set; }
        string NodeIPOrFQDN { get; set; }
        string NodeName { get; set; }
        string RuntimeConnectionAddress { get; set; }
        string ServicePackageInstanceId { get; set; }
        string ServicePackageName { get; set; }
        string ServicePackageVersionInstance { get; set; }
        string PackageFileName { get; set; }
    }
}
