using Microsoft.Win32;
using ST10446806_PROG6212_POEPART1.Data;
using ST10446806_PROG6212_POEPART1.Models;
using ST10446806_PROG6212_POEPART1.Windows;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ST10446806_PROG6212_POEPART1
{
    public partial class CoordinatorWindow : Window
    {
        public CoordinatorWindow(User user)
        {
            InitializeComponent();
            RefreshList();
            this.Closing += CoordinatorWindow_Closing;
        }

        private void RefreshList()
        {
            using var context = new ApplicationDbContext();

            // Load only claims needing coordinator attention
            var claims = context.Claims
                .Where(c => c.Status == "Documents uploaded" ||
                            c.Status.StartsWith("Coordinator"))
                .OrderByDescending(c => c.SubmittedDate)
                .ToList();

            PendingList.ItemsSource = null;
            PendingList.ItemsSource = claims;
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is Claim selected)
            {
                using var context = new ApplicationDbContext();
                var claim = context.Claims.FirstOrDefault(c => c.ClaimID == selected.ClaimID);

                if (claim != null)
                {
                    claim.Status = "Coordinator Approved";
                    context.SaveChanges();
                    RefreshList();
                }
            }
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is Claim selected)
            {
                using var context = new ApplicationDbContext();
                var claim = context.Claims.FirstOrDefault(c => c.ClaimID == selected.ClaimID);

                if (claim != null)
                {
                    claim.Status = "Rejected";
                    context.SaveChanges();
                    RefreshList();
                }
            }
        }

        private void Document_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock tb && tb.DataContext is string filePath)
            {
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
            LoginWindow login = new LoginWindow("Coordinator");
            login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Close();
        }

        private void CoordinatorWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Go back to roles screen
            RolesWindow rolesWindow = new RolesWindow();
            rolesWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            rolesWindow.Show();
        }
    }
}
