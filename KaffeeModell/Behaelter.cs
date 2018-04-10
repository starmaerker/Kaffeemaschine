﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeeModell
{
    public class Behaelter : IComparable
    {
        #region Klassenvariablen (Felder) und Eingenschaften (Properties)

        private int _volumen;
        private int _fuellstand;
        private Zutat _typ;        

        public int Volumen
        {
            get { return _volumen; }
            set { _volumen = value; }
        }

        public int Fuellstand
        {
            get { return _fuellstand; }
            private set { _fuellstand = value; }
        }

        public Zutat Typ
        {
            get { return _typ; }
            private set { _typ = value; }
        }


        #endregion

        #region Kontruktoren

        public Behaelter(Zutat typ, int volumen = 100)
        {
            this.Typ = typ;
            this.Volumen = volumen;
        }

        #endregion

        #region Ereignisse

        //todo Ereignisbehandlung 2: Ereignis deklarieren
        //event schränkt Zugriff auf invocation list ein
        public event BinLeerEventHandler BinLeer;

        #endregion

        #region Methoden

        /// <summary>
        /// etwas in Behälter einfüllen
        /// </summary>
        /// <param name="menge">die einzufüllende Menge</param>
        /// <returns>die tatsächlich eingefüllte Menge</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>

        public int Fuellen(int menge)
        {
            if (menge <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(menge), menge, "Die einzufüllende Menge muss größer 0 sein.");
            }

            if (menge < Volumen - Fuellstand)
            {
                Fuellstand += menge;
                return menge;
            }
            else
            {
                int eingefuellt = Volumen - Fuellstand;
                Fuellstand = Volumen;
                return eingefuellt;
            }
        }

        /// <summary>
        /// Füllt den Behälter bis zum Rand
        /// </summary>
        /// <returns>die tatsächlich eingefüllte Menge</returns>
        public int Fuellen()
        {
            return Fuellen(Volumen - Fuellstand);
        }

        public int Entnehmen(int menge)
        {
            //if (menge <= _fuellstand)
            //{
            //    _fuellstand -= menge;

            //    return menge;
            //}
            //else
            //{
            //    return _fuellstand;
            //}

            if (menge <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(menge), menge, "Die zu entnehmende Menge muss größer 0 sein.");
            }

            int ergebnis;

            if (menge <= Fuellstand)
            {
                Fuellstand -= menge;
                ergebnis = menge;
            }
            else
            {
                int entnommeneMenge = Fuellstand;
                Fuellstand = 0;
                ergebnis = entnommeneMenge;
            }

            if (Fuellstand <= Volumen / 10)
            {
                //todo Ereignisbehandlung 3: Ereignis auslösen
                //Ereignis nur auslösen, wenn es Abonnenten gibt (mit Nullbedingung-Operator auf null prüfen)
                BinLeer?.Invoke(this, new EventArgs());
            }

            return ergebnis;
        }

        public int Entnehmen(string menge)
        {
            
            if (int.TryParse(menge, out int mengeAlsInt))
            {
                return Entnehmen(mengeAlsInt); 
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(menge), menge, "Die zu entnehmende Menge muss als ganze Zahl angegeben werden.");  
            }
        }

        public int CompareTo(object obj)
        {
            Behaelter other = (Behaelter)obj;
            return this.Volumen.CompareTo(other.Volumen);
        }


        #endregion
    }
}
