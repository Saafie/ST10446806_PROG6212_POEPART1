using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ST10446806_PROG6212_POEPART1
{
    public partial class ManagerWindow : Window
    {
        private List<Claim> claims;
        private bool loginSuccessful = false;
        public ManagerWindow()
        {
            InitializeComponent();
            claims = LecturerWindow.GetClaims(); // shared static list from LecturerWindow
            RefreshList();
            this.Closing += ManagerWindow_Closing;
        }

        private void RefreshList()
        {
            ApprovalList.ItemsSource = null;
            // Show all claims submitted by lecturers
            ApprovalList.ItemsSource = claims;
        }

        private void ManagerWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // If login was NOT successful, show RolesWindow
            if (!loginSuccessful)
            {
                LoginWindow loginWindow = new LoginWindow("Coordinator");
                loginWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                loginWindow.Show();
            }
        }

        private void Approve_Click_Row(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Claim claim)
            {
                if (claim.Status == "Coordinator Approved")
                {
                    claim.Status = "Approved by Manager";
                    RefreshList();
                }
                else
                {
                    MessageBox.Show("Only claims approved by coordinator can be approved by manager.");
                }
            }
        }

        private void Reject_Click_Row(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Claim claim)
            {
                if (claim.Status == "Coordinator Approved")
                {
                    claim.Status = "Rejected by Manager";
                    RefreshList();
                }
                else
                {
                    MessageBox.Show("Only claims approved by coordinator can be rejected by manager.");
                }
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the login window for manager role
            LoginWindow login = new LoginWindow("Manager");
            login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            login.Show();

            // Close the current window
            this.Close();
        }
    }
}

