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
        private List<string> _allImages;
        private int _currentIndex;
        private Window _parentWindow;
        public Photos(Window parentWindow)
        {
            InitializeComponent();
            _parentWindow = parentWindow;
            _allImages = new List<string>
                {
                  "c1.png", "c2.png", "c3.png", "c4.png", "c5.png", "c6.png",
                   "c7.png", "c8.png", "c9.png", "c10.png", "c11.png", "c12.png",
                   "c13.png", "c14.png", "b1.png", "b2.png", "b3.png", "b4.png",
                   "b5.png", "b6.png", "u1.png", "u2.png", "u3.png", "u4.png",
                   "u5.png", "u6.png", "u7.png", "u8.png", "u9.png"
                };
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image image)
            {
                if (image.Source != null)
                {
                    if (!(image.Source is BitmapImage))
                    {
                        Uri uri = new Uri(image.Source.ToString(), UriKind.RelativeOrAbsolute);
                        image.Source = new BitmapImage(uri);
                    }

                    if (image.Source is BitmapImage bitmapImage)
                    {
                        string fullPath = bitmapImage.UriSource.ToString();
                        string imageName = fullPath.Substring(fullPath.LastIndexOf('/') + 1);
                        _currentIndex = _allImages.IndexOf(imageName);
                        if(_currentIndex >=0)
                        {
                            string imagePath = "Poze/" + _allImages[_currentIndex];

                            if (!string.IsNullOrEmpty(imagePath))
                            {
                                ShowOverlay();
                                DisplayImage(imagePath); 
                            }
                            else
                            {
                                MessageBox.Show($"Imaginea {imageName} nu a fost găsită.");
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Imaginea {imageName} nu a fost găsită în lista de căi.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sursa nu a putut fi convertită în BitmapImage.");
                    }
                }
                else
                {
                    MessageBox.Show("Image source is null.");
                }
            }
            else
            {
                MessageBox.Show("Sender-ul nu este de tip Image.");
            }
        }

        private void ShowOverlay()
        {
            ImageOverlay.Visibility = Visibility.Visible;
        }

        private void HideOverlay()
        {
            ImageOverlay.Visibility = Visibility.Collapsed;
        }

        private void DisplayImage(string imagePath)
        {
            if (_currentIndex >= 0 && _currentIndex < _allImages.Count)
            {
                string fullImagePath = "pack://application:,,,/Proiect;component/" + imagePath;
                LargeImage.Source = new BitmapImage(new Uri(fullImagePath, UriKind.Absolute));
            }
        }

        private void CloseOverlay_Click(object sender, RoutedEventArgs e)
        {
            HideOverlay();
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            _currentIndex = (_currentIndex - 1 + _allImages.Count) % _allImages.Count;
            string imagePath = "Poze/" + _allImages[_currentIndex];
            DisplayImage(imagePath);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _currentIndex = (_currentIndex + 1) % _allImages.Count;
            string imagePath = "Poze/" + _allImages[_currentIndex];
            DisplayImage(imagePath);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow.Show();
            this.Close();
        }
    }
}

