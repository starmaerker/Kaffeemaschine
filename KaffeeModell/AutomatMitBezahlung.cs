using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeeModell
{
    public class AutomatMitBezahlung : Automat 
    {
        public decimal GeldEinwurf { get; set; }

        public override string Zubereiten(string rezeptName, out bool erledigt, IProgress<int> progress)
        {
            erledigt = false;

            if (GeldEinwurf < 0.5m)
            {
                return $"Bitte noch {0.5m - GeldEinwurf:C} einwerfen.";
            }

            string ergebnis = base.Zubereiten(rezeptName, out erledigt);

            if (erledigt)
            {
                GeldEinwurf -= 0.5m;
            }

            return ergebnis;
        }
    }
}
