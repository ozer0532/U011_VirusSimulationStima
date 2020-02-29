using System;
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
            Queue<Graph<T>.AdjacentNodes<T>> QueueKota = new Queue[G.Size];
            QueueKota<T>.Append<T>(G.Adjacent(K0));

            
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
            Console.WriteLine("Hello World!");
        }
    }
}
