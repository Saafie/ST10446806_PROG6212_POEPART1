using Microsoft.EntityFrameworkCore;

namespace ST10446806_PROG6212_POEPART1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<LecturerProfile> LecturerProfiles { get; set; }
        public DbSet<Claim> Claims { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=ContractMonthlyPOE;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.UserID);
            modelBuilder.Entity<Claim>().HasKey(c => c.ClaimID);
            modelBuilder.Entity<LecturerProfile>().HasKey(l => l.LecturerID);

            // Specify precision for decimals
            modelBuilder.Entity<Claim>()
                .Property(c => c.TotalHours)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Claim>()
                .Property(c => c.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<LecturerProfile>()
                .Property(l => l.HourlyRate)
                .HasPrecision(18, 2);

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, FullName = "Doe Eye", Username = "lecturer1", Password = "1234", Role = UserRole.Lecturer },
                new User { UserID = 2, FullName = "John Smith", Username = "coordinator1", Password = "1234", Role = UserRole.Coordinator },
                new User { UserID = 3, FullName = "Michael Jackson", Username = "manager1", Password = "1234", Role = UserRole.Manager },
                new User { UserID = 4, FullName = "Alice White", Username = "lecturer2", Password = "1234", Role = UserRole.Lecturer },
                new User { UserID = 5, FullName = "Miss Piggy", Username = "coordinator2", Password = "1234", Role = UserRole.Coordinator },
                new User { UserID = 6, FullName = "Linken Black", Username = "manager2", Password = "1234", Role = UserRole.Manager },
                new User { UserID = 7, FullName = "Emma HR", Username = "hr1", Password = "1234", Role = UserRole.HR },
                 new User { UserID = 7, FullName = "Kermit Frog", Username = "hr2", Password = "1234", Role = UserRole.HR }
            );



            // Seed LecturerProfiles
            modelBuilder.Entity<LecturerProfile>().HasData(
    new LecturerProfile
    {
        LecturerID = 1,
        UserID = 1,
        HourlyRate = 300,
        BankDetails = "Bank A",
        Email = "doe.eye@example.com",
        PhoneNumber = "1234567890"
    },
    new LecturerProfile
    {
        LecturerID = 2,
        UserID = 4,
        HourlyRate = 350,
        BankDetails = "Bank B",
        Email = "jane.doe@example.com",
        PhoneNumber = "0987654321"
    }
);

        }


    }
}

