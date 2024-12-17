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
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Proiect
{
    /// <summary>
    /// Interaction logic for LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        private SALONDataContext db = new SALONDataContext(); 
        private Window _parentWindow;

        public LogWindow(Window parentWindow)
        {
            InitializeComponent();
            _parentWindow = parentWindow;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null && passwordBox.Tag != null && passwordBox.Password == passwordBox.Tag.ToString())
            {
                passwordBox.Clear();
                passwordBox.Foreground = Brushes.Black;
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null && string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                passwordBox.Password = passwordBox.Tag.ToString();
                passwordBox.Foreground = Brushes.Gray;
            }
        }

        private void CreateAccountButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SignInWindow signInWindow = new SignInWindow();
            signInWindow.Left = this.Left;
            signInWindow.Top = this.Top;
            signInWindow.Show();
            this.Close();
        }

        private void Click_getAccount(object sender, RoutedEventArgs e)
        {
            if (ClientRadioButton.IsChecked == true)
            {
                try
                {
                    var client = db.Clientis.FirstOrDefault(c => c.Email == EmailInput.Text && c.Parola == PasswordInput.Password);

                    if (client != null)
                    {

                        ClientWindow page = new ClientWindow(client);
                        page.Top = this.Top;
                        page.Left = this.Left;
                        page.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid email or password.", "Authentication Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (AdminRadioButton.IsChecked == true)
            {
                try
                {
                    if(EmailInput.Text =="admin@ab.ro" && PasswordInput.Password =="admin")
                    {
                        var angajat = db.Angajatis.FirstOrDefault(a => a.Email == EmailInput.Text && a.Parola == PasswordInput.Password);
                        if (angajat != null)
                        {
                            AdminWindow page = new AdminWindow();
                            page.Top = this.Top;
                            page.Left = this.Left;
                            page.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Admin doesn't exist.", "Authentication Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid email or password.", "Authentication Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (StilistRadioButton.IsChecked == true)
            {
                try
                {
                    var angajat = db.Angajatis.FirstOrDefault(a => a.Email == EmailInput.Text && a.Parola == PasswordInput.Password);
                    if (angajat != null)
                    {
                        StilistWindow page = new StilistWindow(angajat);
                        page.Top = this.Top;
                        page.Left = this.Left;
                        page.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid email or password.", "Authentication Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
    
}
