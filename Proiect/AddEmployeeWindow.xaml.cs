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
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        private SALONDataContext db = new SALONDataContext();
        public AddEmployeeWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Preia valorile din controale
                string firstName = FirstNameTextBox.Text;
                string lastName = LastNameTextBox.Text;
                DateTime? dateOfBirth = DateOfBirthPicker.SelectedDate;
                DateTime? hireDate = HireDatePicker.SelectedDate;
                string address = AddressTextBox.Text;
                string city = CityTextBox.Text;
                string email = EmailTextBox.Text;
                string password = PasswordBox.Password;

                // Validări simple
                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("All required fields must be completed!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Adaugă angajatul în baza de date
               
                var newEmployee = new Angajati
                {
                    Nume = firstName,
                    Prenume = lastName,
                    Data_Nasterii = dateOfBirth,
                    Data_Angajarii = hireDate,
                    Adresa = address,
                    Oras = city,
                    Email = email,
                    Parola = password
                };

                db.Angajatis.InsertOnSubmit(newEmployee);
                db.SubmitChanges();
                

                MessageBox.Show("Employee added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding employee: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult=false;
            this.Close();
        }
    }
}
