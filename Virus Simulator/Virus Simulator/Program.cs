using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Virus_Simulator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            const string fileName = "../../FileConfig.txt";
            int numOfCities;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Console.WriteLine("Hello World!");        
            try
            {
                StreamReader sr = new StreamReader(fileName);
                String line = sr.ReadLine();
                numOfCities = int.Parse(line);
                while (line != null)
                {
                    line = sr.ReadLine();
                    int count = 2;
                    char[] seperator = {' '};
                    String[] city = line.Split(seperator, count, StringSplitOptions.None);
                    foreach(String s in city)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine("done");
                }
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }
    }
}
