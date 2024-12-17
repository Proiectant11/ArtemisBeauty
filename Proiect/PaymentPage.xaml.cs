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
    /// Interaction logic for PaymentPage.xaml
    /// </summary>
    public partial class PaymentPage : Window
    {
        private string _serviceName;
        private float _servicePrice;
        public PaymentPage(string serviceName, float servicePrice)
        {
            InitializeComponent();
            _serviceName = serviceName;
            _servicePrice = servicePrice;
            ServiceNameText.Text = _serviceName;
            ServicePriceText.Text = $"{servicePrice:C}";
        }

        private void OnPayButtonClick(object sender, RoutedEventArgs e)
        {
            string cardNumber = CardNumberInput.Text.Trim();
            string expiryDate = ExpiryDateInput.Text.Trim();
            string cvv = CVVInput.Text.Trim();

            var paymentService = new PaymentService();
            if (paymentService.ValidateCard(cardNumber, expiryDate, cvv, out string errorMessage))
            {
                paymentService.ProcessPayment(cardNumber, expiryDate, cvv);
                MessageBox.Show("Payment has been processed successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show(errorMessage);
            }
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
    }
}
