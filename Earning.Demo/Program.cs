using Earning.Demo.Shared.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Earning.Demo
{
    class Program
    {
        static IConfigurationService Configuration = new ConfigurationService();
        static ConnectionMultiplexer Connection = ConnectionMultiplexer.Connect(Configuration.RedisConnectionString);

        static string _redisKey;
        static string _busyKey;

        static Task nextTask;

        static void Main(string[] args)
        {
            Console.WriteLine("[WORKER STARTED]");

            var enviroment = new EnviromentService(Configuration);
            enviroment.StartTracking(Configuration.WorkerRedisKey);

            _redisKey = $"{enviroment.GetKey(Configuration.WorkerRedisKey)}~Data";
            _busyKey = Configuration.WorkerBusyKey;

            DoWork();

            Console.Write("Press any key to exit... ");
            Console.ReadKey();

            Connection.Dispose();
            Console.WriteLine("[WORKER STOPED]");
        }

        private static void DoWork()
        {
            var db = Connection.GetDatabase();
            var value = db.StringGet(_redisKey);

            if(!db.KeyExists(_busyKey))
            {
                Console.WriteLine($" WORKER INCREMENT ACTION: {value}");
                db.StringIncrement(_redisKey, 1);
                nextTask = Task.Delay((int)(new TimeSpan(0, 1, 0).TotalMilliseconds))
                    .ContinueWith(t => DoWork());
                return;
            }

            Console.WriteLine("[WORKER BUSY]");

            while (db.KeyExists(_busyKey))
            {
                List<Task> tasks = new List<Task>();
                for (int ctr = 1; ctr <= 30; ctr++)
                {
                    tasks.Add(Task.Factory.StartNew(() => {
                        var fib = FibonacciNumber(60);
                        Console.WriteLine($" WORKER BUSY: {fib}");
                    }));
                }
                Task.WaitAll(tasks.ToArray());
            }

            DoWork();
        }

        private static decimal FibonacciNumber(decimal n)
        {
            decimal a = 0;
            decimal b = 1;
            decimal tmp;

            for (decimal i = 0; i < n; i++)
            {
                tmp = a;
                a = b;
                b += tmp;
            }

            return a;
        }
    }
}
