using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ST10446806_PROG6212_POEPART1
{
    public partial class RolesWindow : Window
    {
        public RolesWindow()
        {
            InitializeComponent();
        }

        private void ClickLecturer(object sender, RoutedEventArgs e)
        {
            OpenLoginWindow("Lecturer");
        }

        private void ClickCoordinator(object sender, RoutedEventArgs e)
        {
            OpenLoginWindow("Coordinator");
        }

        private void ClickManager(object sender, RoutedEventArgs e)
        {
            OpenLoginWindow("Manager");
        }

        private void OpenLoginWindow(string role)
        {
            // Create and show the LoginWindow, passing the selected role
            var loginWindow = new LoginWindow(role);
            loginWindow.Show();

            // Close the roles window if you want to move on
            this.Close();
        }
    }
}



