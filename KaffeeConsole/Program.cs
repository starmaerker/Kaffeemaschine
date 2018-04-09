using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaffeeModell;

namespace KaffeeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //todo Modul2 implementieren
            Modul2Demos();
            
            
            Console.WriteLine();
            Console.Write("press any key to quit: ");
            Console.ReadKey();            
            
        }

        private static void Modul2Demos()
        {
            Behaelter behaelter = new Behaelter();
            Console.WriteLine($"Eingefüllt wurden {behaelter.Fuellen(30)} cl");
            behaelter._volumen = 100;
            Console.WriteLine($"Eingefüllt wurden {behaelter.Fuellen(30)} cl");
            Console.WriteLine($"Eingefüllt wurden {behaelter.Fuellen()} cl");
            //Console.WriteLine($"Nach Entnahme sind noch {behaelter.Entnehmen(40)} cl vorhanden.");
            //Console.WriteLine($"Nach Entnahme sind noch {behaelter.Entnehmen(70)} cl vorhanden.");

            while (true)
            {
                Console.Write("Entnehmen: ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }
                Console.WriteLine($"Entnommen sind {behaelter.Entnehmen(input)} cl.");
            }
        }
    }
}
