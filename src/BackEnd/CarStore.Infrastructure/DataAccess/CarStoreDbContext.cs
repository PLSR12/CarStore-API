using CarStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Infrastructure.DataAccess
{
    public class CarStoreDbContext : DbContext
    {
        public CarStoreDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarStoreDbContext).Assembly);
        }

    }
}
