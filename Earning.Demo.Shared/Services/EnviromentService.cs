using StackExchange.Redis;

namespace Earning.Demo.Shared.Services
{
    public class EnviromentService
    {
        public static IConfigurationProvider Configuration = new ConfigurationProvider();

        public static void LogVariables(string applicationKey)
        {
            ConnectionMultiplexer _connection = ConnectionMultiplexer.Connect(Configuration.RedisConnectionString);

            var db = _connection.GetDatabase();

            var prefix = $"{applicationKey}~{Configuration.NodeName}~{Configuration.ApplicationId}";

            db.HashSet(prefix, Configuration.ToHashEntries());
        }

        public static string GetKey(string applicationKey)
        {
            return $"{applicationKey}~{Configuration.NodeName}~{Configuration.ApplicationId}";
        }
    }
}
