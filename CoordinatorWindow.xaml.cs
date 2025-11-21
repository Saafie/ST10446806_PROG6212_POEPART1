using ST10446806_PROG6212_POEPART1.Data;
using ST10446806_PROG6212_POEPART1.Helpers;
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
            // Load all claims
            using var context = new ApplicationDbContext();
            claims = context.Claims.ToList();
            RefreshList();

            this.Closing += CoordinatorWindow_Closing;
        }

        private void RefreshList()
        {
            PendingList.ItemsSource = null;
            var displayList = claims
                .Where(c => c.Status == "Documents uploaded" || c.Status.StartsWith("Coordinator"))
                .ToList();
            PendingList.ItemsSource = displayList;
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is Claim selected)
            {
                // Validate claim rules first
                if (CoordinatorClaimRules.ValidateClaim(selected))
                {
                    MessageBox.Show("Cannot approve: Claim violates policy rules (hours, hourly rate, or missing documents).");

                    // Optional: auto-reject if violating
                    selected.Status = "Rejected by Coordinator (policy violation)";
                }
                else
                {
                    // Only approve if currently "Documents uploaded"
                    if (selected.Status == "Documents uploaded")
                    {
                        selected.Status = "Coordinator Approved";
                    }
                    else
                    {
                        MessageBox.Show("Claim cannot be approved in its current status.");
                        return;
                    }
                }

                // Save change to database
                using var context = new ApplicationDbContext();
                context.Claims.Attach(selected);
                context.Entry(selected).Property(c => c.Status).IsModified = true;
                context.SaveChanges();

                RefreshList();
            }
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is Claim selected)
            {
                // Only reject if currently "Documents uploaded"
                if (selected.Status == "Documents uploaded" || selected.Status.StartsWith("Rejected"))
                {
                    selected.Status = "Rejected by Coordinator";

                    using var context = new ApplicationDbContext();
                    context.Claims.Attach(selected);
                    context.Entry(selected).Property(c => c.Status).IsModified = true;
                    context.SaveChanges();

                    RefreshList();
                }
                else
                {
                    MessageBox.Show("Only claims in the correct state can be rejected.");
                }
            }
        }

        // Optional: call this when loading claims to auto-flag invalid ones
        private void FlagInvalidClaims()
        {
            foreach (var claim in claims)
            {
                if (!ClaimRules.ValidateClaim(claim) && claim.Status == "Documents uploaded")
                {
                    claim.Status = "Rejected by Coordinator (policy violation)";
                }
            }
            RefreshList();
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
 

        private void CoordinatorWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
