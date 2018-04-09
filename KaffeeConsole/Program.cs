using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
            if (args.Length >= 2)
            {
                Console.WriteLine(args[0] + " " + args[1]); 
            }
            else
            {
                Console.WriteLine("Hallo Welt");
            }
            
            Console.WriteLine();
            Console.Write("press any key to quit: ");
            Console.ReadKey();            
            
        }
        
    }
}
