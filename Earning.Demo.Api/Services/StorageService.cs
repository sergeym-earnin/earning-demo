using Earning.Demo.Shared;
using Earning.Demo.Shared.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace Earning.Demo.Api.Services
{
    internal sealed class StorageService: IDisposable
    {
        ConnectionMultiplexer _connection;
        public IConfigurationProvider Configuration;
        IList<string> _appTypes;

        public StorageService()
        {
            Configuration = new ConfigurationProvider();
            _connection = ConnectionMultiplexer.Connect(Configuration.RedisConnectionString);
            _appTypes = new List<string>()
            {
                Configuration.ApiRedisKey,
                Configuration.WebRedisKey,
                Configuration.WorkerRedisKey
            };
        }

        public void Increment(string key, int incrementValue)
        {
            var db = _connection.GetDatabase();
            var value = db.StringIncrement(key, incrementValue);
        }

        public string Get(string key)
        {
            var db = _connection.GetDatabase();
            return db.StringGet(key);
        }

        public List<object> GetAll()
        {
            var db = _connection.GetDatabase();
            var server = _connection.GetServer(Configuration.RedisServer);

            List<object> result = new List<object>();

            foreach(var appType in _appTypes)
            {
                var keys = server.Keys(pattern: $"{appType}*");

                foreach (var key in keys)
                {
                    var parts = key.ToString().Split('~');
                    if(parts.Length > 3)
                    {
                        string value = db.StringGet(key);
                        result.Add(new
                        {
                            ApplicationType = parts[0],
                            NodeName = parts[1],
                            ApplicationId = parts[2],
                            Data = value
                        });
                    }
                    else
                    {
                        HashEntry[] value = db.HashGetAll(key);
                        result.Add(new
                        {
                            ApplicationType = parts[0],
                            NodeName = parts[1],
                            ApplicationId = parts[2],
                            Configuration = value.ConvertFromRedis<ConfigurationProvider>()
                        });
                    }
                }
            }

            return result;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
