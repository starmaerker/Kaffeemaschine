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

        public string Zubereiten(string rezeptName)
        {
            Rezept gewuenschtesRezept = 
            RezeptList.Where(r => r.Name == rezeptName)
                .FirstOrDefault();

            if (gewuenschtesRezept == null)
            {
                return $"{rezeptName} leider nicht verfügbar";
            }

            //Überprüfen, ob für alle Zutaten ein passender Behälter mit genügend Inhalt vorhanden ist.


            bool alleZutatenInAusreichenderMengeVorhanden =
            gewuenschtesRezept.ZutatenListe
                .All(zutat => BehaelterListe.Any(b =>
               (b.Typ == zutat.Key) && (b.Fuellstand >= zutat.Value)));

            if (!alleZutatenInAusreichenderMengeVorhanden)
            {
                return $"Nicht alle Zutaten für {rezeptName} vorhanden";
            }

            foreach (KeyValuePair<Inhaltsstoff, int> zutat in gewuenschtesRezept.ZutatenListe)
            {
                BehaelterListe
                    .FirstOrDefault(b => b.Typ == zutat.Key)?
                    .Entnehmen(zutat.Value);
            }

            return $"{rezeptName} bereit.";
        }

        //todo Ereignisbehandlung 4: Callback-Methode schreiben
        public void Auffuellen(Behaelter sender, EventArgs e)
        {
            sender.Fuellen();
        }
    }

}
