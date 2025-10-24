using ST10446806_PROG6212_POEPART1;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ST10446806_PROG6212_POEPART1
{

    public partial class LoginWindow : Window
    {
        private bool loginSuccessful = false; // track if login worked

        public LoginWindow(string role)
        {
            InitializeComponent();
            ChangeColorBasedOnRole(role);

            // When login window closes without login, reopen RolesWindow
            this.Closed += LoginWindow_Closed;
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
                    // When role window closes, return to RolesWindow
                    nextWindow.Closed += (s, args) =>
                    {
                        RolesWindow rolesWindow = new RolesWindow();
                        rolesWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        rolesWindow.Show();
                    };

                    nextWindow.Show();
                    this.Close(); // Close LoginWindow
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed");
            }
        }

        private void LoginWindow_Closed(object? sender, EventArgs e)
        {
            // Only reopen RolesWindow if login was NOT successful
            if (!loginSuccessful)
            {
                RolesWindow rolesWindow = new RolesWindow();
                rolesWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                rolesWindow.Show();
            }
        }
    }
}
