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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;


namespace Proiect
{
    /// <summary>
    /// Interaction logic for Prices.xaml
    /// </summary>
    public partial class Prices : Window
    {
        private SALONDataContext db = new SALONDataContext(); // Obiectul LINQ to SQL DataContext
        private Window _parentWindow;

        public Prices(Window parentWindow)
        {
            InitializeComponent();
            _parentWindow = parentWindow;
            ServiceIconsPanel.Visibility= Visibility.Visible;
        }

        private void LoadServiciiByCategorie(string categorie)
        {
            // Selectăm serviciile din tabelul "Servicii" cu categoria specificată
            var servicii = from serviciu in db.Serviciis
                            where serviciu.Categorie == categorie
                            select new KeyValuePair<string, object>(serviciu.Denumire, serviciu.Pret);

            ObservableCollection<KeyValuePair<string, object>> serviciiList = new ObservableCollection<KeyValuePair<string, object>>(servicii);
            listViewServicii.ItemsSource = serviciiList;
        }

        private void bt_coafor_Click(object sender, RoutedEventArgs e)
        {
            LoadServiciiByCategorie("Coafor");
        }

        private void bt_manicure_Click(object sender, RoutedEventArgs e)
        {
            LoadServiciiByCategorie("Manicure");
        }

        private void bt_pedicure_Click(object sender, RoutedEventArgs e)
        {
            LoadServiciiByCategorie("Pedicure");
        }

        private void bt_beauty_Click(object sender, RoutedEventArgs e)
        {
            LoadServiciiByCategorie("Beauty");
        }

        private void bt_massage_Click(object sender, RoutedEventArgs e)
        {
            LoadServiciiByCategorie("Massage");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow.Show();
            this.Close();
        }
    }
}
