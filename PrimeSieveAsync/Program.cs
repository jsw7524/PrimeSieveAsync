using System;

namespace MyApp // Note: actual namespace depends on the project name.
{

    public class PrimeSieve
    {
        int _size;
        bool[] _notPrimeTable;
        int counterThread = 0;
        int interval = Environment.ProcessorCount;

        public PrimeSieve(int size)
        {
            _size = size;
            _notPrimeTable = new bool[size];
        }


        public void Sieve(int x)
        {
            int tmp = 2 * x;
            while (tmp < _size)
            {
                _notPrimeTable[tmp] = true;
                tmp += x;
            } 
        }

        public void SetInterval(int tid)
        {
            Console.WriteLine($"thread id:{tid}");
            int t = tid;
            while (t < _size)
            {
                if (_notPrimeTable[t] == false)
                {
                    Sieve(t);
                }

                t += interval;
            }
        }

        public void Run()
        {
            List<Task> jobs = new List<Task>();
            for (int i = 2; i < Environment.ProcessorCount; i++)
            {
                int tmp = i;

                jobs.Add(Task.Run(() => SetInterval(tmp)));
            }
            Task.WaitAll(jobs.ToArray());
        }
    }

    public class MultiAwait
    {
        public async void run()
        {
            List<Task> tasks = new List<Task>();

            tasks.Add(Task.Run(() => { Thread.Sleep(3000); Console.WriteLine("A"); }));
            tasks.Add(Task.Run(() => { Thread.Sleep(5000); Console.WriteLine("B"); }));
            tasks.Add(Task.Run(() => { Thread.Sleep(1000); Console.WriteLine("C"); }));

            foreach (Task task in tasks)
            {
                await task;
            }
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            PrimeSieve ps = new PrimeSieve(500000000);
            ps.Run();
            Console.WriteLine("Hello World!");
            //MultiAwait multiAwait = new MultiAwait();
            //multiAwait.run();
            //Console.WriteLine("Hello World!");
            //while (true) ;
        }
    }
}