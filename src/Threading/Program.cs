using System;
using System.Threading;

namespace Threading
{
    //A simple demonstration of multi threading
    class Program
    {
        static void Main(string[] args)
        {
            var testThread1Start = new ThreadStart(new Program().TestThread1);
            var testThread2Start = new ThreadStart(new Program().TestThread2);

            Thread[] testThread = new Thread[2];
            testThread[0] = new Thread(testThread1Start);
            testThread[1] = new Thread(testThread2Start);

            foreach (Thread myThread in testThread)
            {
                myThread.Start();
            }

            Console.ReadLine();
        }

        public void TestThread1()
        {
            //executing in thread
            int count = 0;
            while (count++ < 10)
            {
                Console.WriteLine("Thread 1 Executed " + count + " times");
                Thread.Sleep(1);
            }
        }

        public void TestThread2()
        {
            //executing in thread
            int count = 0;
            while (count++ < 10)
            {
                Console.WriteLine("Thread 2 Executed " + count + " times");
                Thread.Sleep(1);
            }
        }
    }
}
