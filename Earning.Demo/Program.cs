using Earning.Demo.Shared;
using Earning.Demo.Shared.Services;
using StackExchange.Redis;
using System;
using System.Timers;

namespace Earning.Demo
{
    class Program
    {
        static IConfigurationProvider Configuration = new ConfigurationProvider();
        static ConnectionMultiplexer Connection = ConnectionMultiplexer.Connect(Configuration.RedisConnectionString);

        static string _redisKey;

        static void Main(string[] args)
        {
            Console.WriteLine("[WORKER STARTED]");
            Console.WriteLine($"MachineName: {Environment.MachineName}");

            _redisKey = $"{EnviromentService.GetKey(Configuration.WorkerRedisKey)}~Data";
            EnviromentService.LogVariables(Configuration.WorkerRedisKey);

            Timer timer = new Timer(10000);
            timer.Elapsed += (sender, e) => HandleTimer();
            timer.Start();

            Console.Write("Press any key to exit... ");
            Console.ReadKey();

            Connection.Dispose();
            Console.WriteLine("[WORKER STOPED]");
        }

        private static void HandleTimer()
        {
            var db = Connection.GetDatabase();
            var value = db.StringGet(_redisKey);

            Console.WriteLine($" WORKER INCREMENT ACTION: {value}");

            db.StringIncrement(_redisKey, 1);
        }
    }
}
