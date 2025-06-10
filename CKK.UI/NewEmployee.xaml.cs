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
using System.Data;
using System.Data.SqlClient;
using CKK.DB.Interfaces;
using CKK.DB.UOW;
using System.Text.RegularExpressions;
using CKK.DB.Repository;

namespace CKK.UI
{
    /// <summary>  
    /// Interaction logic for NewEmployee.xaml  
    /// </summary>  
    public partial class NewEmployee : Window
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IConnectionFactory connectionFactory;

        public NewEmployee()
        {
            connectionFactory = new DatabaseConnectionFactory();
            unitOfWork = new UnitOfWork(connectionFactory);

            InitializeComponent();
        }


        private bool IsStrongPassword(string password, int minLength = 8)
        {
            if (string.IsNullOrEmpty(password) || password.Length < minLength)
                return false;

            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpper && hasLower && hasDigit && hasSpecial;
        }

        private void PhoneBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }



        private void Register_Click(object sender, RoutedEventArgs e)
        {

            string firstName = FirstNameBox.Text?.Trim();
            string lastName = LastNameBox.Text?.Trim();
            string email = EmailBox.Text?.Trim();
            string phone = PhoneBox.Text?.Trim();
            string position = PositionBox.Text?.Trim();
            string password = PasswordBox.Password?.Trim();

            if (!IsStrongPassword(password, 8))
            {
                MessageBox.Show(
                    "Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.",
                    "Password Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(position) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address != email)
                {
                    MessageBox.Show("Please enter a valid email address.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid email address.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Regex.IsMatch(phone, @"^\d{3}-\d{3}-\d{4}$"))
            {
                MessageBox.Show("Phone number must be in the format 123-456-7890.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                Position = position,
                Password = password // Consider hashing in production
            };

            try
            {
                var employeeRepo = new EmployeeRepository(connectionFactory);
                int newEmployeeNumber = employeeRepo.AddEmployee(employee);

                MessageBox.Show($"Employee registered successfully!\nEmployee Number: {newEmployeeNumber}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            };
        }

        private void PhoneBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            string digits = new string(textBox.Text.Where(char.IsDigit).ToArray());

            string formatted = digits;
            if (digits.Length > 6)
                formatted = $"{digits.Substring(0, 3)}-{digits.Substring(3, 3)}-{digits.Substring(6, Math.Min(4, digits.Length - 6))}";
            else if (digits.Length > 3)
                formatted = $"{digits.Substring(0, 3)}-{digits.Substring(3, Math.Min(3, digits.Length - 3))}";

            if (textBox.Text != formatted)
            {
                int selStart = textBox.SelectionStart;
                textBox.Text = formatted;
                // Set caret to the end or as close as possible
                textBox.SelectionStart = Math.Min(selStart, textBox.Text.Length);
            }
        }
    }
}
