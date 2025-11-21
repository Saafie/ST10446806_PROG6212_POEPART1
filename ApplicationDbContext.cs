using Microsoft.EntityFrameworkCore;
using ST10446806_PROG6212_POEPART1.Models;

namespace ST10446806_PROG6212_POEPART1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<LecturerProfile> LecturerProfiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost\SQLEXPRESS;Database=ContractMonthlyPOE;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed users
            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, FullName = "John Doe", Username = "lecturer1", Password = "1234", Role = UserRole.Lecturer },
                new User { UserID = 2, FullName = "Sarah Smith", Username = "coordinator1", Password = "1234", Role = UserRole.Coordinator },
                new User { UserID = 3, FullName = "Michael Brown", Username = "manager1", Password = "1234", Role = UserRole.Manager }
            );

            // Specify decimal precision for Claims
            modelBuilder.Entity<Claim>().Property(c => c.Amount).HasPrecision(18, 2);
            modelBuilder.Entity<Claim>().Property(c => c.TotalHours).HasPrecision(10, 2);

            // Specify decimal precision for LecturerProfile
            modelBuilder.Entity<LecturerProfile>().Property(l => l.HourlyRate).HasPrecision(18, 2);

            // Keys
            modelBuilder.Entity<User>().HasKey(u => u.UserID);
            modelBuilder.Entity<Claim>().HasKey(c => c.ClaimID);
            modelBuilder.Entity<LecturerProfile>().HasKey(l => l.LecturerID);
        }
    }
    }
