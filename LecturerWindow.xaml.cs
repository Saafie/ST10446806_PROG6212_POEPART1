using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ST10446806_PROG6212_POEPART1
{
    public partial class LecturerWindow : Window
    {
        private static List<Claim> claims = new List<Claim>();
        private static int claimCounter = 1;
        private LecturerProfile lecturer = new LecturerProfile { LecturerID = 1, HourlyRate = 350 };

        // Only use one collection for selected documents
        public ObservableCollection<UploadedFile> SelectedDocumentPaths { get; set; } = new ObservableCollection<UploadedFile>();

        public LecturerWindow()
        {
            InitializeComponent();
            ClaimList.ItemsSource = claims;
            DocumentsList.ItemsSource = SelectedDocumentPaths; // Bind ItemsControl
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!decimal.TryParse(HoursBox.Text, out decimal hours))
            {
                MessageBox.Show("Please enter a valid number of hours.");
                return;
            }

            if (SelectedDocumentPaths.Count == 0)
            {
                MessageBox.Show("Please upload at least one document before submitting the claim.");
                return;
            }

            var claim = new Claim
            {
                ClaimID = claimCounter++,
                LecturerID = lecturer.LecturerID,
                Day = DateTime.Now.Day,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                TotalHours = hours,
                Amount = hours * lecturer.HourlyRate,
                Status = "Documents uploaded",
                SubmittedDate = DateTime.Now,
                Documents = SelectedDocumentPaths.Select(f => f.FilePath).ToList()
            };

            claims.Add(claim);
            RefreshClaims();

            MessageBox.Show($"Claim submitted with {claim.Documents.Count} document(s).");

            // Clear uploaded documents and reset UI
            SelectedDocumentPaths.Clear();
            HoursBox.Clear();
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
                // Ensure the app folder exists
                string appDocsFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadedDocs");
                if (!System.IO.Directory.Exists(appDocsFolder))
                    System.IO.Directory.CreateDirectory(appDocsFolder);

                // Copy selected files to the app folder
                foreach (var file in dlg.FileNames)
                {
                    string destFile = System.IO.Path.Combine(appDocsFolder, System.IO.Path.GetFileName(file));
                    if (!System.IO.File.Exists(destFile))
                        System.IO.File.Copy(file, destFile);

                    if (!SelectedDocumentPaths.Any(f => f.FilePath == destFile))
                        SelectedDocumentPaths.Add(new UploadedFile { FilePath = destFile });
                }
            }
        }



        private void RemoveDocument_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is System.Windows.Controls.TextBlock tb && tb.DataContext is UploadedFile file)
            {
                SelectedDocumentPaths.Remove(file);
            }
        }

        private void Document_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is System.Windows.Controls.TextBlock tb)
            {
                string filePath = tb.Text;

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


        private void RefreshClaims()
        {
            ClaimList.ItemsSource = null;
            ClaimList.ItemsSource = claims;
        }

        private void LogoutButton(object sender, RoutedEventArgs e)
        {
            // Pass true to indicate this is logout navigation
            LoginWindow login = new LoginWindow("Lecturer");
            login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            login.Show();
            this.Close();
        }


        public static List<Claim> GetClaims() => claims;
    }
    public class UploadedFile
    {
        public string FilePath { get; set; }
        public string FileName => System.IO.Path.GetFileName(FilePath);
    }
}

    


