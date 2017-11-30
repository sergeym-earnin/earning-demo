using Earning.Demo.Shared.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Earning.Demo
{
    class Program
    {
        const int Second = 1000;
        static IConfigurationService Configuration = new ConfigurationService();
        static ConnectionMultiplexer Connection = ConnectionMultiplexer.Connect(Configuration.RedisConnectionString);

        static string _redisKey;
        static string _busyKey;


        static Task nextTask;
        static Task _refreshCounterTask;

        static volatile bool _isWorkerShouldBeBusy = false;

        static object _syncRoot = new object();

        public static bool IsWorkerShouldBeBusy
        {
            get
            {
                lock (_syncRoot)
                {
                    return _isWorkerShouldBeBusy;
                }
            }
            set
            {
                lock (_syncRoot)
                {
                    _isWorkerShouldBeBusy = value;
                }
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("[WORKER STARTED]");

            var enviroment = new EnviromentService(Configuration);
            enviroment.StartTracking(Configuration.WorkerRedisKey);

            _redisKey = $"{enviroment.GetDataKey(Configuration.WorkerRedisKey)}";
            _busyKey = Configuration.WorkerBusyKey;

            _refreshCounterTask = StartMonitorBusyKeyTask();
            DoWork();

            while (true)
            {
                Thread.Sleep(Second);
            }
        }

        private static Task StartMonitorBusyKeyTask()
        {
            var refreshTask = new Task(() =>
            {
                var db = Connection.GetDatabase();
                while (true)
                {
                    IsWorkerShouldBeBusy = db.KeyExists(_busyKey);
                    Thread.Sleep(Second);
                }
            }, TaskCreationOptions.LongRunning);
            refreshTask.Start();
            return refreshTask;
        }

        private static void DoWork()
        {
            var db = Connection.GetDatabase();
            var value = db.StringGet(_redisKey);

            Console.WriteLine($"IsWorkerBusy: {IsWorkerShouldBeBusy}");

            if (!IsWorkerShouldBeBusy)
            {
                Console.WriteLine($" WORKER INCREMENT ACTION: {value}");
                db.StringIncrement(_redisKey, 1);
                nextTask = Task.Delay((int)(new TimeSpan(0, 0, 10).TotalMilliseconds))
                    .ContinueWith(t => DoWork());
                return;
            }

            Console.WriteLine("[WORKER BUSY]");

            ulong _counter = 0;

            while (IsWorkerShouldBeBusy)
            {
                _counter++;
                long fib = 0;
                List<Task> tasks = new List<Task>();
                for (int ctr = 1; ctr <= 30; ctr++)
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        for (int i = 1; i < 100; i++)
                        {
                            
                            var result = FibonacciNumber(120);
                            Interlocked.Exchange(ref fib, result);
                        }
                    }));
                }
                Task.WaitAll(tasks.ToArray());
                Console.WriteLine($" WORKER BUSY: {_counter} result: {fib}. Is Busy: {IsWorkerShouldBeBusy} ");
            }

            DoWork();
        }

        private static long FibonacciNumber(long n)
        {
            long a = 0;
            long b = 1;
            long tmp;

            for (long i = 0; i < n; i++)
            {
                tmp = a;
                a = b;
                b += tmp;
            }

            return a;
        }
    }
}
