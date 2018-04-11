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

namespace KaffeeWpf
{
    /// <summary>
    /// Interaktionslogik für AutomatWindow.xaml
    /// </summary>
    public partial class AutomatWindow : Window
    {
        private Automat _automat;

        public AutomatWindow()
        {
            InitializeComponent();
            _automat = Automat.ErstelleStandardAutomat();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Datenbindung
            itemsControlBehaelter.ItemsSource = _automat.BehaelterListe;
            itemsControlRezepte.ItemsSource = _automat.RezeptList;
        }

        private void buttonZubereiten_Click(object sender, RoutedEventArgs e)
        {
            textBlockAusgabe.Text += Environment.NewLine +
            _automat.Zubereiten(((Button)sender).Content.ToString());
        }
    }
}
