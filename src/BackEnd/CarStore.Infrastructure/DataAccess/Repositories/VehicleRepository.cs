using CarStore.Domain.Entities;
using CarStore.Domain.Repositories.Vehicle;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Infrastructure.DataAccess.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {

        private readonly CarStoreDbContext _dbContext;

        public VehicleRepository(CarStoreDbContext dbContext) => _dbContext = dbContext;

        public async Task<List<Vehicle>> GetAll()
        {
            return await _dbContext.Vehicles
              .Include(v => v.Owner)
              .Include(v => v.Brand)
              .Include(v => v.Type)
              .ToListAsync();
        }
        public async Task<Vehicle?> GetById(Guid vehicleId)
        {
            return await _dbContext
              .Vehicles
              .Include(v => v.Owner)
              .Include(v => v.Brand)
              .Include(v => v.Type)
              .SingleOrDefaultAsync(u => u.Id == vehicleId);
        }

        public async Task Add(Vehicle vehicle) => await _dbContext.Vehicles.AddAsync(vehicle);
    }
}
