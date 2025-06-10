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
using Dapper;
using CKK.DB.Repository; 
using CKK.DB.Interfaces;
using System.Data;
using CKK.DB.UOW;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        private readonly IConnectionFactory connectionFactory; 
        private readonly UnitOfWork unitOfWork;

        public ForgotPassword()
        {
            connectionFactory = new DatabaseConnectionFactory();
            unitOfWork = new UnitOfWork(connectionFactory);

            InitializeComponent();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(employeeIdTextBox.Text) || string.IsNullOrWhiteSpace(newPasswordBox.Password))
            {
                MessageBox.Show("Please enter an employee ID and a new password.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(employeeIdTextBox.Text, out int employeeId))
            {
                MessageBox.Show("Employee ID must be a number.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string newPassword = newPasswordBox.Password;

            if (!IsStrongPassword(newPassword, 8))
            {
                MessageBox.Show("Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.", "Password Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (IDbConnection conn = connectionFactory.GetConnection)
                {
                    conn.Open();
                    // Check if employee exists
                    var sqlCheck = "SELECT COUNT(*) FROM Employee WHERE EmployeeNumber = @EmployeeNumber";
                    int count = conn.QuerySingle<int>(sqlCheck, new { EmployeeNumber = employeeId });

                    if (count == 0)
                    {
                        MessageBox.Show("Employee ID does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Update password
                    var sqlUpdate = "UPDATE Employee SET Password = @Password WHERE EmployeeNumber = @EmployeeNumber";
                    conn.Execute(sqlUpdate, new { Password = newPassword, EmployeeNumber = employeeId });

                    MessageBox.Show("Password updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
    }
}
