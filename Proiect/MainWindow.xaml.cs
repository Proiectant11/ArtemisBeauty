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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace Proiect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString = ConfigurationManager.ConnectionStrings["salon"].ToString();
        SqlConnection connection = new SqlConnection();
        public MainWindow()
        {
            InitializeComponent();
            connection.ConnectionString = connectionString;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainScrollViewer.ScrollToVerticalOffset(Home.TranslatePoint(new Point(0, 0), MainScrollViewer).Y);
        }

        private void Prices_Button(object sender, RoutedEventArgs e)
        {
            Prices prices = new Prices(this);
            prices.Left = this.Left;
            prices.Top = this.Top;
            prices.Show();
            this.Hide();
        }

        private void Shedule_Click(object sender, RoutedEventArgs e)
        {
            LogWindow log_page = new LogWindow(this);
            log_page.Left = this.Left;
            log_page.Top = this.Top;
            log_page.Show();
            this.Hide();
        }

        private void Review_Click(object sender, RoutedEventArgs e)
        {
            MainScrollViewer.ScrollToVerticalOffset(Review.TranslatePoint(new Point(0, 0), MainScrollViewer).Y);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Nota, Recenzie from Istoric_Clienti";
            var dr = cmd.ExecuteReader();
            ObservableCollection<KeyValuePair<string, object>> serviciiList = new ObservableCollection<KeyValuePair<string, object>>();
            while (dr.Read())
            {
                string descriere = dr["Recenzie"].ToString();
                int nota = Convert.ToInt32(dr["Nota"]);
                serviciiList.Add(new KeyValuePair<string, object>(descriere, nota));
            }

            connection.Close();
            ReviewList.ItemsSource = serviciiList;
        }

        private void Photos_Click(object sender, RoutedEventArgs e)
        {
            Photos photo_page= new Photos(this);
            photo_page.Left = this.Left;
            photo_page.Top = this.Top;
            photo_page.Show();
            this.Hide();
        }

        private void Contact_Click(object sender, RoutedEventArgs e)
        {
            MainScrollViewer.ScrollToVerticalOffset(Contact.TranslatePoint(new Point(0, 0), MainScrollViewer).Y);
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
