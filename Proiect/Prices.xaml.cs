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
        string connectionString = ConfigurationManager.ConnectionStrings["salon"].ToString();
        SqlConnection connection = new SqlConnection();
        private Window _parentWindow;
        public Prices(Window parentWindow)
        {
            InitializeComponent();
            _parentWindow = parentWindow;
            connection.ConnectionString = connectionString;
        }
        private void bt_coafor_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            SqlCommand cmd=connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Denumire, Pret from Servicii where Categorie = 'Coafor'";
            var dr=cmd.ExecuteReader();
            ObservableCollection<KeyValuePair<string, object>> serviciiList = new ObservableCollection<KeyValuePair<string, object>>();
            while (dr.Read())
            {
                string nume = dr["Denumire"].ToString();
                decimal pret = Convert.ToDecimal(dr["Pret"]);
                serviciiList.Add(new KeyValuePair<string, object>(nume, pret));
            }

            connection.Close();
            listViewServicii.ItemsSource = serviciiList;
        }

        private void bt_manicure_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Denumire, Pret from Servicii where Categorie = 'Manicure'";
            var dr = cmd.ExecuteReader();
            ObservableCollection<KeyValuePair<string, object>> serviciiList = new ObservableCollection<KeyValuePair<string, object>>();
            while (dr.Read())
            {
                string nume = dr["Denumire"].ToString();
                decimal pret = Convert.ToDecimal(dr["Pret"]);
                serviciiList.Add(new KeyValuePair<string, object>(nume, pret));
            }

            connection.Close();
            listViewServicii.ItemsSource = serviciiList;
        }

        private void bt_pedicure_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Denumire, Pret from Servicii where Categorie = 'Pedicure'";
            var dr = cmd.ExecuteReader();
            ObservableCollection<KeyValuePair<string, object>> serviciiList = new ObservableCollection<KeyValuePair<string, object>>();
            while (dr.Read())
            {
                string nume = dr["Denumire"].ToString();
                decimal pret = Convert.ToDecimal(dr["Pret"]);
                serviciiList.Add(new KeyValuePair<string, object>(nume, pret));
            }

            connection.Close();
            listViewServicii.ItemsSource = serviciiList;
        }

        private void bt_beauty_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Denumire, Pret from Servicii where Categorie = 'Beauty'";
            var dr = cmd.ExecuteReader();
            ObservableCollection<KeyValuePair<string, object>> serviciiList = new ObservableCollection<KeyValuePair<string, object>>();
            while (dr.Read())
            {
                string nume = dr["Denumire"].ToString();
                decimal pret = Convert.ToDecimal(dr["Pret"]);
                serviciiList.Add(new KeyValuePair<string, object>(nume, pret));
            }

            connection.Close();
            listViewServicii.ItemsSource = serviciiList;
        }

        private void bt_massage_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Denumire, Pret from Servicii where Categorie = 'Massage'";
            var dr = cmd.ExecuteReader();
            ObservableCollection<KeyValuePair<string, object>> serviciiList = new ObservableCollection<KeyValuePair<string, object>>();
            while (dr.Read())
            {
                string nume = dr["Denumire"].ToString();
                decimal pret = Convert.ToDecimal(dr["Pret"]);
                serviciiList.Add(new KeyValuePair<string, object>(nume, pret));
            }

            connection.Close();
            listViewServicii.ItemsSource = serviciiList;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
           _parentWindow.Show();
            this.Close();
        }
    }

}
