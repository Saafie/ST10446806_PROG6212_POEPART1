using ST10446806_PROG6212_POEPART1.Data;
using ST10446806_PROG6212_POEPART1.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ST10446806_PROG6212_POEPART1
{
    public partial class HRWindow : Window
    {
        private ApplicationDbContext context;
        private LecturerProfile selectedLecturer;

        public HRWindow()
        {
            InitializeComponent();
            context = new ApplicationDbContext();
            LoadLecturers();
        }

        // Load lecturers into the ListBox
        private void LoadLecturers()
        {
            var lecturers = context.LecturerProfiles
                                   .OrderBy(l => l.LecturerID)
                                   .ToList();
            LecturerListBox.ItemsSource = lecturers;
        }

        // When a lecturer is selected from the list
        private void LecturerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LecturerListBox.SelectedItem is LecturerProfile lecturer)
            {
                selectedLecturer = lecturer;

                // Populate fields (some may be null)
                FullNameTextBox.Text = lecturer.FullName ?? "";
                EmailTextBox.Text = lecturer.Email ?? "";
                PhoneTextBox.Text = lecturer.PhoneNumber ?? "";
                HourlyRateTextBox.Text = lecturer.HourlyRate.ToString("0.00");
                BankDetailsTextBox.Text = lecturer.BankDetails ?? "";
            }
        }

        // Save changes or new details
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedLecturer == null)
            {
                MessageBox.Show("Please select a lecturer first.");
                return;
            }

            // Update fields from TextBoxes
            selectedLecturer.FullName = FullNameTextBox.Text;
            selectedLecturer.Email = EmailTextBox.Text;
            selectedLecturer.PhoneNumber = PhoneTextBox.Text;

            if (decimal.TryParse(HourlyRateTextBox.Text, out decimal rate))
                selectedLecturer.HourlyRate = rate;
            else
                MessageBox.Show("Invalid hourly rate. Keeping previous value.");

            selectedLecturer.BankDetails = BankDetailsTextBox.Text;

            // Save to database
            context.LecturerProfiles.Update(selectedLecturer);
            context.SaveChanges();

            MessageBox.Show("Lecturer details saved successfully.");
            LoadLecturers(); // refresh list
        }

        // Close DB context when window closes
        protected override void OnClosed(System.EventArgs e)
        {
            base.OnClosed(e);
            context.Dispose();
        }
    }
}

