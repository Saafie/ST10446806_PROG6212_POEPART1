using Microsoft.EntityFrameworkCore;
using ST10446806_POE.Data;
using ST10446806_POE.Models;
using ST10446806_PROG6212_POEPART1;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ST10446806_PROG6212_POEPART1.Windows
{
    public partial class LoginWindow : Window
    {
        private bool loginSuccessful = false;

        public LoginWindow(string role)
        {
            InitializeComponent();
            ChangeColorBasedOnRole(role);
            this.Closing += LoginWindow_Closing;
        }

        private void LoginWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!loginSuccessful)
            {
                var rolesWindow = new RolesWindow();
                rolesWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                rolesWindow.Show();
            }
        }

        private void ChangeColorBasedOnRole(string role)
        {
            switch (role)
            {
                case "Lecturer": this.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203)); break;
                case "Coordinator": this.Background = new SolidColorBrush(Color.FromRgb(188, 239, 245)); break;
                case "Manager": this.Background = new SolidColorBrush(Color.FromRgb(212, 212, 250)); break;
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim().ToLower();
            string password = PasswordBox.Password;

            using var context = new ApplicationDbContext();
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower() == username && u.Password == password);

            if (user != null)
            {
                loginSuccessful = true;
                MessageBox.Show($"Welcome {user.FullName} ({user.Role})!");

                switch (user.Role)
                {
                    case UserRole.Lecturer:
                        var lecturerWin = new LecturerWindow(user);
                        lecturerWin.Show();
                        break;
                    case UserRole.Coordinator:
                        var coordWin = new CoordinatorWindow(user);
                        coordWin.Show();
                        break;
                    case UserRole.Manager:
                        var managerWin = new ManagerWindow(user);
                        managerWin.Show();
                        break;
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

    }
    }