using System.ComponentModel.DataAnnotations;

namespace ST10446806_PROG6212_POEPART1.Models
{
    public class LecturerProfile
    {
        [Key]
        public int LecturerID { get; set; }
        public int UserID { get; set; }
        public decimal HourlyRate { get; set; }
        public string BankDetails { get; set; } = string.Empty;
    }
}
