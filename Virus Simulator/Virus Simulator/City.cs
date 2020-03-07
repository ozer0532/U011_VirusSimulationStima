using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virus_Simulator
{
    class City
    {
        public string name;
        public int population;

        public City()
        {
            this.name = "Bandung";
            this.population = 0;
        }

        public City(string name, int population)
        {
            this.name = name;
            this.population = population;
        }

        public void PrintInfo()
        {
            Console.Write("Country: ");
            Console.Write(this.name);
            Console.Write(" - Population: ");
            Console.WriteLine(this.population);
        }
    }
}
