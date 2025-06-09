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

        private static int LastEmployeeNumber = 110;

        private void Register_Click(object sender, RoutedEventArgs e)
        {

            string firstName = FirstNameBox.Text?.Trim();
            string lastName = LastNameBox.Text?.Trim();
            string email = EmailBox.Text?.Trim();
            string phone = PhoneBox.Text?.Trim();
            string position = PositionBox.Text?.Trim();
            string password = PasswordBox.Password?.Trim();
            int employeeNumber = ++LastEmployeeNumber;


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

            if (!phone.All(char.IsDigit))
            {
                MessageBox.Show("Phone number must contain only digits.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            MessageBox.Show($"Employee registered successfully!\nEmployee Number: {employeeNumber}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);


            // Registration successful
            MessageBox.Show("Employee registered successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            try
            {
                using (var conn = connectionFactory.GetConnection)
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(
                        "INSERT INTO Employees (EmployeeNumber, FirstName, LastName, Email, Phone, Position, Password) " +
                        "VALUES (@EmployeeNumber, @FirstName, @LastName, @Email, @Phone, @Position, @Password)", (SqlConnection)conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeNumber", employeeNumber);
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Position", position);
                        cmd.Parameters.AddWithValue("@Password", password); // Consider hashing in production

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            this.Close();

        }

    }
}
