using StackExchange.Redis;

namespace Earning.Demo.Shared.Services
{
    public class FabricService
    {
        public static void LogVariables(string applicationKey)
        {
            IConfigurationService Configuration = new ConfigurationService();
            ConnectionMultiplexer _connection = ConnectionMultiplexer.Connect(Configuration.RedisConnectionString);

            var db = _connection.GetDatabase();

            var prefix = Configuration.NodeName + applicationKey + Configuration.ApplicationId;

            db.StringSet($"{prefix}_ApplicationHostId", Configuration.ApplicationHostId);
            db.StringSet($"{prefix}_ApplicationHostType", Configuration.ApplicationHostType);
            db.StringSet($"{prefix}_ApplicationId", Configuration.ApplicationId);
            db.StringSet($"{prefix}_ApplicationName", Configuration.ApplicationName);
            db.StringSet($"{prefix}_CodePackageInstanceId", Configuration.CodePackageInstanceId);
            db.StringSet($"{prefix}_CodePackageName", Configuration.CodePackageName);
            db.StringSet($"{prefix}_NodeId", Configuration.NodeId);
            db.StringSet($"{prefix}_NodeIPOrFQDN", Configuration.NodeIPOrFQDN);
            db.StringSet($"{prefix}_CodePackageName", Configuration.NodeName);
            db.StringSet($"{prefix}_PackageFileName", Configuration.PackageFileName);
            db.StringSet($"{prefix}_ServiceId", Configuration.ServiceId);
            db.StringSet($"{prefix}_ServicePackageInstanceId", Configuration.ServicePackageInstanceId);
            db.StringSet($"{prefix}_ServicePackageName", Configuration.ServicePackageName);
            db.StringSet($"{prefix}_ServicePackageVersionInstance", Configuration.ServicePackageVersionInstance);
        }
    }
}
