using CarStore.Domain.Dtos;
using CarStore.Domain.Entities;
using CarStore.Domain.Repositories.Vehicle;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Infrastructure.DataAccess.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {

        private readonly CarStoreDbContext _dbContext;
        public VehicleRepository(CarStoreDbContext dbContext) => _dbContext = dbContext;
        public async Task<IList<Vehicle>> Get(VehicleFilterDto filter)
        {
            var query = _dbContext.Vehicles
                .Include(v => v.Owner)
                .Include(v => v.Brand)
                .Include(v => v.Type)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Model))
                query = query.Where(v => v.Model.Contains(filter.Model));

            if (!string.IsNullOrWhiteSpace(filter.OwnerName))
                query = query.Where(v => v.Owner != null && v.Owner.Name.Contains(filter.OwnerName));

            if (!string.IsNullOrWhiteSpace(filter.BrandName))
                query = query.Where(v => v.Brand != null && v.Brand.Name.Contains(filter.BrandName));

            if (filter.YearFabrication.HasValue)
                query = query.Where(v => v.YearFabrication == filter.YearFabrication.Value);

            return await query.ToListAsync();
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

        public void Update(Vehicle vehicle) => _dbContext.Vehicles.Update(vehicle);

        public async Task Delete(Guid vehicleId)
        {
            var vehicle = await _dbContext.Vehicles.FindAsync(vehicleId);

            _dbContext.Vehicles.Remove(vehicle!);
        }
    }
}
