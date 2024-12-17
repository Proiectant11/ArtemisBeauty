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
    /// Interaction logic for StilistWindow.xaml
    /// </summary>
    public partial class StilistWindow : Window
    {
        private Angajati currentAngajat;
        private SALONDataContext db = new SALONDataContext();
        public StilistWindow(Angajati _angajat)
        {
            InitializeComponent();
            currentAngajat = _angajat;
        }

        private List<ProgramariDetaliate> GetAppointmentByEmployeeId(int employeeId)
        {
            DateTime dataLimita = DateTime.Now.AddMonths(-1);
            var appointments = db.Programaris
                                .Where(a => a.ID_Angajat == employeeId && a.Data_programare >= dataLimita && a.Stare==null)
                                .Select(a => new
                                {
                                    a.Data_programare,
                                    a.Ora,
                                    Servicu = db.Serviciis.FirstOrDefault(s => s.ID_Serviciu == a.ID_Serviciu).Denumire,
                                    Client_nume = db.Clientis.FirstOrDefault(e => e.ID_Client == a.ID_Client).Nume,
                                    Client_prenume = db.Clientis.FirstOrDefault(e => e.ID_Client == a.ID_Client).Prenume,
                                    Preferinte=db.Preferintes.FirstOrDefault(p => p.ID_Programare == a.ID_Programare).Detalii_Preferinta
                                })
                                .AsEnumerable()
                                .Select(a => new ProgramariDetaliate
                                {
                                    Denumire = a.Servicu,
                                    DataProgramare = a.Data_programare.ToString("yyyy-MM-dd"),
                                    Ora = a.Ora.ToString(@"hh\:mm"),
                                    NumeAngajat = a.Client_nume + ' ' + a.Client_prenume,
                                    Preferinte=a.Preferinte
                                })
                                .ToList();
            return appointments;
        }
        private void Appointments_Click(object sender, RoutedEventArgs e)
        {
            AppointmentPanel.Visibility = Visibility.Visible;
            ProfilePanel.Visibility = Visibility.Collapsed;
            ProductsPanel.Visibility = Visibility.Collapsed;
            var appointments = GetAppointmentByEmployeeId(currentAngajat.ID_Angajat);
            AppointmentsList.ItemsSource = appointments;
            
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            ProductsPanel.Visibility = Visibility.Visible;   
            AppointmentPanel.Visibility=Visibility.Collapsed;
            ProfilePanel.Visibility=Visibility.Collapsed;
            LoadProducts();
        }

        private void LoadEmployee()
        {
            StylistFirstNameTextBox.Text = currentAngajat.Nume;
            StylistLastNameTextBox.Text=currentAngajat.Prenume;
            StylistEmailTextBox.Text = currentAngajat.Email;
            StylistBirthDateTextBox.Text = currentAngajat.Data_Nasterii.ToString();
            StylistAddressTextBox.Text = currentAngajat.Adresa;
            StylistCityTextBox.Text = currentAngajat.Oras;
            StylistHiringDateTextBox.Text = currentAngajat.Data_Angajarii.ToString();
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            ProfilePanel.Visibility = Visibility.Visible;
            AppointmentPanel.Visibility= Visibility.Collapsed;
            ProductsPanel.Visibility= Visibility.Collapsed;
            LoadEmployee();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow page = new MainWindow();
                page.Left = this.Left;
                page.Top = this.Top;
                currentAngajat = null;
                this.Close();
                page.Show();
            }
        }
        private void CancelAppointment_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null && clickedButton.Tag is ProgramariDetaliate programare)
            {


                var result = MessageBox.Show("Are you sure you want to cancel this appointment?",
                                     "Confirm Cancellation",
                                     MessageBoxButton.YesNo,
                                     MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    DateTime dataProgramare = DateTime.Parse(programare.DataProgramare);
                    TimeSpan oraProgramare = TimeSpan.Parse(programare.Ora);

                    var appointment = db.Programaris
                                        .FirstOrDefault(a => a.Data_programare == dataProgramare &&
                                                             a.Ora == oraProgramare);

                    if (appointment != null)
                    {
                        appointment.Stare = "Cancel";
                        db.SubmitChanges();
                    }
                    var itemToRemove = AppointmentsList.Items.Cast<ProgramariDetaliate>()
                                      .FirstOrDefault(a => a.DataProgramare == programare.DataProgramare &&
                                                           a.Ora == programare.Ora);
                    if (itemToRemove != null)
                    {
                        var itemsSource = AppointmentsList.ItemsSource as List<ProgramariDetaliate>;
                        if (itemsSource != null)
                        {
                            itemsSource.Remove(itemToRemove);
                            AppointmentsList.ItemsSource = null;
                            AppointmentsList.ItemsSource = itemsSource;
                        }
                    }

                    MessageBox.Show("The appointment has been successfully canceled.",
                                    "Cancellation Successful",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            StylistFirstNameTextBox.IsReadOnly = false;
            StylistLastNameTextBox.IsReadOnly = false;
            StylistEmailTextBox.IsReadOnly = false;
            StylistBirthDateTextBox.IsReadOnly = false;
            StylistAddressTextBox.IsReadOnly = false;
            StylistCityTextBox.IsReadOnly=false;
            StylistHiringDateTextBox.IsReadOnly=false ;

            
            SaveButton.Visibility = Visibility.Visible;
            EditButton.Visibility = Visibility.Collapsed;
            MessageBox.Show("You can now edit your profile information.", "Edit Mode", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StylistFirstNameTextBox.Text) || string.IsNullOrWhiteSpace(StylistLastNameTextBox.Text) || string.IsNullOrWhiteSpace(StylistEmailTextBox.Text))
            {
                MessageBox.Show("First Name, Last Name, and Email are required.",
                                "Validation Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            
            currentAngajat.Nume = StylistFirstNameTextBox.Text;
            currentAngajat.Prenume = StylistLastNameTextBox.Text;
            currentAngajat.Email = StylistEmailTextBox.Text;
            currentAngajat.Adresa = StylistAddressTextBox.Text;
            currentAngajat.Oras = StylistCityTextBox.Text;
            currentAngajat.Data_Nasterii = DateTime.Parse(StylistBirthDateTextBox.Text);
            currentAngajat.Data_Angajarii = DateTime.Parse(StylistHiringDateTextBox.Text);

            var employeeInDb = db.Angajatis.FirstOrDefault(p => p.ID_Angajat == currentAngajat.ID_Angajat);
            if (employeeInDb != null)
            {
                
                employeeInDb.Nume = StylistFirstNameTextBox.Text;
                employeeInDb.Prenume = StylistLastNameTextBox.Text;
                employeeInDb.Email = StylistEmailTextBox.Text;
                employeeInDb.Adresa = StylistAddressTextBox.Text;
                employeeInDb.Oras = StylistCityTextBox.Text;
                employeeInDb.Data_Nasterii = DateTime.Parse(StylistBirthDateTextBox.Text);
                employeeInDb.Data_Angajarii = DateTime.Parse(StylistHiringDateTextBox.Text);

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving changes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            
            StylistFirstNameTextBox.IsReadOnly = true;
            StylistLastNameTextBox.IsReadOnly = true;
            StylistEmailTextBox.IsReadOnly = true;
            StylistAddressTextBox.IsReadOnly = true;
            StylistCityTextBox.IsReadOnly = true;
            StylistBirthDateTextBox.IsReadOnly = true;
            StylistHiringDateTextBox.IsReadOnly = true;

            SaveButton.Visibility = Visibility.Collapsed;
            EditButton.Visibility = Visibility.Visible;   

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
            catch(Exception ex)
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
        public class SelectedProduct
        {
            public string Denumire { get; set; }
            public int Cantitate { get; set; }
        }
        private void SaveSelectedProducts_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProductsListBox.Items.Count == 0)
            {
                MessageBox.Show("No products selected.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                foreach (SelectedProduct selectedProduct in SelectedProductsListBox.Items)
                {
                    var productInDb = db.Produse_Cosmetices.FirstOrDefault(p => p.Denumire == selectedProduct.Denumire);
                    if (productInDb != null)
                    {
                        if (productInDb.CantitaeInStoc >= selectedProduct.Cantitate)
                        {
                            productInDb.CantitaeInStoc -= selectedProduct.Cantitate; 
                        }
                        else
                        {
                            MessageBox.Show($"Not enough stock for {selectedProduct.Denumire}. Available: {productInDb.CantitaeInStoc}",
                                             "Stock Error",
                                             MessageBoxButton.OK,
                                             MessageBoxImage.Warning);
                            return;
                        }
                    }
                }

                db.SubmitChanges(); 
                MessageBox.Show("Stock updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                SelectedProductsListBox.Items.Clear(); 
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving products: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void AddProductToSelection_Click(object sender, RoutedEventArgs e)
        {
            var selectedProducts = ProductsListBox.SelectedItems.Cast<Product>().ToList(); 
            if (selectedProducts.Count == 0)
            {
                MessageBox.Show("Please select at least one product.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(QuantityTextBox.Text, out int selectedQuantity) || selectedQuantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var selectedProduct in selectedProducts)
            {
                if (selectedQuantity > selectedProduct.CantitateInStoc)
                {
                    MessageBox.Show($"Insufficient stock for {selectedProduct.Denumire}. Available: {selectedProduct.CantitateInStoc}",
                                    "Stock Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    return; 
                }

                var existingProduct = SelectedProductsListBox.Items.Cast<SelectedProduct>()
                    .FirstOrDefault(p => p.Denumire == selectedProduct.Denumire);

                if (existingProduct == null)
                {
                    SelectedProductsListBox.Items.Add(new SelectedProduct
                    {
                        Denumire = selectedProduct.Denumire,
                        Cantitate = selectedQuantity
                    });
                }
            }
            QuantityTextBox.Clear();
        }
    }
}
