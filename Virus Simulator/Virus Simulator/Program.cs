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
        public double I(int Pa, int ta) // Populasi kota A (P(A)), ta menunjukkan waktu hari t(a)
        {
            return Convert.ToDouble(Pa)/(1+(Pa-1)* Math.Pow(2.71828,0.25*ta));
        }
        public double S(int Pa, int ta, int Tr) {
            return I(Pa, ta) * Tr;
        }
        public int lamaWaktuNyebar(int Pa, int Tr) {
            //belom jadi
            return Tr;
        }

        public void BFS<T>(Graph<T> G)
        {
            //Inisialisasi Kota awal
            int K0 = 0;  // Masukkan user, K0 adalah indeks kota awal
            int Tot = 3; // Masukkan user, T adalah jumlah hari total
            int Pa = 0;  //Masukkan User, Misalkan Pa adalah peluang Pa;
            int Tr = 0; // Masukkan user Tr;
            Queue<Graph<T>.AdjacentNodes<T>> QueueKota = new Queue<Graph<T>.AdjacentNodes<T>>();
            Graph<T>.AdjacentNodes<T>[] adjacentNodes = G.Adjacent(K0);
            foreach (Graph<T>.AdjacentNodes<T> adjacentNode in adjacentNodes) {
                QueueKota.Enqueue(adjacentNode);
            }

            //Make Array Time BFS
            int[] Time = new int[G.Size];
            Time[K0] = 0;
            for (int i=0; i<=G.Size; i++) {
                if (i!=K0) {
                    Time[i] = int.MaxValue;
                }
            }

            int waktusebar = 0;
            while (QueueKota.Count!=0)
            {
                Graph<T>.AdjacentNodes<T> KotaPop = QueueKota.Dequeue();
                int ta;
                ta = Tot - Time[K0];
                
                //Check apakah virus menyebar
                if (S(Pa,ta,Tr)>1)
                {
                    //Cari lama waktu menyebar
                    waktusebar = lamaWaktuNyebar(Pa, Tr) + Time[0];
                    if (waktusebar > Time[KotaPop]) { //error instance
                        //Do nothing
                    }
                    else{
                        Time[KotaPop] = waktusebar; // error instace
                        QueueKota.Enqueue(KotaPop);
                    }
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
            foreach (City city in Cities)
            {
                Country.AddNode(city);
            }
            ReadCitiesConnection(Country);

            // Show Form
            Form1 form = new Form1();
            form.ShowDialog();
        }

        public static void ReadCities(List<City> Cities, City firstInfectedCity)
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

        public static void ReadCitiesConnection(Graph<City> Country)
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

        public static void DrawInfected(Microsoft.Msagl.Drawing.Graph graph, string city) 
        {
            graph.FindNode(city).Attr.Color = Microsoft.Msagl.Drawing.Color.Crimson;
        }

        public static void DrawInfection(Microsoft.Msagl.Drawing.Graph graph, string from, string to)
        {
            var edge = graph.FindNode(from).OutEdges.Where(e => e.Target == to).Single();
            DrawInfected(graph, edge.Source);
            DrawInfected(graph, edge.Target);
            edge.Attr.Color = Microsoft.Msagl.Drawing.Color.Crimson;
        }
    }

}
