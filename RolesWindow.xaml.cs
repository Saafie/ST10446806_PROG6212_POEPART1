using ST10446806_PROG6212_POEPART1;
using System.Windows;

namespace ST10446806_PROG6212_POEPART1.Windows
{
    public partial class RolesWindow : Window
    {
        public RolesWindow()
        {
            InitializeComponent();
        }

        private void ClickLecturer(object sender, RoutedEventArgs e) => OpenLoginWindow("Lecturer");
        private void ClickCoordinator(object sender, RoutedEventArgs e) => OpenLoginWindow("Coordinator");
        private void ClickManager(object sender, RoutedEventArgs e) => OpenLoginWindow("Manager");

        private void OpenLoginWindow(string role)
        {
            var login = new LoginWindow(role);
            login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            login.Show();
            this.Close();
        }
    }
}



