using ST10446806_PROG6212_POEPART1.Data;
using ST10446806_PROG6212_POEPART1.Windows;
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

        public ManagerWindow(User user)
        {
            InitializeComponent();
            using var context = new ApplicationDbContext();
            claims = context.Claims.ToList();
            RefreshList();
            this.Closing += ManagerWindow_Closing;
        }

        private void RefreshList()
        {
            ApprovalList.ItemsSource = null;
            ApprovalList.ItemsSource = claims;
        }

        private void Approve_Click_Row(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Claim claim)
            {
                // Only proceed if Coordinator approved
                if (claim.Status == "Coordinator Approved")
                {
                    claim.Status = "Approved by Manager";

                    using var context = new ApplicationDbContext();

                    // Attach the claim to the context and update
                    context.Claims.Attach(claim);
                    context.Entry(claim).Property(c => c.Status).IsModified = true;
                    context.SaveChanges();

                    RefreshList();
                }
                else
                {
                    MessageBox.Show("Only claims approved by the Coordinator can be approved by the Manager.");
                }
            }
        }

        private void Reject_Click_Row(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Claim claim)
            {
                // Only proceed if Coordinator approved
                if (claim.Status == "Coordinator Approved")
                {
                    claim.Status = "Rejected by Manager";

                    using var context = new ApplicationDbContext();

                    // Attach the claim to the context and update
                    context.Claims.Attach(claim);
                    context.Entry(claim).Property(c => c.Status).IsModified = true;
                    context.SaveChanges();

                    RefreshList();
                }
                else
                {
                    MessageBox.Show("Only claims approved by the Coordinator can be rejected by the Manager.");
                }
            }
        }



        private void Document_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock tb && tb.DataContext != null)
            {
                string filePath = tb.DataContext.ToString();
                if (System.IO.File.Exists(filePath))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("File not found.");
                }
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            loginSuccessful = true; // mark as intentional logout
            RolesWindow rolesWindow = new RolesWindow();
            rolesWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            rolesWindow.Show();
            this.Close();
        }

        private void ManagerWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!loginSuccessful)
            {
                RolesWindow rolesWindow = new RolesWindow();
                rolesWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                rolesWindow.Show();
            }
        }
    }
}
