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
        string connectionString = ConfigurationManager.ConnectionStrings["salon"].ToString();
        SqlConnection connection = new SqlConnection();
        public LogWindow()
        {
            InitializeComponent();
            connection.ConnectionString = connectionString;
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
            string query = "SELECT COUNT(1) FROM Clienti WHERE Email=@Email AND Parola=@Parola";
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", EmailInput.Text);
                    command.Parameters.AddWithValue("@Parola", PasswordInput.Password);

                    int count = (int)command.ExecuteScalar();

                    if (count == 1)
                    {
                        //PAGINA PRINCIPALA 
                        //Dashboard dashboardWindow = new Dashboard();
                        //dashboardWindow.Left = this.Left;
                        //dashboardWindow.Top = this.Top;
                        //dashboardWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid email or password.", "Authentication Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

        }
    }
}
