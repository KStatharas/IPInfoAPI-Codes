using IPInfoAPI_Codes.DTO;
using IPInfoAPI_Codes.Models;
using Microsoft.EntityFrameworkCore;

namespace IPInfoAPI_Codes.Data
{
    public class IPInfoAPIDbContext : DbContext
    {
        public IPInfoAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IPAddress>()
                .HasOne(p => p.Country)
                .WithMany(c => c.IPAddresses)
                .HasForeignKey(fk => fk.CountryId);
        }

        public DbSet<IPAddress> IPAddresses { get; set; }
        public DbSet<Country> Countries { get; set; }

    }
}
