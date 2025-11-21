using Microsoft.EntityFrameworkCore;
using ST10446806_PROG6212_POEPART1.Data;
using ST10446806_PROG6212_POEPART1.Models;
using System;
using System.Windows;
using System.Windows.Media;

namespace ST10446806_PROG6212_POEPART1.Windows
{
    public partial class LoginWindow : Window
    {
        private string Role;
        private bool loginSuccessful = false;
        public LoginWindow(string role)
        {
            InitializeComponent();
            Role = role;
            ChangeColorBasedOnRole(role);
            
        }

        private void ChangeColorBasedOnRole(string role)
        {
            switch (role)
            {
                case "Lecturer":
                    this.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
                    break;
                case "Coordinator":
                    this.Background = new SolidColorBrush(Color.FromRgb(188, 239, 245));
                    break;
                case "Manager":
                    this.Background = new SolidColorBrush(Color.FromRgb(212, 212, 250));
                    break;
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password;

            // Convert role string to UserRole enum
            if (!Enum.TryParse<UserRole>(Role, out var roleEnum))
            {
                MessageBox.Show("Invalid role selected.");
                return;
            }

            using var context = new ApplicationDbContext();

            // Compare enum directly
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password && u.Role == roleEnum);

            loginSuccessful = true;
          

            if (user != null)
            {
                MessageBox.Show($"Welcome {user.FullName} ({user.Role})!");

                // Open corresponding window
                switch (user.Role)
                {
                    case UserRole.Lecturer:
                        new LecturerWindow(user).Show();
                        break;
                    case UserRole.Coordinator:
                        new CoordinatorWindow(user).Show();
                        break;
                    case UserRole.Manager:
                        new ManagerWindow(user).Show();
                        break;
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoginWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Open the login/roles window when the user clicks X
            RolesWindow rolesWindow = new RolesWindow();
            rolesWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            rolesWindow.Show();
        }
    }
}
