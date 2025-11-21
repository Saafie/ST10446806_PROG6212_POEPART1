using ST10446806_PROG6212_POEPART1.Data;
using ST10446806_PROG6212_POEPART1.Models;
using ST10446806_PROG6212_POEPART1.Windows;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ST10446806_PROG6212_POEPART1
{
    public partial class ManagerWindow : Window
    {
        private bool loginSuccessful = false;

        public ManagerWindow(User user)
        {
            InitializeComponent();
            RefreshList();
        }

        private void RefreshList()
        {
            using var context = new ApplicationDbContext();

            // Load ALL claims from DB
            var claims = context.Claims
                .OrderByDescending(c => c.SubmittedDate)
                .ToList();

            ApprovalList.ItemsSource = null;
            ApprovalList.ItemsSource = claims;
        }

        private void Approve_Click_Row(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Claim claim)
            {
                using var context = new ApplicationDbContext();
                var dbClaim = context.Claims.FirstOrDefault(c => c.ClaimID == claim.ClaimID);

                if (dbClaim != null)
                {
                    if (dbClaim.Status == "Coordinator Approved")
                    {
                        dbClaim.Status = "Approved by Manager";
                        context.SaveChanges();
                        RefreshList();
                    }
                    else
                    {
                        MessageBox.Show("Only coordinator-approved claims can be approved.");
                    }
                }
            }
        }

        private void Reject_Click_Row(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Claim claim)
            {
                using var context = new ApplicationDbContext();
                var dbClaim = context.Claims.FirstOrDefault(c => c.ClaimID == claim.ClaimID);

                if (dbClaim != null)
                {
                    if (dbClaim.Status == "Coordinator Approved")
                    {
                        dbClaim.Status = "Rejected by Manager";
                        context.SaveChanges();
                        RefreshList();
                    }
                    else
                    {
                        MessageBox.Show("Only coordinator-approved claims can be rejected.");
                    }
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow("Manager");
            login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            login.Show();
            this.Close();
        }
    }
}
