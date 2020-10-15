using System;
using System.Collections.Generic;
using System.Threading;
using Tanks.WorldOfTanks;

namespace TanksWar
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tank> t34 = new List<Tank>();
            Full(t34, "T - 34");

            List<Tank> tpantera = new List<Tank>();
            Full(tpantera, "Pantera");
            for (int i = 0; i < t34.Count; i++)
                Console.WriteLine(t34[i] ^ tpantera[i]);

        }
        static void Full(List<Tank> tanks, string name)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(50);
                tanks.Add(new Tank($"{name}({i+1})"));
            }
        }

    }
}
