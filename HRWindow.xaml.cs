using ST10446806_PROG6212_POEPART1.Data;
using ST10446806_PROG6212_POEPART1.Models;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using iTextSharp.text;
using iTextSharp.text.pdf;


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

        private void GenerateInvoice_Click(object sender, RoutedEventArgs e)
        {
            using var context = new ApplicationDbContext();

            // Load only APPROVED claims
            var approvedClaims = context.Claims
                .Where(c => c.Status == "Manager Approved" || c.Status == "Coordinator Approved")
                .ToList();

            if (approvedClaims.Count == 0)
            {
                MessageBox.Show("No approved claims found to generate an invoice.");
                return;
            }

            // Save PDF
            string folder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Invoices");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string pdfPath = Path.Combine(folder, $"InvoiceReport_{DateTime.Now:yyyyMMdd_HHmm}.pdf");

            // Create PDF document
            var doc = new iTextSharp.text.Document();
            PdfWriter.GetInstance(doc, new FileStream(pdfPath, FileMode.Create));
            doc.Open();

            // Title
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
            doc.Add(new iTextSharp.text.Paragraph("Monthly Invoice Report", titleFont));
            doc.Add(new iTextSharp.text.Paragraph($"Generated: {DateTime.Now}\n\n"));


            // Table
            PdfPTable table = new PdfPTable(5); // 5 columns
            table.AddCell("Lecturer ID");
            table.AddCell("Hours Worked");
            table.AddCell("Amount");
            table.AddCell("Month");
            table.AddCell("Status");

            foreach (var claim in approvedClaims)
            {
                table.AddCell(claim.LecturerID.ToString());
                table.AddCell(claim.TotalHours.ToString());
                table.AddCell($"R{claim.Amount:0.00}");
                table.AddCell($"{claim.Month}/{claim.Year}");
                table.AddCell(claim.Status);
            }

            doc.Add(table);
            doc.Close();

            MessageBox.Show($"Invoice Report Generated!\nSaved at:\n{pdfPath}");
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

