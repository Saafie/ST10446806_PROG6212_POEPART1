
using Microsoft.Win32;
using ST10446806_PROG6212_POEPART1.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ST10446806_PROG6212_POEPART1
{
    public partial class CoordinatorWindow : Window
    {
        private List<Claim> claims;
        private bool loginSuccessful = false;
        public CoordinatorWindow(User user)
        {
            InitializeComponent();
            claims = LecturerWindow.GetClaims(); // shared static list from LecturerWindow
            RefreshList();
            this.Closing += CoordinatorWindow_Closing;
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
        private void Document_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock tb && tb.DataContext != null)
            {
                // Assuming the ItemsControl binds a list of file paths
                string filePath = tb.DataContext.ToString();

                if (System.IO.File.Exists(filePath))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = filePath,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Cannot open document: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("File not found.");
                }
            }
        }


   private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Pass true to indicate this is logout navigation
            LoginWindow login = new LoginWindow("Coordinator");
            this.Close();
        }
        private void CoordinatorWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Open the login/roles window when the user clicks X
            RolesWindow rolesWindow = new RolesWindow();
            rolesWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            rolesWindow.Show();
        }

    }
}



