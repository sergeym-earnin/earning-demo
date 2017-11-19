using StackExchange.Redis;

namespace Earning.Demo.Api.Services
{
    public class StorageService
    {
        string REDIS_ITEM_KEY = "API_KEY";
        string CONNECTION_STRING = "earnindemo.redis.cache.windows.net:6380,password=aGxKWzVUlpzQLyvDOP8cXYC3MMl99zOsdMU8QtNqNi0=,ssl=True,abortConnect=False";

        ConnectionMultiplexer _connection;

        public StorageService()
        {
            _connection = ConnectionMultiplexer.Connect(CONNECTION_STRING);
        }

        public void Increment()
        {
            var db = _connection.GetDatabase();
            var value = db.StringGet(REDIS_ITEM_KEY);
            db.StringSet(REDIS_ITEM_KEY, string.IsNullOrEmpty(value) ? 0 : int.Parse(value) + 1);
        }

        public string Get()
        {
            var db = _connection.GetDatabase();
            return db.StringGet(REDIS_ITEM_KEY);
        }
    }
}
