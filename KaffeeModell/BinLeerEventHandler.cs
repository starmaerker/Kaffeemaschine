using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeeModell
{
    //todo Ereignisbehandlung 1: Delegate-Typ festlegen
    //Typ für das Ereignis selbst. Beschreibt das Aussehen der Callback-Methoden
    public delegate void BinLeerEventHandler(Behaelter sender, EventArgs e);
    
}
