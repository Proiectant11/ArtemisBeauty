using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private Clienti currentClient;
        private SALONDataContext db = new SALONDataContext();
        public ClientWindow(Clienti _client)
        {
            InitializeComponent();
            AppointmentDatePicker.DisplayDateStart = DateTime.Now.Date;
            AppointmentTypeComboBox.Items.Clear();
            AppointmentTypeComboBox.Items.Add("Coafor");
            AppointmentTypeComboBox.Items.Add("Beauty");
            AppointmentTypeComboBox.Items.Add("Manicure");
            AppointmentTypeComboBox.Items.Add("Pedicure");
            AppointmentTypeComboBox.Items.Add("Massage");
            currentClient = _client;
            if (currentClient.Gen == "F")
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("pack://application:,,,/Poze/woman.png");
                bitmap.EndInit();
                image_user.Source = bitmap;
            }
            else
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("pack://application:,,,/Poze/man.png");
                bitmap.EndInit();
                image_user.Source = bitmap;
            }
            tb_nume.Text = currentClient.Prenume;
            CheckAppointments();
        }

        private void CheckAppointments()
        {
            NotificationBell.Visibility = Visibility.Visible;
            DateTime now = DateTime.Now;
            DateTime next24hours = now.AddDays(1);
            var appointments = db.Programaris
                                .Where(p => p.ID_Client == currentClient.ID_Client &&
                                            p.Data_programare >= now && p.Data_programare <= next24hours)
                                .ToList();
            if (appointments.Count > 0)
            {
                NotificationBadge.Visibility = Visibility.Visible;
                NotificationCount.Visibility = Visibility.Visible;
                NotificationCount.Text = appointments.Count.ToString();
            }
            else
            {
                NotificationBadge.Visibility = Visibility.Collapsed;
                NotificationCount.Visibility = Visibility.Collapsed;
            }
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuPanel.Visibility == Visibility.Collapsed)
            {
                MenuPanel.Visibility = Visibility.Visible;
                AppointmentPanel.Visibility = Visibility.Collapsed;
                AppointmentsListPanel.Visibility = Visibility.Collapsed;
                ProfilePanel.Visibility = Visibility.Collapsed;
                CheckAppointments();
            }
            else
            {
                MenuPanel.Visibility = Visibility.Collapsed;
            }
        }
        private List<ProgramariDetaliate> GetAppointmentsByClientID(int clientID)
        {
            var appointments = db.Programaris
                                .Where(a => a.ID_Client == clientID && a.Stare == null)
                                .Select(a => new
                                {
                                    a.Data_programare,
                                    a.Ora,
                                    Servicu = db.Serviciis.FirstOrDefault(s => s.ID_Serviciu == a.ID_Serviciu).Denumire,
                                    Angajat_nume = db.Angajatis.FirstOrDefault(e => e.ID_Angajat == a.ID_Angajat).Nume,
                                    Angajat_prenume = db.Angajatis.FirstOrDefault(e => e.ID_Angajat == a.ID_Angajat).Prenume,
                                    Preferinte = db.Preferintes.FirstOrDefault(p => p.ID_Programare == a.ID_Programare).Detalii_Preferinta
                                })
                                .AsEnumerable()
                                .Select(a => new ProgramariDetaliate
                                {
                                    Denumire = a.Servicu,
                                    DataProgramare = a.Data_programare.ToString("yyyy-MM-dd"),
                                    Ora = a.Ora.ToString(@"hh\:mm"),
                                    NumeAngajat = a.Angajat_nume + ' ' + a.Angajat_prenume,
                                    Preferinte=a.Preferinte
                                })
                                .ToList();
            return appointments;
        }

        private void Programari_Click(object sender, RoutedEventArgs e)
        {
            WelcomeMessage.Visibility = Visibility.Collapsed;
            MenuPanel.Visibility = Visibility.Collapsed;
            AppointmentPanel.Visibility = Visibility.Collapsed;
            ProfilePanel.Visibility = Visibility.Collapsed;
            AppointmentsListPanel.Visibility = Visibility.Visible;
            NotificationBell.Visibility = Visibility.Collapsed;
            NotificationBadge.Visibility = Visibility.Collapsed;
            NotificationCount.Visibility = Visibility.Collapsed;

            var appointments = GetAppointmentsByClientID(currentClient.ID_Client);
            AppointmentsList.ItemsSource = appointments;
        }

        private void Servicii_Click(object sender, RoutedEventArgs e)
        {
            WelcomeMessage.Visibility = Visibility.Collapsed;
            MenuPanel.Visibility = Visibility.Collapsed;
            AppointmentsListPanel.Visibility = Visibility.Collapsed;
            ProfilePanel.Visibility = Visibility.Collapsed;
            AppointmentPanel.Visibility= Visibility.Visible;
            NotificationBell.Visibility = Visibility.Collapsed;
            NotificationBadge.Visibility = Visibility.Collapsed;
            NotificationCount.Visibility = Visibility.Collapsed;
            
        }

        private void LoadClientProfile()
        {
            FirstNameTextBox.Text = currentClient.Nume;
            LastNameTextBox.Text = currentClient.Prenume;
            PhoneTextBox.Text = currentClient.Telefon;
            EmailTextBox.Text = currentClient.Email;
            GenreTextBox.Text = currentClient.Gen;
        }
        private void Profil_Click(object sender, RoutedEventArgs e)
        {
            WelcomeMessage.Visibility = Visibility.Collapsed;
            MenuPanel.Visibility = Visibility.Collapsed;
            AppointmentPanel.Visibility = Visibility.Collapsed;
            AppointmentsListPanel.Visibility = Visibility.Collapsed;
            ProfilePanel.Visibility = Visibility.Visible;
            NotificationBell.Visibility = Visibility.Collapsed;
            NotificationBadge.Visibility = Visibility.Collapsed;
            NotificationCount.Visibility = Visibility.Collapsed;
            LoadClientProfile();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow page = new MainWindow();
                page.Left = this.Left;
                page.Top = this.Top;
                currentClient = null;
                this.Close();
                page.Show();
            }
        }

        private void AppointmentTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AppointmentTypeComboBox.SelectedItem != null)
            {
                string selectedType = AppointmentTypeComboBox.SelectedItem.ToString();
                using (var db = new SALONDataContext())
                {
                    var servicii = db.Serviciis
                        .Where(s => s.Categorie == selectedType)
                        .Select(s => s.Denumire).ToList();
                    ServiceComboBox.ItemsSource = servicii;
                }
            }
        }

        private void ServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedServiceName = ServiceComboBox.SelectedItem.ToString();
            using (var db = new SALONDataContext())
            {
                var serviceId = db.Serviciis
                    .Where(s => s.Denumire == selectedServiceName)
                    .Select(s => s.ID_Serviciu)
                    .FirstOrDefault();

                var angajati = db.Angajati_Serviciis
                    .Where(aserv => aserv.ID_Serviciu == serviceId)
                    .Select(aserv => new
                    {
                        NumeComplet = aserv.Angajati.Nume + " " + aserv.Angajati.Prenume,
                        aserv.Angajati.ID_Angajat
                    })
                    .ToList();

                EmployeeComboBox.ItemsSource = angajati;
                EmployeeComboBox.DisplayMemberPath = "NumeComplet";
                EmployeeComboBox.SelectedValuePath = "ID_Angajat";
            }
        }

        private void PopulateHoursComboBox(int IDangajat, DateTime selectedDate)
        {
            using (var db = new SALONDataContext())
            {
                var occupiedHours = db.Programaris
                            .Where(p => p.ID_Angajat == IDangajat && p.Data_programare == selectedDate.Date)
                            .Select(p => p.Ora)
                            .ToList();

                var formattedOccupiedHours = occupiedHours
                    .Select(ora => ora.ToString(@"hh\:mm"))
                    .ToList();

                var availableHours = new List<string>();
                for (int hour = 8; hour <= 21; hour += 2)
                {
                    availableHours.Add($"{hour:D2}:00");
                }
                availableHours.RemoveAll(hour => formattedOccupiedHours.Contains(hour));
                if (availableHours.Count > 0)
                {
                    TimeComboBox.ItemsSource = availableHours;
                }
                else
                {
                    MessageBox.Show("There are no times available for this day. Please select another employee or another day.");
                    TimeComboBox.ItemsSource = null;
                }

            }
        }

        private void EmployeeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeeComboBox.SelectedItem != null && AppointmentDatePicker != null)
            {
                int selectedEmployeeId = (int)EmployeeComboBox.SelectedValue;
                DateTime selectedDate = AppointmentDatePicker.SelectedDate.Value;
                PopulateHoursComboBox(selectedEmployeeId, selectedDate);
            }
            
        }

        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text) || string.IsNullOrWhiteSpace(EmailTextBox.Text) || string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                MessageBox.Show("Name and Email are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            currentClient.Nume = FirstNameTextBox.Text;
            currentClient.Prenume = LastNameTextBox.Text;
            currentClient.Email = EmailTextBox.Text;
            currentClient.Telefon = PhoneTextBox.Text;
            currentClient.Gen = GenreTextBox.Text;

            var clientInDb = db.Clientis.FirstOrDefault(c => c.ID_Client == currentClient.ID_Client);
            if (clientInDb != null)
            {
                clientInDb.Nume = FirstNameTextBox.Text;
                clientInDb.Prenume = LastNameTextBox.Text;
                clientInDb.Email = EmailTextBox.Text;
                clientInDb.Telefon = PhoneTextBox.Text;
                clientInDb.Gen = GenreTextBox.Text;

                db.SubmitChanges();
            }

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving changes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            FirstNameTextBox.IsReadOnly = true;
            EmailTextBox.IsReadOnly = true;
            PhoneTextBox.IsReadOnly = true;
            GenreTextBox.IsReadOnly = true;
            SaveButton.IsEnabled = false;

            MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            FirstNameTextBox.IsReadOnly = false;
            LastNameTextBox.IsReadOnly = false;
            EmailTextBox.IsReadOnly = false;
            PhoneTextBox.IsReadOnly = false;
            GenreTextBox.IsReadOnly = false;

            SaveButton.IsEnabled = true;
            MessageBox.Show("You can now edit your profile information.", "Edit Mode", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void NotificationBell_Click(object sender, RoutedEventArgs e)
        {
            var clientID = currentClient.ID_Client;
            DateTime now = DateTime.Now;
            DateTime next24hours = now.AddDays(1);
            var appointments = db.Programaris
                                 .Where(p => p.ID_Client == currentClient.ID_Client &&
                                             p.Data_programare >= now && p.Data_programare <= next24hours)
                                 .Select(a => new
                                 {
                                     a.Data_programare,
                                     a.Ora,
                                     Servicu = db.Serviciis.FirstOrDefault(s => s.ID_Serviciu == a.ID_Serviciu).Denumire,
                                     Angajat_nume = db.Angajatis.FirstOrDefault(q => q.ID_Angajat == a.ID_Angajat).Nume,
                                     Angajat_prenume = db.Angajatis.FirstOrDefault(q => q.ID_Angajat == a.ID_Angajat).Prenume
                                 })
                         .AsEnumerable()
                         .Select(a => new ProgramariDetaliate
                         {
                             Denumire = a.Servicu,
                             DataProgramare = a.Data_programare.ToString("yyyy-MM-dd"),
                             Ora = a.Ora.ToString(@"hh\:mm"),
                             NumeAngajat = a.Angajat_nume + ' ' + a.Angajat_prenume
                         })
                         .ToList();

            if (appointments.Count > 0)
            {
                NotificationBadge.Visibility = Visibility.Visible;
                NotificationCount.Text = appointments.Count.ToString();
            }
            else
            {
                NotificationBadge.Visibility = Visibility.Collapsed;
            }

            NotificationsList.Items.Clear();
            foreach (var appointment in appointments)
            {
                string notificationMessage = $"You have an appointment on {appointment.DataProgramare} at {appointment.Ora}\n"+$"for {appointment.Denumire}";
                NotificationsList.Items.Add(new TextBlock { Text = notificationMessage, Foreground = Brushes.Black });
            }

            NotificationsPopup.IsOpen = true;
            NotificationBadge.Visibility = Visibility.Collapsed;
            NotificationCount.Visibility = Visibility.Collapsed;
        }

        private void ConfirmAppointment_Click(object sender, RoutedEventArgs e)
        {
            string preferences = PreferencesTextBox.Text;
            InsertAppointment(preferences);
        }
       
        private void InsertAppointment(string preferences = "")
        {
            try
            {
                if (EmployeeComboBox.SelectedItem == null || ServiceComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Please make sure to select all fields!");
                    return;
                }
                if (!AppointmentDatePicker.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select a valid date!");
                    return;
                }
                DateTime selectedDate = AppointmentDatePicker.SelectedDate.Value;
                string selectedTime = TimeComboBox.SelectedItem.ToString();
                TimeSpan selectedTimeSpan = TimeSpan.Parse(selectedTime);
                var selectedEmployee = (dynamic)EmployeeComboBox.SelectedItem;
                string selectedService = ServiceComboBox.SelectedItem.ToString();
                int selectedEmployeeID = selectedEmployee.ID_Angajat;
                int selectedServiceID;
                using (var db = new SALONDataContext())
                {
                    var service = db.Serviciis.SingleOrDefault(s => s.Denumire == selectedService);
                    if (service != null)
                    {
                        selectedServiceID = service.ID_Serviciu;
                    }
                    else
                    {
                        return;
                    }
                    var newAppointment = new Programari
                    {
                        ID_Client = currentClient.ID_Client,
                        ID_Angajat = selectedEmployeeID,
                        ID_Serviciu = selectedServiceID,
                        Data_programare = selectedDate,
                        Ora = selectedTimeSpan
                    };
                    db.Programaris.InsertOnSubmit(newAppointment);
                    db.SubmitChanges();
                    if (!string.IsNullOrEmpty(preferences))
                    {
                        int appointmentID = newAppointment.ID_Programare;
                        Preferinte newPreference = new Preferinte
                        {
                            ID_Programare = appointmentID,
                            Detalii_Preferinta = preferences
                        };

                        db.Preferintes.InsertOnSubmit(newPreference);
                        db.SubmitChanges();
                    }
                }
                MessageBox.Show("Appointment successfully created!");
                AppointmentPanel.Visibility = Visibility.Collapsed;
                var service_price = (float)db.Serviciis.SingleOrDefault(s => s.Denumire == selectedService).Pret;
                PaymentPage page = new PaymentPage(selectedService, service_price);
                page.ShowDialog();
                CheckAppointments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the appointment: {ex.Message}");
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
    }
}
