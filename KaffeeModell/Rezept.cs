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

        protected Rezept()
        {
            ZutatenListe = new Dictionary<Inhaltsstoff, int>();
        }

        public Rezept(string name)
            : this()
        {
            this.Name = name;            
        }

    }
}
