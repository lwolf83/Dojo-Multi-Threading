using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace dojoMultiThreading
{
    class Program
    {
        private static Mutex mutex = new Mutex();
        public static Random random { get; set; } = new System.Random();

        static void Main(string[] args)
        {
           
            var threads = new List<Thread>();
            for(int i = 0; i < 3; i++)
            {
                var threadStartDelegate = new ThreadStart(OnThreadStart);
                threads.Add(new Thread(threadStartDelegate));
            }

            threads.ForEach(t => t.Start());

            foreach (var t in threads)
            {
                t.Join(); 
            }
            Console.ReadLine();
        }

        private static void OnThreadStart()
        {
            Console.WriteLine("Thread number{0} is activated", Thread.CurrentThread.ManagedThreadId);
            var executionTime = random.Next(10000);
            Thread.Sleep(executionTime);
            Console.WriteLine("Thread number{0} is finished ({1})", Thread.CurrentThread.ManagedThreadId, executionTime);
        }
    }
}
