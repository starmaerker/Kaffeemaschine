using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KaffeeModell;
using Microsoft.Win32;
using System.Runtime.Serialization.Json;
using System.IO;

namespace KaffeeWpf
{
    /// <summary>
    /// Interaktionslogik für AutomatWindow.xaml
    /// </summary>
    public partial class AutomatWindow : Window
    {
        private Automat _automat;

        private DataContractJsonSerializer _serializer;

        public AutomatWindow()
        {
            InitializeComponent();
            _automat = Automat.ErstelleStandardAutomat();
            _serializer = new DataContractJsonSerializer(typeof(Automat));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Datenbindung
            Datenbindung();
        }

        private void Datenbindung()
        {
            itemsControlBehaelter.ItemsSource = _automat.BehaelterListe;
            itemsControlRezepte.ItemsSource = _automat.RezeptList;
            if (checkBoxAutoRefill.IsChecked == true)
            {
                checkBoxAutoRefill_Checked(checkBoxAutoRefill, new RoutedEventArgs());
            }
        }

        //private void buttonZubereiten_Click(object sender, RoutedEventArgs e)
        //{
        //    textBlockAusgabe.Text += Environment.NewLine +
        //    _automat.Zubereiten(((Button)sender).Content.ToString(), out bool erledigt);
        //}

        private async void buttonZubereiten_Click(object sender, RoutedEventArgs e)
        {
            //async Variante
            Button button = (Button)sender;
            button.IsEnabled = false;
            //progressBar.IsIndeterminate = true;

            Progress<int> progress = new Progress<int>(wert => progressBar.Value = wert);

            string ergebnis = await _automat.ZubereitenAsync(button.Content.ToString(), progress);
            textBlockAusgabe.Text += Environment.NewLine + ergebnis;

            //progressBar.IsIndeterminate = false;

            button.IsEnabled = true;
        }

        private void checkBoxAutoRefill_Checked(object sender, RoutedEventArgs e)
        {
            foreach(Behaelter b in _automat.BehaelterListe)
            {
                b.BinLeer += _automat.Auffuellen;
            }
        }

        private void checkBoxAutoRefill_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (Behaelter b in _automat.BehaelterListe)
            {
                //todo Ereignisbehandlung 6: Ereignisabo kündigen
                b.BinLeer -= _automat.Auffuellen;
            }
        }

        private void buttonSpeichern_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "JSON Dateien | *.json | Alle Dateien | *.*";

            if (dialog.ShowDialog() == true)
            {
                FileStream stream = null;

                try
                {
                    stream = File.Create(dialog.FileName);
                    _serializer.WriteObject(stream, _automat);
                    MessageBox.Show("Automat gespeichert.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    stream?.Close();
                }
            }
        }

        private void buttonLaden_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Filter = "JSON Dateien | *.json | Alle Dateien | *.*";

            if (dialog.ShowDialog() == true)
            {
                FileStream stream = null;

                try
                {
                    stream = File.OpenRead(dialog.FileName);
                    //Ereignisabos des alten Automaten entfernen
                    checkBoxAutoRefill_Unchecked(checkBoxAutoRefill, new RoutedEventArgs());
                    _automat = (Automat)_serializer.ReadObject(stream);
                    Datenbindung();
                    MessageBox.Show("Automat geladen.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    stream?.Close();
                }
            }

        }
    }
}
