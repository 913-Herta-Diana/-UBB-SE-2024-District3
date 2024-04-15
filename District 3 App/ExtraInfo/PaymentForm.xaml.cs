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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace District_3_App.ExtraInfo
{
    /// <summary>
    /// Interaction logic for PaymentForm.xaml
    /// </summary>
    public partial class PaymentForm : UserControl
    {
        public Account Account { get; set; }
        public PaymentForm()
        {
            InitializeComponent();
            Account = new Account(new UserExtraInfo("john_doe", "securepassword"), "", "", "", "");
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true;
            if (!ValidateCardNumber(CardNumberTextBox.Text))
            {
                MessageBox.Show("Card Number must be a string of 16 digits (numeric input only).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }
            //Console.WriteLine(ValidateExpirationDate(ExpirationDateTextBox.Text));
            if (!ValidateExpirationDate(ExpirationDateTextBox.Text))
            {
                MessageBox.Show("Expiration Date must be in MM/YY format (numeric input only).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if (!ValidateCVV(CVVTextBox.Text))
            {
                MessageBox.Show("CVV must be a three-digit code (numeric input only).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if (!ValidateHolderName(HolderNameTextBox.Text))
            {
                MessageBox.Show("Holder Name must contain alphabetic characters only.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if (isValid)
            {
                Account.CardNumber = CardNumberTextBox.Text;
                Account.ExpirationDate = ExpirationDateTextBox.Text;
                Account.CVV = CVVTextBox.Text;
                Account.HolderName = HolderNameTextBox.Text;

                var newContent = new PaymentConfirmed();

                CompleteGrid.Children.Clear();
                Grid.SetColumn(newContent, 1);
                CompleteGrid.Children.Add(newContent);
            }
        }
        private bool ValidateCardNumber(string cardNumber)
        {
            return !string.IsNullOrWhiteSpace(cardNumber) && (cardNumber.Length == 16 && IsNumeric(cardNumber));
        }

        private bool ValidateExpirationDate(string expirationDate)
        {
            if (string.IsNullOrWhiteSpace(expirationDate) || expirationDate.Length != 5)
                return false;

            string[] parts = expirationDate.Split('/');
            if (parts.Length != 2)
                return false;

            if (!int.TryParse(parts[0], out int month) || !int.TryParse(parts[1], out int year))
                return false;

            int currentYear = DateTime.Now.Year % 100;
            return month >= 1 && month <= 12 && year >= currentYear && year <= currentYear + 10;
        }


        private bool ValidateCVV(string cvv)
        {
            return !string.IsNullOrWhiteSpace(cvv) && (cvv.Length == 3 && IsNumeric(cvv));
        }

        private bool ValidateHolderName(string holderName)
        {
            return !string.IsNullOrWhiteSpace(holderName) && IsAlphabetic(holderName);
        }


        private bool IsNumeric(string value)
        {
            return long.TryParse(value, out _);
        }

        private bool IsAlphabetic(string value)
        {
            return value.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }
    }
}
