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
        static readonly IConfigurationService Configuration = new ConfigurationService();
        static readonly ConnectionMultiplexer Connection = ConnectionMultiplexer.Connect(Configuration.RedisConnectionString);

        static string _redisKey;
        static string _busyKey;

        static Task nextTask;
        static Task _refreshBusyFlagTask;

        static volatile bool _isWorkerShouldBeBusy = false;

        static void Main(string[] args)
        {
            Console.WriteLine("[WORKER STARTED]");
            Connection.PreserveAsyncOrder = false;

            var enviroment = new EnviromentService(Configuration);
            enviroment.StartTracking(Configuration.WorkerRedisKey);

            _redisKey = $"{enviroment.GetDataKey(Configuration.WorkerRedisKey)}";
            _busyKey = Configuration.WorkerBusyKey;

            _refreshBusyFlagTask = StartMonitorBusyKeyTask();
            DoWork();

            while (true)
            {
                Thread.Sleep(Second);
                if (_refreshBusyFlagTask.IsFaulted)
                {
                    Console.WriteLine("Refresh Faulted");
                }

                if (_refreshBusyFlagTask.IsCompleted)
                {
                    Console.WriteLine("Refresh Completed");
                }
            }
        }

        private static Task StartMonitorBusyKeyTask()
        {
            var refreshTask = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        var db = Connection.GetDatabase();
                        bool oldValue = _isWorkerShouldBeBusy;
                        _isWorkerShouldBeBusy = db.KeyExists(_busyKey);
                        if (oldValue != _isWorkerShouldBeBusy)
                        {
                            Console.WriteLine("Is Busy changed to " + _isWorkerShouldBeBusy);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    Thread.Sleep(3 * Second);
                }
            }, TaskCreationOptions.LongRunning);
            refreshTask.Start();
            return refreshTask;
        }

        private static void DoWork()
        {
            var db = Connection.GetDatabase();
            var value = db.StringGet(_redisKey);

            Console.WriteLine($"IsWorkerBusy: {_isWorkerShouldBeBusy}");

            if (!_isWorkerShouldBeBusy)
            {
                Console.WriteLine($" WORKER INCREMENT ACTION: {value}");
                try
                {
                    db.StringIncrement(_redisKey, 1);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                nextTask = Task.Delay((int)(new TimeSpan(0, 0, 10).TotalMilliseconds))
                    .ContinueWith(t => DoWork());
                return;
            }

            Console.WriteLine("[WORKER BUSY]");

            ulong _counter = 0;

            while (_isWorkerShouldBeBusy)
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
                if (_counter % 5000 == 0)
                {
                    Console.WriteLine($" WORKER BUSY: {_counter} result: {fib}. Is Busy: {_isWorkerShouldBeBusy} ");
                }
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
