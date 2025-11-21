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

        public HRWindow(User user)
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
                
                BankDetailsTextBox.Text = lecturer.BankDetails ?? "";
            }
        }

        // Save changes or add a new lecturer
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("Full Name is required.");
                return;
            }

            if (selectedLecturer == null)
            {
                // Adding a new lecturer
                selectedLecturer = new LecturerProfile
                {
                    FullName = FullNameTextBox.Text,
                    Email = EmailTextBox.Text,
                    PhoneNumber = PhoneTextBox.Text,
                    BankDetails = BankDetailsTextBox.Text
                };

                

                context.LecturerProfiles.Add(selectedLecturer);
                context.SaveChanges();

                MessageBox.Show("New lecturer added successfully.");
            }
            else
            {
                // Editing existing lecturer
                selectedLecturer.FullName = FullNameTextBox.Text;
                selectedLecturer.Email = EmailTextBox.Text;
                selectedLecturer.PhoneNumber = PhoneTextBox.Text;

               

                selectedLecturer.BankDetails = BankDetailsTextBox.Text;

                context.LecturerProfiles.Update(selectedLecturer);
                context.SaveChanges();

                MessageBox.Show("Lecturer details updated successfully.");
            }

            // Refresh UI
            LoadLecturers();
            ClearFields();
        }

        private void ClearFields()
        {
            FullNameTextBox.Clear();
            EmailTextBox.Clear();
            PhoneTextBox.Clear();
            
            BankDetailsTextBox.Clear();
            LecturerListBox.SelectedItem = null;
            selectedLecturer = null;
        }

        // Close DB context when window closes
        protected override void OnClosed(System.EventArgs e)
        {
            base.OnClosed(e);
            context.Dispose();
        }
    }
}

