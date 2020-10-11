using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHW
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ведите предложение");
            string str = Console.ReadLine();
            Task[] task = new Task[5] {
            new Task(()=>Console.WriteLine($"Количестве предложений {str.Split(".?!".ToArray(),StringSplitOptions.RemoveEmptyEntries).Length.ToString()}")),
            new Task(()=>Console.WriteLine($"Количестве символов  {str.Count().ToString()}")),
            new Task(()=>Console.WriteLine($"Количестве слов  {str.Split(" .!?,".ToArray(),StringSplitOptions.RemoveEmptyEntries).Length.ToString()}")),
            new Task(()=>Console.WriteLine($"Количестве вопросительных предложений  {str.Count(x=>x=='?').ToString()}")),
            new Task(()=>Console.WriteLine($"Количестве восклицательных предложений  {str.Count(x=>x=='!').ToString()}")),
            };
            string gg = null;
            do
            {
                Console.WriteLine("0.Количестве предложений\n1.Количестве символов \n2.Количестве слов \n3.Количестве вопросительных предложений \n4.Количестве восклицательных предложений\nВийти");
                gg = Console.ReadLine();
                switch (gg)
                {
                    case "0":
                        task[0].Start();
                        break;
                    case "1":
                        task[1].Start();
                        break;
                    case "2":
                        task[2].Start();
                        break;
                    case "3":
                        task[3].Start();
                        break;
                    case "4":
                        task[4].Start();
                        break;
                    case "5":
                        gg = "5";
                        break;
                    default:
                        break;
                }
            } while (gg != "5");
            Console.ReadLine();
        }
    }
}
