using Earning.Demo.Shared;
using Earning.Demo.Shared.Services;
using StackExchange.Redis;
using System;
using System.Timers;

namespace Earning.Demo
{
    class Program
    {
        static IConfigurationService Configuration = new ConfigurationService();

        static ConnectionMultiplexer Connection = ConnectionMultiplexer.Connect(Configuration.RedisConnectionString);

        static void Main(string[] args)
        {
            Console.WriteLine("[WORKER STARTED]");
            Console.WriteLine($"MachineName: {Environment.MachineName}");

            FabricService.LogVariables(Configuration.WorkerRedisKey);

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
            var value = Get();
            Console.WriteLine($" WORKER INCREMENT ACTION: {value}");

            Increment();
        }

        private static void Increment()
        {
            var db = Connection.GetDatabase();
            var value = db.StringGet($"{Configuration.WorkerRedisKey}_Data");
            db.StringSet($"{Configuration.WorkerRedisKey}_Data", string.IsNullOrEmpty(value) ? 0 : int.Parse(value) + 1);
        }

        private static string Get()
        {
            var db = Connection.GetDatabase();
            return db.StringGet($"{Configuration.WorkerRedisKey}_Data");
        }
    }
}
