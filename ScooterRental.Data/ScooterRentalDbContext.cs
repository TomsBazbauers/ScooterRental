using Microsoft.EntityFrameworkCore;
using ScooterRental.Core.Models;
using System.Threading.Tasks;

namespace ScooterRental.Data
{
    public class ScooterRentalDbContext : DbContext, IScooterRentalDbContext
    {
        public ScooterRentalDbContext(DbContextOptions options) : base(options)
        { }

        public ScooterRentalDbContext() { }

        public DbSet<Scooter> Scooters { get; set; }

        public DbSet<RentalReport> RentalReports { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
