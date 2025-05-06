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
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text) || string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                MessageBox.Show("Please enter a username and password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (usernameTextBox.Text == "admin" && passwordBox.Password == "password")
            {

                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
