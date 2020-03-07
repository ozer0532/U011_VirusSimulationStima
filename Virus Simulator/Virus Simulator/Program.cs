using System;
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
        static void Main() {
            Console.WriteLine("Hello World!");
        }
    }
}