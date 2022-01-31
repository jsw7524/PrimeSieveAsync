using System;

namespace MyApp // Note: actual namespace depends on the project name.
{

    public class PrimeSieve
    {
        int _size;
        bool[] _notPrimeTable;

        //SemaphoreSlim semaphoreSlim = new SemaphoreSlim(Environment.ProcessorCount, Environment.ProcessorCount);
        SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);


        public PrimeSieve(int size)
        {
            _size = size;
            _notPrimeTable = new bool[size];
        }


        public void Sieve(int x)
        {
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            int tmp = 2*x;
            while (tmp < _size)
            {
                _notPrimeTable[tmp] = true;
                tmp += x;
            }
        }

        public async Task SieveAsync(int x)
        {
            semaphoreSlim.Wait();
            await Task.Run(() => Sieve(x));
            semaphoreSlim.Release();
        }

        public void Run()
        {
            
            Sieve(2);
            for (int i = 3; i < _size; i++)
            {
                if (_notPrimeTable[i]==false)
                {
                    Sieve(i);
                }
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
        }
    }
}