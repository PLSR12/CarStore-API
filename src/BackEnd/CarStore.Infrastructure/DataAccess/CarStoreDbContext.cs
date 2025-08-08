using CarStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Infrastructure.DataAccess
{
    public class CarStoreDbContext : DbContext
    {
        public CarStoreDbContext(DbContextOptions<CarStoreDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<TypesVehicle> TypesVehicle { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarStoreDbContext).Assembly);
        }

    }
}
