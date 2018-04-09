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

        public int _volumen;

        public int _fuellstand;


        #endregion

        #region Methoden

        /// <summary>
        /// etwas in Behälter einfüllen
        /// </summary>
        /// <param name="menge">die einzufüllende Menge</param>
        /// <returns>die tatsächlich eingefüllte Menge</returns>

        public int Fuellen(int menge)
        {
            if (menge < _volumen - _fuellstand)
            {
                _fuellstand += menge;
                return menge;
            }
            else
            {
                int eingefuellt = _volumen - _fuellstand;
                _fuellstand = _volumen;
                return eingefuellt;
            }
        }

        /// <summary>
        /// Füllt den Behälter bis zum Rand
        /// </summary>
        /// <returns>die tatsächlich eingefüllte Menge</returns>
        public int Fuellen()
        {
            return Fuellen(_volumen - _fuellstand);
        }

        public int Entnehmen(int menge)
        {
            if (menge < _fuellstand)
            {
                _fuellstand -= menge;

                return menge;
            }
            else
            {
                return _fuellstand;
            }
        }

        public int Entnehmen(string menge)
        {
            int mengeAlsInt = int.Parse(menge);
            return Entnehmen(mengeAlsInt);
        }
           

        #endregion
    }
}
