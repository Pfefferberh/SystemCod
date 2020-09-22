using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _22._09._20_Fibonachi
{
    class Program
    {

        static long sum_Fact = 1;
        static long sum_Fibs =1;
        static long sum_Sqw = 1;
        static void Main(string[] args)
        {
            Mutex mutex = new Mutex();
            Thread[] threads = new Thread[4];
            Console.WriteLine($"Fibonach    Sqw      Factor");
            for (int i = 0; i < 20; i++)
            {
                mutex.WaitOne();
                try
                {
                    threads[0] = new Thread(Factorial);
                    threads[0].Start(i);
                threads[1] = new Thread(Fibonachi);
                    threads[1].Start(i);
                threads[2] = new Thread(Sqw);
                    threads[2].Start(i); 
                    threads[3] = new Thread(Show);
                    threads[3].Start();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
                threads[0].Join();
                threads[1].Join();
                threads[2].Join();
                threads[3].Join();
            
           
        }
        static void Factorial(object digit)
        {
            sum_Fact = 1;
            for (int i = 1; i < (int)digit; i++)
            {
                sum_Fact *= i;
            }
        }
        static void Fibonachi(object digit)
        {
            List<long> sum_Fib = new List<long>() { 1, 1 };
            for (int i = 2; i < (int)digit; i++)
            {
                sum_Fib.Add(sum_Fib[i-1]+ sum_Fib[i-2]);
            }
            sum_Fibs = sum_Fib.Last();
            sum_Fib.Clear();
        }
        static void Sqw(object digit)
        {
            sum_Sqw = (int)digit * (int)digit;
        }
        static void Show(object digit)
        {
            Console.WriteLine($" {sum_Fibs}           {sum_Sqw}         {sum_Fact}   ");
        }

    }
}
