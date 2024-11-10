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

namespace Proiect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainScrollViewer.ScrollToVerticalOffset(Home.TranslatePoint(new Point(0, 0), MainScrollViewer).Y);
        }

        private void Prices_Button(object sender, RoutedEventArgs e)
        {
            Prices prices = new Prices();
            prices.Left = this.Left;
            prices.Top = this.Top;
            prices.Show();
            this.Close();
        }

        private void Shedule_Click(object sender, RoutedEventArgs e)
        {
            LogWindow log_page = new LogWindow();
            log_page.Left = this.Left;
            log_page.Top = this.Top;
            log_page.Show();
            this.Close();
        }

        private void Review_Click(object sender, RoutedEventArgs e)
        {
            MainScrollViewer.ScrollToVerticalOffset(Review.TranslatePoint(new Point(0, 0), MainScrollViewer).Y);
        }

        private void Photos_Click(object sender, RoutedEventArgs e)
        {
            Photos photo_page= new Photos();
            photo_page.Left = this.Left;
            photo_page.Top = this.Top;
            photo_page.Show();
            this.Close();
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
