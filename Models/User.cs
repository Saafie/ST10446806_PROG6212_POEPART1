using System.ComponentModel.DataAnnotations;

namespace ST10446806_PROG6212_POEPART1.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
