using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Models
{
    public class HotelDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = localhost ;TrustServerCertificate=True ;Initial Catalog = HotelDb ;User Id=SA ;Password=Hoaian98247;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccomodationPackage>()
                .Property(ap => ap.FeePerNight)
                .HasColumnType("decimal(18,2)");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<AccomodationType> AccomodationTypes { get; set; }
        public DbSet<AccomodationPackage> AccomodationPackages { get; set; }
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
