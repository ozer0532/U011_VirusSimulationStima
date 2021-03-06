﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Virus_Simulator
{
    public class Algo
    {
        public static double I(int Pa, int ta) // Populasi kota A (P(A)), ta menunjukkan waktu hari t(a)
        {
            return Convert.ToDouble(Pa)/(1+(Pa-1)* Math.Pow(Math.E,-0.25*ta));
        }
        public static double S(int Pa, int ta, float Tr) {
            return I(Pa, ta) * Tr;
        }
        public static int lamaWaktuNyebar(int Pa, float Tr) {
            double dPa = Pa;
            double dTr = Tr;
            double waktuSebar = -4 * Math.Log((dPa * dTr - 1) / (dPa - 1));
            int ceilWaktuSebar = (int)Math.Ceiling(waktuSebar);
            return (waktuSebar == ceilWaktuSebar) ? ceilWaktuSebar + 1 : ceilWaktuSebar;
        }

        public static Graph<T>.AdjacentNodes<T>[] BFS<T>(Graph<T> graph, int K0, int Tot)
        {
            //Inisialisasi Kota awal
            Graph<City> G = graph as Graph<City>;
            Queue<Graph<City>.AdjacentNodes<City>> QueueKota = new Queue<Graph<City>.AdjacentNodes<City>>();
            Graph<City>.AdjacentNodes<City>[] adjacentNodes = G.Adjacent(K0);
            foreach (Graph<City>.AdjacentNodes<City> adjacentNode in adjacentNodes) {
                QueueKota.Enqueue(adjacentNode);
            }
            List<Graph<City>.AdjacentNodes<City>> infectionPath = new List<Graph<City>.AdjacentNodes<City>>();

            //Make Array Time BFS
            int[] Time = new int[G.Size];
            Time[K0] = 0;
            for (int i=0; i<G.Size; i++) {
                if (i!=K0) {
                    Time[i] = int.MaxValue;
                }
            }

            int waktusebar = 0;
            while (QueueKota.Count!=0)
            {
                Graph<City>.AdjacentNodes<City> KotaPop = QueueKota.Dequeue();
                int ta;
                ta = Tot - Time[G.FindNodeIndex(n => n.item.name == KotaPop.first.name)];
                Console.WriteLine(KotaPop.first.name + " -> " + KotaPop.second.name);
                Console.WriteLine(KotaPop.first.population);
                Console.WriteLine(ta);
                Console.WriteLine(KotaPop.weight);
                Console.WriteLine(S(KotaPop.first.population, ta, KotaPop.weight));
                Console.WriteLine(lamaWaktuNyebar(KotaPop.first.population, KotaPop.weight) + Time[G.FindNodeIndex(n => n.item.name == KotaPop.first.name)]);
                //Check apakah virus menyebar
                if (S(KotaPop.first.population,ta,KotaPop.weight) > 1)
                {
                    //Cari lama waktu menyebar
                    waktusebar = lamaWaktuNyebar(KotaPop.first.population, KotaPop.weight) + Time[G.FindNodeIndex(n => n.item.name == KotaPop.first.name)];
                    if (waktusebar >= Time[G.FindNodeIndex(n => n.item.name == KotaPop.second.name)]) {
                        //Do nothing
                    }
                    else{
                        // Hapus path lama
                        infectionPath.RemoveAll(adj => adj.second == KotaPop.second);
                        // Tambah path baru
                        infectionPath.Add(KotaPop);

                        // Update awal penyebaran
                        Time[G.FindNodeIndex(n => n.item.name == KotaPop.second.name)] = waktusebar;

                        // Push neighbors
                        var neighbors = G.Adjacent(G.FindNodeIndex(n => n.item.name == KotaPop.second.name));
                        foreach (var neighbor in neighbors)
                        {
                            QueueKota.Enqueue(neighbor);
                        }

                        Console.WriteLine("Duar kena korona " + KotaPop.first.name + " -> " + KotaPop.second.name);
                    }
                }
            }
            return infectionPath.ToArray() as Graph<T>.AdjacentNodes<T>[];
        }
    }
    static class Program
    {
        public static Graph<City> Country;
        public static City firstInfectedCity;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
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
