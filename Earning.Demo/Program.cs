using StackExchange.Redis;
using System;
using System.Timers;

namespace Earning.Demo
{
    class Program
    {
        static string REDIS_ITEM_KEY = "WORKER_KEY";

        static string CONNECTION_STRING = "earnindemo.redis.cache.windows.net:6380,password=aGxKWzVUlpzQLyvDOP8cXYC3MMl99zOsdMU8QtNqNi0=,ssl=True,abortConnect=False";

        static ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(CONNECTION_STRING);

        static void Main(string[] args)
        {
            Console.WriteLine($"MachineName: {Environment.MachineName}");
            Console.WriteLine("[WORKER STARTED]:" + (args.Length > 0 ? args[0] : String.Empty));
            using (connection)
            using (Timer timer = new Timer(10000))
            {

                timer.Elapsed += (sender, e) => HandleTimer();
                timer.Start();


                Console.Write("Press any key to exit... ");
                Console.ReadKey();
            }
        }

        private static void HandleTimer()
        {
            var value = Get();
            Console.WriteLine($" WORKER INCREMENT ACTION: {value}");

            Increment();
        }

        private static void Increment()
        {
            var db = connection.GetDatabase();
            var value = db.StringGet(REDIS_ITEM_KEY);
            db.StringSet(REDIS_ITEM_KEY, string.IsNullOrEmpty(value) ? 0 : int.Parse(value) + 1);
        }

        private static string Get()
        {
            var db = connection.GetDatabase();
            return db.StringGet(REDIS_ITEM_KEY);
        }
    }
}
