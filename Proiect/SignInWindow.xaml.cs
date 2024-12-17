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

namespace Proiect
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        private SALONDataContext db;
        string connectionString = ConfigurationManager.ConnectionStrings["salon"].ToString();
        SqlConnection connection = new SqlConnection();
        int genre;//0-femeie, 1--barbat
        public SignInWindow()
        {
            InitializeComponent();
            db = new SALONDataContext(connectionString);
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = "";
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null && passwordBox.Tag != null && passwordBox.Password == passwordBox.Tag.ToString())
            {
                passwordBox.Password = "";
                passwordBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null && string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                passwordBox.Password = passwordBox.Tag.ToString();
                passwordBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void LogInAccountButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LogWindow log_window = new LogWindow(this);
            log_window.Left = this.Left;
            log_window.Top = this.Top;
            log_window.Show();
            this.Close();

        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void CreateAccount(object sender, RoutedEventArgs e)
        {
            bool emailExists = db.Clientis.Any(client => client.Email == EmailInput.Text);

            if (emailExists)
            {
                MessageBox.Show("An account with this email already exists. Please try a different email.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var newClient = new Clienti
            {
                Nume = FirstNameInput.Text,
                Prenume = LastNameInput.Text,
                Email = EmailInput.Text,
                Telefon = PhoneNumberInput.Text,
                Parola = FirstPasswordInput.Password == SecondPasswordInput.Password
                            ? FirstPasswordInput.Password
                            : throw new InvalidOperationException("Passwords do not match."),
                Gen = genre == 0 ? "F" : "M"
            };

            try
            {
                db.Clientis.InsertOnSubmit(newClient);
                db.SubmitChanges();

                MessageBox.Show("Account created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
                ClientWindow cw= new ClientWindow(newClient);
                cw.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cb_woman_Checked(object sender, RoutedEventArgs e)
        {
            this.genre = 0;
        }

        private void cb_man_Checked(object sender, RoutedEventArgs e)
        {
            this.genre = 1;
        }
    }
}
