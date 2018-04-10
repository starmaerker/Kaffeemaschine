using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeeModell
{
    public class Automat
    {
        //todo Ereignisbehandlung 4: Callback-Methode schreiben
        public void Auffuellen(Behaelter sender, EventArgs e)
        {
            sender.Fuellen();
        }
    }

}
