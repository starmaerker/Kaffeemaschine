using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeeModell
{
    public class Behaelter
    {
        #region Klassenvariablen (Felder)

        public uint _volumen;

        public uint _fuellstand;


        #endregion

        #region Methoden

        /// <summary>
        /// etwas in Behälter einfüllen
        /// </summary>
        /// <param name="menge">die einzufüllende Menge</param>
        /// <returns>die tatsächlich eingefüllte Menge</returns>

        public uint Fuellen(uint menge)
        {
            if (menge < _volumen - _fuellstand)
            {
                _fuellstand += menge;
                return menge;
            }
            else
            {
                uint eingefuellt = _volumen - _fuellstand;
                _fuellstand = _volumen;
                return eingefuellt;
            }
        }

        /// <summary>
        /// Füllt den Behälter bis zum Rand
        /// </summary>
        /// <returns>die tatsächlich eingefüllte Menge</returns>
        public uint Fuellen()
        {
            return Fuellen(_volumen - _fuellstand);
        }
           

        #endregion
    }
}
