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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private SALONDataContext db = new SALONDataContext();
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void ManageEmployees_Click(object sender, RoutedEventArgs e)
        {
            EmployeePanel.Visibility = Visibility.Visible;
            ProductsPanel.Visibility = Visibility.Collapsed;
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            var employees=db.Angajatis
                .Select(emp => $"{emp.Nume} {emp.Prenume} - {emp.Email}")
                .ToList();
            EmployeeListBox.ItemsSource = employees;
        }

        private void ManageProducts_Click(object sender, RoutedEventArgs e)
        {
            ProductsPanel.Visibility = Visibility.Visible;
            EmployeePanel.Visibility = Visibility.Collapsed;
            LoadProducts();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow page = new MainWindow();
                page.Left = this.Left;
                page.Top = this.Top;
                this.Close();
                page.Show();
            }
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var addEmployeeWindow = new AddEmployeeWindow();
            bool? result = addEmployeeWindow.ShowDialog(); 

            if (result == true)
            {
                LoadEmployees();
            }
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select an employee from the list to delete.",
                                "No Selection",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }
            string selectedEmployee = EmployeeListBox.SelectedItem.ToString();
            var result = MessageBox.Show($"Are you sure you want to delete {selectedEmployee}?",
                                         "Confirm Deletion",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    string email = selectedEmployee.Split('-')[1].Trim();

                    var employeeToDelete = db.Angajatis
                        .FirstOrDefault(emp => emp.Email == email);

                    if (employeeToDelete != null)
                    {
                        try
                        {
                            
                            var programari = db.Programaris.Where(p => p.ID_Angajat == employeeToDelete.ID_Angajat).ToList();
                            if (programari.Any())
                            {
                                db.Programaris.DeleteAllOnSubmit(programari);
                            }

                            
                            var angajatiServicii = db.Angajati_Serviciis.Where(aserv => aserv.ID_Angajat == employeeToDelete.ID_Angajat).ToList();
                            if (angajatiServicii.Any())
                            {
                                db.Angajati_Serviciis.DeleteAllOnSubmit(angajatiServicii);
                            }

                            
                            var angajatiFunctiiDepartamente = db.Angajati_Functii_Departamentes
                                .Where(afd => afd.ID_Angajat == employeeToDelete.ID_Angajat).ToList();
                            if (angajatiFunctiiDepartamente.Any())
                            {
                                db.Angajati_Functii_Departamentes.DeleteAllOnSubmit(angajatiFunctiiDepartamente);
                            }

                            
                            db.Angajatis.DeleteOnSubmit(employeeToDelete);

                            
                            db.SubmitChanges();

                            MessageBox.Show("Employee and associated records deleted successfully.",
                                            "Success",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Information);
                            LoadEmployees();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error occurred while deleting the employee: {ex.Message}",
                                            "Error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected employee not found in the database.",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting employee: {ex.Message}",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }
        }

        private void LoadProducts()
        {
            try
            {
                var products = db.Produse_Cosmetices.Select(p => new Product
                {
                    ID_Produs = p.ID_Produs,
                    Denumire = p.Denumire,
                    CantitateInStoc = p.CantitaeInStoc,
                    PretBucata = (float)p.PretBucata,
                    Furnizor = p.Furnizor
                }).ToList();
                ProductsListBox.ItemsSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public class Product
        {
            public int ID_Produs { get; set; }
            public string Denumire { get; set; }
            public int CantitateInStoc { get; set; }
            public float PretBucata { get; set; }
            public string Furnizor { get; set; }
        }
        private void UpdateProductStock_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = ProductsListBox.SelectedItem as Product;

            if (selectedProduct == null)
            {
                MessageBox.Show("Please select a product from the list.",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

           
            if (int.TryParse(QuantityTextBox.Text, out int quantityToAdd) && quantityToAdd > 0)
            {
                
                UpdateProductStock(selectedProduct.ID_Produs, quantityToAdd);
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity.",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        public void UpdateProductStock(int productId, int quantityToAdd)
        {
            try
            {
                var product = db.Produse_Cosmetices.FirstOrDefault(p => p.ID_Produs == productId);

                if (product == null)
                {
                    MessageBox.Show("Product not found. Please check the ID and try again.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    return;
                }

                product.CantitaeInStoc += quantityToAdd;

                db.SubmitChanges();

                MessageBox.Show($"The stock for {product.Denumire} has been updated to {product.CantitaeInStoc}.",
                                "Success",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                LoadProducts();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the stock: {ex.Message}",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

    }
}
