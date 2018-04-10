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
            Modul3Demos();
            
            //UserSettingsDemo();


            //todo Modul2 implementieren
            //Modul2Demos();


            Console.WriteLine();
            Console.Write("press any key to quit: ");
            Console.ReadKey();

        }

        private static void Modul3Demos()
        {
            Behaelter wasserBehaelter = new Behaelter(Zutat.Wasser);
            //wasserBehaelter.Typ = Zutat.Wasser; nicht möglich wegen Schreibschutz

            //todo Ereignisbehandlung 5: Ereignis abonnieren
            wasserBehaelter.BinLeer += Program.LeerstandAnzeigen;
            //Langnotation: wasserBehaelter.BinLeer += new BinLeerEventHandler(Program.LeerstandAnzeigen);
            Automat automat = new Automat();
            wasserBehaelter.BinLeer += automat.Auffuellen;

            wasserBehaelter.Fuellen();
            wasserBehaelter.Entnehmen(150);

            Console.WriteLine($"Aktueller Füllstand: {wasserBehaelter.Fuellstand} cl.");
        }

        //todo Ereignisbehandlung 4: Callback-Methode schreiben
        private static void LeerstandAnzeigen(Behaelter sender, EventArgs e)
        {
            Console.WriteLine($"Behälter mit {sender.Typ} ist leer.");
        }

        private static void UserSettingsDemo()
        {
            Console.ForegroundColor = Properties.Settings.Default.ConsolenFarbe;

            Console.WriteLine("Gewünschte Farbe: ");

            string gewuenschteFarbe = Console.ReadLine();

            if (Enum.TryParse(gewuenschteFarbe, out ConsoleColor farbe))
            {
                Console.ForegroundColor = farbe;
                Properties.Settings.Default.ConsolenFarbe = farbe;
                Properties.Settings.Default.Save();
            }
            else
            {
                Console.WriteLine("Die Farbe gibt es nicht.");
            }
        }

        private static void Modul2Demos()
        {
            Behaelter behaelter = new Behaelter(Zutat.Wasser);
            try
            {
                Console.WriteLine($"Eingefüllt wurden {behaelter.Fuellen(-30)} cl");
                behaelter.Volumen = 100;
                Console.WriteLine($"Eingefüllt wurden {behaelter.Fuellen(30)} cl");
                Console.WriteLine($"Eingefüllt wurden {behaelter.Fuellen()} cl");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                ;
            }

            while (true)
            {
                try
                {
                    Console.Write("Entnehmen: ");
                    string input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        break;
                    }
                    Console.WriteLine($"Entnommen sind {behaelter.Entnehmen(input)} cl.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);                    
                }
            }
        }
    }
}
