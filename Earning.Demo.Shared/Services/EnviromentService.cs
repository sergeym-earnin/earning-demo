using Earning.Demo.Shared.Extensions;
using StackExchange.Redis;

namespace Earning.Demo.Shared.Services
{
    public sealed class EnviromentService: IEnviromentService
    {
        public IConfigurationService _configuration { get; set; }

        public EnviromentService(IConfigurationService configuration)
        {
            _configuration = configuration;
        }

        public void StartTracking(string applicationKey)
        {
            HandleTimer(applicationKey);

            System.Timers.Timer timer = new System.Timers.Timer(new System.TimeSpan(0, 1, 0).TotalMilliseconds);
            timer.Elapsed += (sender, e) => HandleTimer(applicationKey);
            timer.Start();
        }

        public string GetDataKey(string applicationKey)
        {
            return "Data";//$"{_configuration.NodeName}~{applicationKey}~{_configuration.ApplicationId}";
        }

        private void HandleTimer(string applicationKey)
        {
            using (var connection = ConnectionMultiplexer.Connect(_configuration.RedisConnectionString))
            {
                var db = connection.GetDatabase();
                var prefix = $"{_configuration.NodeName}~{applicationKey}~{_configuration.ApplicationId}";
                db.HashSet(prefix, _configuration.ToHashEntries());
                db.KeyExpire(prefix, new System.TimeSpan(0, 2, 0));
            }
        }
    }
}
