using CKK.DB.Interfaces;
using CKK.DB.Repository;
using CKK.DB.UOW;
using CKK.Logic.Models;
using Dapper;
using System.Windows;


namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for LoginWPF.xaml
    /// </summary>
    public partial class LoginWPF : Window
    {
        private IConnectionFactory _connectionFactory;

        public LoginWPF()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(employeeIdTextBox.Text) || string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                MessageBox.Show("Please enter a Id and password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(employeeIdTextBox.Text, out int employeeId))
            {
                MessageBox.Show("Employee Id must be a number.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Initialize _connectionFactory if not already done
            if (_connectionFactory == null)
                _connectionFactory = new DatabaseConnectionFactory();

            if (ValidateEmployeeCredentials(employeeId, passwordBox.Password))
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Invalid Id or password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ForgotPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if(ForgotPasswordButton_Click != null)
            {
                var forgotPassword = new ForgotPassword();
                forgotPassword.Owner = this; // Optional: sets this window as the owner
                forgotPassword.ShowDialog();
            }
        }

        private void NewEmployee_Click(object sender, RoutedEventArgs e)
        {
            var newEmployeeWindow = new NewEmployee();
            newEmployeeWindow.Owner = this; // Optional: sets this window as the owner
            newEmployeeWindow.ShowDialog();
        }

        private void employeeIdTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }

        private bool ValidateEmployeeCredentials(int employeeId, string password)
        {
            // Make sure _connectionFactory is initialized (e.g., in constructor)
            var employeeRepo = new EmployeeRepository(_connectionFactory);

            // Query the employee by ID (you may want to add a GetEmployeeById method in your repository)
            using (var conn = _connectionFactory.GetConnection)
            {
                conn.Open();
                var sql = "SELECT Password FROM Employee WHERE EmployeeNumber = @EmployeeNumber";
                var dbPassword = conn.QueryFirstOrDefault<string>(sql, new { EmployeeNumber = employeeId });

                if (dbPassword == null)
                    return false; // No such employee

                // In production, compare password hashes!
                return dbPassword == password;
            }
        }
    }
}
