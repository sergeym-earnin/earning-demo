using StackExchange.Redis;
using System;

namespace Earning.Demo.Api.Services
{
    public class StorageService: IDisposable
    {
        public string REDIS_API_ITEM_KEY = "API_KEY";
        public string REDIS_ITEM_KEY = "WORKER_KEY";

        string CONNECTION_STRING = "earnindemo.redis.cache.windows.net:6380,password=aGxKWzVUlpzQLyvDOP8cXYC3MMl99zOsdMU8QtNqNi0=,ssl=True,abortConnect=False";

        ConnectionMultiplexer _connection;

        public StorageService()
        {
            _connection = ConnectionMultiplexer.Connect(CONNECTION_STRING);
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
