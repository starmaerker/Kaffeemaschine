using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeeModell
{
    public class Automat
    {
        public List<Behaelter> BehaelterListe { get; set; }

        public List<Rezept> RezeptList { get; set; }

        public Automat()
        {
            BehaelterListe = new List<Behaelter>();
            RezeptList = new List<Rezept>();
        }

        public virtual Task<string> ZubereitenAsync(string rezeptName, IProgress<int> progress)
        {
            Task<string> task = Task.Run<string>(() => {            
                return Zubereiten(rezeptName, out bool erledigtTemp, progress);              
              
            });
            return task;
        }

        public virtual string  Zubereiten(string rezeptName, out bool erledigt, IProgress<int> progress = null)
        {
            progress?.Report(0);

            erledigt = false;

            Rezept gewuenschtesRezept = 
            RezeptList.Where(r => r.Name == rezeptName)
                .FirstOrDefault();

            if (gewuenschtesRezept == null)
            {
                return $"{rezeptName} leider nicht verfügbar";
            }

            Task.Delay(500).Wait();
            progress?.Report(20);

            //Überprüfen, ob für alle Zutaten ein passender Behälter mit genügend Inhalt vorhanden ist.

            bool alleZutatenInAusreichenderMengeVorhanden =
            gewuenschtesRezept.ZutatenListe
                .All(zutat => BehaelterListe.Any(b =>
               (b.Typ == zutat.Key) && (b.Fuellstand >= zutat.Value)));

            if (!alleZutatenInAusreichenderMengeVorhanden)
            {
                return $"Nicht alle Zutaten für {rezeptName} vorhanden";
            }

            Task.Delay(500).Wait();
            progress?.Report(40);

            foreach (KeyValuePair<Inhaltsstoff, int> zutat in gewuenschtesRezept.ZutatenListe)
            {
                BehaelterListe
                    .FirstOrDefault(b => b.Typ == zutat.Key)?
                    .Entnehmen(zutat.Value);
            }

            erledigt = true;

            Task.Delay(1500).Wait();
            progress?.Report(100);


            return $"{rezeptName} bereit.";
        }

        //todo Ereignisbehandlung 4: Callback-Methode schreiben
        public void Auffuellen(Behaelter sender, EventArgs e)
        {
            sender.Fuellen();
        }

        public static Automat ErstelleStandardAutomat()
        {
            Automat a = new Automat();
            a.BehaelterListe.Add(new Behaelter(Inhaltsstoff.Kaffee));
            a.BehaelterListe.Add(new Behaelter(Inhaltsstoff.Kakao));
            a.BehaelterListe.Add(new Behaelter(Inhaltsstoff.Milch));
            a.BehaelterListe.Add(new Behaelter(Inhaltsstoff.Wasser));

            Rezept r;

            r = new Rezept("Kaffee");
            r.ZutatenListe.Add(Inhaltsstoff.Wasser, 15);
            r.ZutatenListe.Add(Inhaltsstoff.Kaffee, 5);
            a.RezeptList.Add(r);

            r = new Rezept("Milchkaffee");
            r.ZutatenListe.Add(Inhaltsstoff.Wasser, 10);
            r.ZutatenListe.Add(Inhaltsstoff.Kaffee, 5);
            r.ZutatenListe.Add(Inhaltsstoff.Milch, 5);
            a.RezeptList.Add(r);

            r = new Rezept("Espresso");
            r.ZutatenListe.Add(Inhaltsstoff.Wasser, 5);
            r.ZutatenListe.Add(Inhaltsstoff.Kaffee, 15);
            a.RezeptList.Add(r);

            r = new Rezept("Cappuccino");
            r.ZutatenListe.Add(Inhaltsstoff.Wasser, 5);
            r.ZutatenListe.Add(Inhaltsstoff.Kaffee, 5);
            r.ZutatenListe.Add(Inhaltsstoff.Milch, 5);
            r.ZutatenListe.Add(Inhaltsstoff.Kakao, 5);
            a.RezeptList.Add(r);

            foreach (Behaelter b in a.BehaelterListe)
            {
                b.Fuellen();
            }

            return a;
        }
    }

}
