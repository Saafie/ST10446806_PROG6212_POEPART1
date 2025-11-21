using Microsoft.Win32;
using ST10446806_PROG6212_POEPART1.Data;
using ST10446806_PROG6212_POEPART1.Windows;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ST10446806_PROG6212_POEPART1
{
    public partial class LecturerWindow : Window
    {
        private LecturerProfile currentProfile;

        // Uploaded documents for current claim
        public ObservableCollection<UploadedFile> SelectedDocumentPaths { get; set; } =
            new ObservableCollection<UploadedFile>();

        public LecturerWindow(User currentUser)
        {
            InitializeComponent();

            using var context = new ApplicationDbContext();

            // Load lecturer profile
            currentProfile = context.LecturerProfiles
                .FirstOrDefault(l => l.UserID == currentUser.UserID);

            HoursBox.TextChanged += InputChanged_UpdateTotal;
            HourlyRateBox.TextChanged += InputChanged_UpdateTotal;

            if (currentProfile == null)
            {
                MessageBox.Show("Lecturer profile not found in database.");
                this.Close();
                return;
            }

            // Load only this lecturer's claims
            LoadMyClaims();

            // Bind uploaded documents list
            DocumentsList.ItemsSource = SelectedDocumentPaths;

            this.Closing += LecturerWindow_Closing;
        }

        // Load only CLAIMS FOR THIS LOGGED-IN LECTURER
        private void LoadMyClaims()
        {
            using var context = new ApplicationDbContext();

            ClaimList.ItemsSource = context.Claims
                .Where(c => c.LecturerID == currentProfile.LecturerID)
                .ToList();
        }


        private void InputChanged_UpdateTotal(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (decimal.TryParse(HoursBox.Text, out decimal hours) &&
                decimal.TryParse(HourlyRateBox.Text, out decimal rate))
            {
                // Validation: prevent negative or unrealistic values
                if (hours < 0 || hours > 24)
                {
                    TotalPaymentLabel.Text = "Invalid hours";
                    return;
                }

                if (rate < 0 || rate > 5000) // example max hourly rate
                {
                    TotalPaymentLabel.Text = "Invalid rate";
                    return;
                }

                decimal total = hours * rate;
                TotalPaymentLabel.Text = $"R{total:0.00}";
            }
            else
            {
                TotalPaymentLabel.Text = "R0.00";
            }
        }


        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!decimal.TryParse(HoursBox.Text, out decimal hours))
            {
                MessageBox.Show("Please enter a valid number of hours.");
                return;
            }

            if (!decimal.TryParse(HourlyRateBox.Text, out decimal hourlyRate))
            {
                MessageBox.Show("Please enter a valid hourly rate.");
                return;
            }

            if (SelectedDocumentPaths.Count == 0)
            {
                MessageBox.Show("Please upload at least one document.");
                return;
            }

            // Allowed file extensions
            string[] allowedExtensions = { ".pdf", ".xlsx", ".doc", ".docx" };

            foreach (var file in SelectedDocumentPaths)
            {
                string ext = System.IO.Path.GetExtension(file.FilePath).ToLower();
                if (!allowedExtensions.Contains(ext))
                {
                    MessageBox.Show($"Invalid file type: {ext}");
                    return;
                }
            }

            // SAVE CLAIM TO DATABASE
            using var context = new ApplicationDbContext();

            var claim = new Claim
            {
                LecturerID = currentProfile.LecturerID,
                Day = DateTime.Now.Day,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                TotalHours = hours,
                Amount = hours * hourlyRate,
                Status = "Documents uploaded",
                SubmittedDate = DateTime.Now,
                Documents = SelectedDocumentPaths.Select(f => f.FilePath).ToList(),

                // Default values to avoid NULL insert
                ApprovedBy = "Pending",
               
            };

            context.Claims.Add(claim);
            context.SaveChanges();


           

            MessageBox.Show($"Claim submitted with {claim.Documents.Count} document(s).");

            // Refresh claim list
            LoadMyClaims();

            // Clear form
            SelectedDocumentPaths.Clear();
            HoursBox.Clear();
            HourlyRateBox.Clear();
        }

        private void UploadDocument_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "All Files|*.*"
            };

            if (dlg.ShowDialog() == true)
            {
                string appDocsFolder =
                    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadedDocs");

                if (!System.IO.Directory.Exists(appDocsFolder))
                    System.IO.Directory.CreateDirectory(appDocsFolder);

                foreach (var file in dlg.FileNames)
                {
                    string destFile = System.IO.Path.Combine(appDocsFolder,
                        System.IO.Path.GetFileName(file));

                    if (!System.IO.File.Exists(destFile))
                        System.IO.File.Copy(file, destFile);

                    if (!SelectedDocumentPaths.Any(f => f.FilePath == destFile))
                    {
                        SelectedDocumentPaths.Add(new UploadedFile
                        {
                            FilePath = destFile
                        });
                    }
                }
            }
        }

        private void RemoveDocument_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is System.Windows.Controls.TextBlock tb &&
                tb.DataContext is UploadedFile file)
            {
                SelectedDocumentPaths.Remove(file);
            }
        }

        private void Document_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is System.Windows.Controls.TextBlock tb &&
                tb.DataContext is UploadedFile file)
            {
                if (System.IO.File.Exists(file.FilePath))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = file.FilePath,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Cannot open file: {ex.Message}");
                    }
                }
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow("Lecturer");
            this.Close();
        }

        private void LecturerWindow_Closing(object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            RolesWindow rolesWindow = new RolesWindow();
            rolesWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            rolesWindow.Show();
        }

        public class UploadedFile
        {
            public string FilePath { get; set; }
            public string FileName => System.IO.Path.GetFileName(FilePath);
        }
    }
}
