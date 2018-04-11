using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeeModell
{
    public class Rezept
    {
        public string Name { get; set; }

        public Dictionary<Inhaltsstoff, int> ZutatenListe { get; set; }

        public Rezept(string name)
        {
            this.Name = name;
            ZutatenListe = new Dictionary<Inhaltsstoff, int>();
        }

    }
}
