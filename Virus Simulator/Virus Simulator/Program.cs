using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Virus_Simulator
{
    public class Algo
    {
        public void BFS<T>(Graph<T> G, T S)
        {
            //Inisialisasi Kota awal
            int K0 = 0; // Masukkan user, K0 adalah indeks kota awal
            
            //Make Array Time BFS
            int[] Time = new int[G.Size];
            Time[K0] = 0;
            for (int i=0; i<=G.Size; i++) {
                if (i!=K0) {
                    Time[i] = int.MaxValue;
                }
            }




   
              
        }
    }


    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Graph<City> Country = new Graph<City>();
            List<City> Cities = new List<City>();
            City firstInfectedCity = new City();
            ReadCities(Cities, firstInfectedCity);
            foreach (City c in Cities)
            {
                Country.AddNode(c);
            }
            ReadCitiesConnection(Country);
        }

        static void ReadCities(List<City> Cities, City firstInfectedCity)
        {
            const string fileName = "../../CitiesInfo.txt";
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                int count = 2;
                char[] seperator = { ' ' };
                String[] cities = lines[0].Split(seperator, count, StringSplitOptions.RemoveEmptyEntries);
                int numOfCities = int.Parse(cities[0]);
                string firstInfectedCityName = cities[1];
                for (int i = 1; i < lines.Length; i++)
                {
                    cities = lines[i].Split(seperator, count, StringSplitOptions.RemoveEmptyEntries);
                    String cityName = cities[0];
                    int cityPopulation = int.Parse(cities[1]);
                    City tempCity = new City(cityName, cityPopulation);
                    if(cityName == firstInfectedCityName)
                    {
                        firstInfectedCity.name = cityName;
                        firstInfectedCity.population = cityPopulation;
                    }
                    Cities.Add(tempCity);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        static void ReadCitiesConnection(Graph<City> Country)
        {
            const string fileName = "../../CitiesConnection.txt";
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                int numOfConnection = int.Parse(lines[0]);
                for (int i = 1; i < lines.Length; i++)
                {
                    int count = 3;
                    char[] seperator = { ' ' };
                    String[] connection = lines[i].Split(seperator, count, StringSplitOptions.RemoveEmptyEntries);
                    int from = 0, to = 0;
                    for (int c = 0; c < Country.Size; c++)
                    {
                        if(Country[c].name == connection[0])
                        {
                            from = c;
                        }
                        if (Country[c].name == connection[1])
                        {
                            to = c;
                        }
                    }
                    Country.ConnectNodes(from, to, float.Parse(connection[2]));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }

}
