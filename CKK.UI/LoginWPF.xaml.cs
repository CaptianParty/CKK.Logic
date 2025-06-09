using CKK.DB.Interfaces;
using CKK.Logic.Models;
using System.Windows;


namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for LoginWPF.xaml
    /// </summary>
    public partial class LoginWPF : Window
    {
        public LoginWPF()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(employeeIdTextBox.Text) || string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                MessageBox.Show("Please enter a username and password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (employeeIdTextBox.Text == "admin" && passwordBox.Password == "password")
            {

                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
