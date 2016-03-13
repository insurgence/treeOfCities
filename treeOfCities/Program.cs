using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace treeOfCities
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\src.txt";
            
            if (!File.Exists(path))
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            else
            {
                List<string> lines = File.ReadLines(path).ToList();

                int x = 3;
                Console.WriteLine($"Value of x = {x}");
                
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
