using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ST10446806_PROG6212_POEPART1.Models
{
    public class Claim
    {
        [Key]
        public int ClaimID { get; set; }
        public int LecturerID { get; set; }
        public DateTime SubmittedDate { get; set; } = DateTime.Now;
        public decimal TotalHours { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Submitted";
        public string ApprovedBy { get; set; } = string.Empty;
        public List<string> Documents { get; set; } = new List<string>();
    }
}
