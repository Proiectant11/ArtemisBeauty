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
        string connectionString = ConfigurationManager.ConnectionStrings["salon"].ToString();
        SqlConnection connection = new SqlConnection();
        int genre;//0-femeie, 1--barbat
        public SignInWindow()
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
            connection.Open();
            var checkCommand = connection.CreateCommand();
            checkCommand.CommandType= CommandType.Text;
            checkCommand.CommandText= "SELECT COUNT(1) FROM Clienti WHERE Email = @Email";
            checkCommand.Parameters.AddWithValue("@Email", EmailInput.Text);
            try
            {
                int emailExists = (int)checkCommand.ExecuteScalar();

                if (emailExists > 0)
                {
                    MessageBox.Show("An account with this email already exists. Please try a different email.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    var insertCommand= connection.CreateCommand();
                    insertCommand.CommandType= CommandType.Text;
                    insertCommand.CommandText= "INSERT INTO Clienti (Nume, Prenume, Email, Telefon, Parola, Gen) VALUES (@Nume, @Prenume, @Email, @Telefon, @Parola, @Gen)";
                    insertCommand.Parameters.AddWithValue("@Email", EmailInput.Text);
                    insertCommand.Parameters.AddWithValue("@Nume", FirstNameInput.Text);
                    insertCommand.Parameters.AddWithValue("@Prenume", LastNameInput.Text);
                    insertCommand.Parameters.AddWithValue("@Telefon", PhoneNumberInput.Text);
                    if(FirstPasswordInput.Password == SecondPasswordInput.Password)
                    {
                        insertCommand.Parameters.AddWithValue("@Parola", FirstPasswordInput.Password);
                    }
                    else
                    {
                        MessageBox.Show("Failed to create account. Please try again. PASSWORDS DOESN'T MATCH", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        connection.Close();
                        return;
                    }
                    if(this.genre==0)
                    {
                        insertCommand.Parameters.AddWithValue("@Gen", "F");
                    }
                    else
                    {
                        insertCommand.Parameters.AddWithValue("@Gen", "M");
                    }
                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Account created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        connection.Close();
                        this.Close();
                       
                    }
                    else
                    {
                        MessageBox.Show("Failed to create account. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        connection.Close();
                        return;
                    }

                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Database error: " +ex.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
