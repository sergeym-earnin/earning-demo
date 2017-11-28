using Earning.Demo.Shared.Entities;
using Earning.Demo.Shared.Extensions;
using Earning.Demo.Shared.Services;
using StackExchange.Redis;
using System.Collections.Generic;

namespace Earning.Demo.Api.Services
{

    class StorageService : IStorageService
    {
        protected ConnectionMultiplexer _connection;
        protected IConfigurationService _configuration;

        public StorageService(IConfigurationService configuration)
        {
            _configuration = configuration;
            _connection = ConnectionMultiplexer.Connect(_configuration.RedisConnectionString);
        }

        public virtual void Increment(string key, int incrementValue)
        {
            var db = _connection.GetDatabase();
            var value = db.StringIncrement(key, incrementValue);
        }

        public virtual string Get(string key)
        {
            var db = _connection.GetDatabase();
            return db.StringGet(key);
        }

        public virtual List<ApplicationDTO> GetAll()
        {
            var db = _connection.GetDatabase();
            var server = _connection.GetServer(_configuration.RedisServer);

            List<ApplicationDTO> result = new List<ApplicationDTO>();


            var keys = server.Keys(pattern: $"*");

            foreach (var key in keys)
            {
                var parts = key.ToString().Split('~');
                if(parts.Length == 1)
                {
                    string value = db.StringGet(key);
                    result.Add(new ApplicationDTO
                    {
                        ApplicationType = null,
                        NodeName = null,
                        ApplicationId = null,
                        Data = value
                    });
                }
                else
                {
                    HashEntry[] value = db.HashGetAll(key);
                    result.Add(new ApplicationDTO
                    {
                        ApplicationType = parts[1].Replace("_KEY", string.Empty),
                        NodeName = parts[0],
                        ApplicationId = parts[2],
                        Configuration = value.ConvertFromRedis<ConfigurationDTO>()
                    });
                }
            }

            return result;
        }

        public virtual void Dispose()
        {
            _connection.Dispose();
        }

        public void SetBusyFlag(bool isBusy)
        {
            var db = _connection.GetDatabase();
            if (isBusy)
            {
                db.StringSet(_configuration.WorkerBusyKey, "BLA");
            }
            else
            {
                db.KeyDelete(_configuration.WorkerBusyKey);
            }
        }

        public bool GetBusyFlag()
        {
            var db = _connection.GetDatabase();
            return db.KeyExists(_configuration.WorkerBusyKey);
        }
    }
}
