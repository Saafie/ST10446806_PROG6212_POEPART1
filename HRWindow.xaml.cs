using ST10446806_PROG6212_POEPART1.Data;
using ST10446806_PROG6212_POEPART1.Models;
using System;
using System.Linq;
using System.Windows;

namespace ST10446806_PROG6212_POEPART1.Helpers
{
    public static class HRHelper
    {
        public static void UpdateLecturerProfile(LecturerProfile lecturer)
        {
            using var context = new ApplicationDbContext();

            // Find the existing lecturer record by LecturerID
            var existing = context.LecturerProfiles
                .FirstOrDefault(l => l.LecturerID == lecturer.LecturerID);

            if (existing == null)
            {
                MessageBox.Show("Lecturer not found in the database.");
                return;
            }

            // Update the existing lecturer with new values
            existing.FullName = lecturer.FullName;
            existing.Email = lecturer.Email;
            existing.PhoneNumber = lecturer.PhoneNumber;
            existing.HourlyRate = lecturer.HourlyRate;
            existing.BankDetails = lecturer.BankDetails;

            try
            {
                context.SaveChanges();
                MessageBox.Show("Lecturer information updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating lecturer: {ex.Message}");
            }
        }
    }
}
