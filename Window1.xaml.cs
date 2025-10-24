using ST10446806_PROG6212_POEPART1;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static ST10446806_PROG6212_POEPART1.Claim;

namespace ST10446806_PROG6212_POEPART1
{

    public partial class LoginWindow : Window
    {
        private bool loginSuccessful = false; // track if login worked

        public LoginWindow(string role)
        {
            InitializeComponent();
            ChangeColorBasedOnRole(role);

            // Only handle closed event if you want to check for unsuccessful login
            this.Closing += LoginWindow_Closing;
        }

        private void LoginWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // If login was NOT successful, show RolesWindow
            if (!loginSuccessful)
            {
                RolesWindow rolesWindow = new RolesWindow();
                rolesWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                rolesWindow.Show();
            }
        }
        private void ChangeColorBasedOnRole(string role)
        {
            switch (role)
            {
                case "Lecturer":
                    this.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203)); // pink
                    break;
                case "Coordinator":
                    this.Background = new SolidColorBrush(Color.FromRgb(212, 212, 250)); // light purple
                    break;
                case "Manager":
                    this.Background = new SolidColorBrush(Color.FromRgb(188, 239, 245)); // light blue
                    break;

            }
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim().ToLower();
            string password = PasswordBox.Password;

            var user = UserRepository.Users
                .FirstOrDefault(u => u.Username.ToLower() == username && u.Password == password);

            if (user != null)
            {
                loginSuccessful = true; // mark login as successful

                Window nextWindow = null;

                switch (user.Role)
                {
                    case UserRole.Lecturer:
                        nextWindow = new LecturerWindow();
                        break;
                    case UserRole.Coordinator:
                        nextWindow = new CoordinatorWindow();
                        break;
                    case UserRole.Manager:
                        nextWindow = new ManagerWindow();
                        break;
                }

                if (nextWindow != null)
                {
                    nextWindow.Show();
                    this.Close(); // Close LoginWindow
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed");
            }
        }
    }
}