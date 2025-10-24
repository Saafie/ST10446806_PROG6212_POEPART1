using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using System;
    using System.Collections.Generic;

namespace ST10446806_PROG6212_POEPART1
{
    // Role options
    public enum UserRole
    {
        Lecturer,
        Coordinator,
        Manager
    }

    // General user class
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }  // plain text for prototype only
        public UserRole Role { get; set; }
    }

    // Lecturer-specific details
    public class LecturerProfile
    {
        public int LecturerID { get; set; }
        public int UserID { get; set; }
        public decimal HourlyRate { get; set; }
        public string BankDetails { get; set; }
    }

    // Monthly claim submitted by a lecturer
    public class Claim
    {
        public int ClaimID { get; set; }
        public int LecturerID { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalHours { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string ApprovedBy { get; set; }
        public List<string> Documents { get; set; } = new List<string>();

        public string DocumentStatus => Documents.Count == 0 ? "No docs" : $"{Documents.Count} doc(s)";
        public string DateFormatted => $"{Day}/{Month}/{Year}";
    

  
        

        public override string ToString()
        {
            // Show claim info with document status
            return $"Lecturer {LecturerID} | Claim #{ClaimID}: {TotalHours}h - R{Amount} - {Status} ({DocumentStatus})";
        }

        // Simple in-memory user store
        public static class UserRepository
        {
            public static List<User> Users = new List<User>
        {
            new User { UserID = 1, FullName = "John Doe", Username = "lecturer1", Password = "1234", Role = UserRole.Lecturer },
            new User { UserID = 2, FullName = "Sarah Smith", Username = "coordinator1", Password = "1234", Role = UserRole.Coordinator },
            new User { UserID = 3, FullName = "Michael Brown", Username = "manager1", Password = "1234", Role = UserRole.Manager }
        };
        }
    }
}

