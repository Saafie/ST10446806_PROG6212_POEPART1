
    using System.ComponentModel;
    using System.Security.Claims;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    namespace ST10446806_PROG6212_POEPART1
    {
        public partial class LecturerWindow : Window
        {
            private static List<Claim> claims = new List<Claim>();
            private static int claimCounter = 1;
            private LecturerProfile lecturer = new LecturerProfile { LecturerID = 1, HourlyRate = 350 };

            public LecturerWindow()
            {
                InitializeComponent();
                RefreshClaims();
                this.Closing += Close_LecturerWindow;
            }

            private void Close_LecturerWindow(object sender, CancelEventArgs e)
            {
                LoginWindow login = new LoginWindow("Lecturer");
                login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                login.Show();
            }

            

            private void Submit_Click(object sender, RoutedEventArgs e)
            {
                if (decimal.TryParse(HoursBox.Text, out decimal hours))
                {
                    var claim = new Claim
                    {
                        ClaimID = claimCounter++,
                        LecturerID = lecturer.LecturerID,
                        Day = DateTime.Now.Day,
                        Month = DateTime.Now.Month,
                        Year = DateTime.Now.Year,
                        TotalHours = hours,
                        Amount = hours * lecturer.HourlyRate,
                        Status = "Submission pending",
                        SubmittedDate = DateTime.Now
                    };

                    MessageBox.Show("Claim submitted");
                    claims.Add(claim);
                    RefreshClaims();
                }
                else
                {
                    MessageBox.Show("Please enter a valid number of hours.");
                }
            }

            private void UploadDocument_Click(object sender, RoutedEventArgs e)
            {
                if (claims.Count == 0)
                {
                    MessageBox.Show("Please submit a claim before uploading a document.", "No Claim Found");
                    return;
                }

                // Create file dialog
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
                {
                    Title = "Select Document to Upload",
                    Filter = "PDF Files (*.pdf)|*.pdf|Image Files (*.jpg;*.png)|*.jpg;*.png|All Files (*.*)|*.*"
                };

                if (dlg.ShowDialog() == true)
                {
                    // Get the most recent claim
                    var latestClaim = claims.Last();

                    // Save the file path
                    latestClaim.DocumentPath = dlg.FileName;

                    MessageBox.Show($"Document '{System.IO.Path.GetFileName(dlg.FileName)}' linked to Claim #{latestClaim.ClaimID}.", "Upload Successful");

                    RefreshClaims();
                }
            }

            private void ViewDocument_Click(object sender, RoutedEventArgs e)
            {
                var selectedClaim = (Claim)ClaimList.SelectedItem;
                if (selectedClaim == null)
                {
                    MessageBox.Show("Please select a claim first.");
                    return;
                }

                if (string.IsNullOrEmpty(selectedClaim.DocumentPath))
                {
                    MessageBox.Show("No document attached to this claim.");
                    return;
                }

                // Open the document using the default app
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = selectedClaim.DocumentPath,
                    UseShellExecute = true
                });
            }


            private void RefreshClaims()
            {
                ClaimList.ItemsSource = null;
                ClaimList.ItemsSource = claims.Select(c => new
                {
                    c.ClaimID,
                    c.TotalHours,
                    c.Amount,
                    c.Status,
                    DateFormatted = $"{c.Day}/{c.Month}/{c.Year}",
                    HasDocument = string.IsNullOrEmpty(c.DocumentPath) ? "No" : "Yes"
                }).ToList();
            }


            private void LogoutButton(object sender, RoutedEventArgs e)
            {
                Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault()?.Show();
                this.Close();
            }

            public static List<Claim> GetClaims() => claims;
        }
    }

  