using System;

namespace Tanks.WorldOfTanks
{
    public class Tank
    {
        public string name { get; private set; }
        public int armor { get; private set; }
        public int manevr { get; private set; }
        public int boekomleke { get; private set; }
        Random random = new Random();
        public Tank(string name)
        {
            this.name = name;
            armor = random.Next(1, 100);
            manevr = random.Next(1, 100);
            boekomleke = random.Next(1, 100);
        }
        public static string operator ^(Tank tank1, Tank tank2)
        { 
            int victory1=0;
            int victory2=0;
            if (tank1.armor > tank2.armor)
                victory1++;
            else
                victory2++;
            if (tank1.boekomleke>tank2.boekomleke)
                victory1++;
            else
                victory2++;
            if (tank1.manevr>tank2.manevr)
                victory1++;
            else
                victory2++;
            if (victory1 > victory2)
                return tank1.name + "  VICTORY";
            else
                return tank2.name + "  VICTORY";
        }
    }
}
