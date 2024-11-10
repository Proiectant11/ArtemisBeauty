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

namespace Proiect
{
    /// <summary>
    /// Interaction logic for Photos.xaml
    /// </summary>
    public partial class Photos : Window
    {
        public Photos()
        {
            InitializeComponent();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var clickedImage = sender as System.Windows.Controls.Image;
            if (clickedImage != null)
            {
                var imageWindow = new ImageWindow();
                imageWindow.SetImageSource(clickedImage.Source);
                imageWindow.Owner = this;
                imageWindow.Show();
            }
        }
    }
}
