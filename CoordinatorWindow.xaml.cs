
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ST10446806_PROG6212_POEPART1
{
    public partial class CoordinatorWindow : Window
    {
        private List<Claim> claims;

        public CoordinatorWindow()
        {
            InitializeComponent();
            claims = LecturerWindow.GetClaims(); // shared static list from LecturerWindow
            RefreshList();
        }

        private void RefreshList()
        {
            PendingList.ItemsSource = null;

            // Show all claims submitted by lecturers
            var displayList = claims
    .Where(c => c.Status == "Documents uploaded" || c.Status.StartsWith("Coordinator"))
    .ToList();


            PendingList.ItemsSource = displayList;
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is Claim selected)
            {
                selected.Status = "Coordinator Approved";
                RefreshList();
            }
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is Claim selected)
            {
                selected.Status = "Rejected";
                RefreshList();
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
 
            // Open the login window for this role
            LoginWindow login = new LoginWindow("Coordinator"); // or "Coordinator"/"Manager" depending on the window
            login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            login.Show();

            // Close the current role window immediately
            this.Close();
        }

    }
}



