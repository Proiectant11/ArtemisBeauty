using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public class PaymentService
    {
        public bool ValidateCard(string cardNumber, string expiryDate, string cvv, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 16)
            {
                errorMessage = "The card number is not valid.";
                return false;
            }

            if (string.IsNullOrEmpty(cvv) || cvv.Length < 3)
            {
                errorMessage = "CVV is not valid.";
                return false;
            }

            if (string.IsNullOrEmpty(expiryDate) || !expiryDate.Contains('/'))
            {
                errorMessage = "The expiration date is not valid. Use MM/YY format.";
                return false;
            }

            string[] parts = expiryDate.Split('/');
            if (parts.Length != 2 || !int.TryParse(parts[0], out int month) || !int.TryParse(parts[1], out int year))
            {
                errorMessage = "The expiration date is not valid. Use MM/YY format.";
                return false;
            }

            year += year < 100 ? 2000 : 0;

            DateTime currentDate = DateTime.Now;
            int currentYear = currentDate.Year;
            int currentMonth = currentDate.Month;

            if (year < currentYear || (year == currentYear && month < currentMonth) || month < 1 || month > 12)
            {
                errorMessage = "The card is expired or the date is invalid.";
                return false;
            }

            return true;
        }

        public void ProcessPayment(string cardNumber, string expiryDate, string cvv)
        {
            Console.WriteLine("Payment has been processed successfully!");
        }
    }
}
