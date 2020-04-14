using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
namespace dojoExo2MultiThreading
{
    class Program
    {
        private static CountdownEvent _countdown;
        private static Int32 _threadsCount;
        //public static List<int> numberList { get; set; } = new List<int>();
        public static ConcurrentQueue<int> numberList = new ConcurrentQueue<int>();
        public static Random random = new Random();
        public static int totalSum = 0;
        public static void Main(string[] args)
        {
            var program = new Program(50);
            program.Run();
            _countdown.Wait();
            DepileNumber();
            Console.WriteLine("Total = " + totalSum);
            Console.ReadLine();
        }
        public Program(Int32 threadsCount)
        {
            _threadsCount = threadsCount;
            _countdown = new CountdownEvent(threadsCount); // Set the counter to the number of threads executing
        }
        public void Run()
        {
            for (int i = 0; i < _threadsCount ; i++)
            {
                ThreadPool.QueueUserWorkItem(x => AddNumber());
            }
        }
        public void AddNumber()
        {
            Console.WriteLine("Ajout d'un nombre - dt");
            int randomNumber = random.Next(1, 50);
            numberList.Enqueue(randomNumber);
            _countdown.Signal();
            Console.WriteLine("Ajout d'un nombre - ft");
        }
        public static void DepileNumber()
        {
            Console.WriteLine("Fait une addition - dt");
            int i;
            int cpt = 0;
            while (numberList.TryDequeue(out i))
            {
                totalSum = totalSum + i;
                cpt++;
            }
            Console.WriteLine("Fait une addition - ft");
        }
    }
}