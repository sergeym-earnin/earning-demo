using Earning.Demo.Shared;
using StackExchange.Redis;
using System;

namespace Earning.Demo.Api.Services
{
    public class StorageService: IDisposable
    {
        ConnectionMultiplexer _connection;
        public IConfigurationService Configuration;

        public StorageService()
        {
            Configuration = new ConfigurationService();
            _connection = ConnectionMultiplexer.Connect(Configuration.RedisConnectionString);
        }

        public void Increment(string key, int incrementValue)
        {
            var db = _connection.GetDatabase();
            var value = db.StringGet(key);
            db.StringSet(key, string.IsNullOrEmpty(value) ? 0 : int.Parse(value) + incrementValue);
        }

        public string Get(string key)
        {
            var db = _connection.GetDatabase();
            return db.StringGet(key);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
