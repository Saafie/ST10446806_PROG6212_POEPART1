using Microsoft.EntityFrameworkCore;
using ST10446806_POE.Models;

namespace ST10446806_PROG6212_POEPART1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Claim> Claims => Set<Claim>();
        public DbSet<LecturerProfile> LecturerProfiles => Set<LecturerProfile>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=ContractMonthlyPOE;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, FullName = "John Doe", Username = "lecturer1", Password = "1234", Role = UserRole.Lecturer },
                new User { UserID = 2, FullName = "Sarah Smith", Username = "coordinator1", Password = "1234", Role = UserRole.Coordinator },
                new User { UserID = 3, FullName = "Michael Brown", Username = "manager1", Password = "1234", Role = UserRole.Manager }
            );
        }
    }
}

