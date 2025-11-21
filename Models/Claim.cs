using System;
using System.Collections.Generic;

namespace ST10446806_PROG6212_POEPART1.Models
{
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
        public List<string> Documents { get; set; } = new List<string>();

        // Approval properties
        public string? ApprovedBy { get; set; }         // nullable string
        public DateTime? ApprovedDate { get; set; }    // nullable DateTime
    }
}

