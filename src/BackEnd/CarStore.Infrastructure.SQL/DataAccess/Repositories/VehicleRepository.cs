using CarStore.Domain.Cache;
using CarStore.Domain.Dtos;
using CarStore.Domain.Entities;
using CarStore.Domain.Repositories.Vehicle;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CarStore.Infrastructure.DataAccess.Repositories
{

    public class VehicleDto
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string BrandName { get; set; }
        public string OwnerName { get; set; }
        public int? YearFabrication { get; set; }
    }
    public class VehicleRepository : IVehicleRepository
    {

        private readonly CarStoreDbContext _dbContext;
        private readonly ICacheService _cacheService;

        public VehicleRepository(CarStoreDbContext context, ICacheService cacheService)
        {
            _dbContext = context;
            _cacheService = cacheService;
        }
        public async Task<IList<Vehicle>> Get(VehicleFilterDto filter)
        {
            var filterJson = JsonConvert.SerializeObject(filter);
            var cacheKey = $"vehicle:filter:{filterJson}";

            var cachedVehicles = await _cacheService.GetAsync<List<Vehicle>>(cacheKey);

            if (cachedVehicles is not null)
            {
                return cachedVehicles;
            }

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

            var vehicles = await query.ToListAsync();

            await _cacheService.SetAsync(cacheKey, vehicles, TimeSpan.FromMinutes(10));

            return vehicles;

        }
        public async Task<Vehicle?> GetById(Guid vehicleId)
        {
            var cacheKey = $"vehicle:{vehicleId}";

            var cachedVehicle = await _cacheService.GetAsync<Vehicle>(cacheKey);

            if (cachedVehicle is not null)
            {
                return cachedVehicle;
            }

            var vehicle = await _dbContext
                      .Vehicles
                      .Include(v => v.Owner)
                      .Include(v => v.Brand)
                      .Include(v => v.Type)
                      .SingleOrDefaultAsync(u => u.Id == vehicleId);
            await _cacheService.SetAsync(cacheKey, vehicle, TimeSpan.FromMinutes(10));
            return vehicle;
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
