using CarStore.Domain.Entities;
using CarStore.Domain.Repositories.Brand;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Infrastructure.DataAccess.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly CarStoreDbContext _dbContext;

        public BrandRepository(CarStoreDbContext dbContext) => _dbContext = dbContext;

        public async Task<IList<Brand?>> Get()
        {
            //var cacheKey = $"vehicles:filter:{filterJson}";

            //var cachedVehicles = await _cacheService.GetAsync<List<Vehicle>>(cacheKey);

            //if (cachedVehicles is not null)
            //{
            //    return cachedVehicles;
            //}

            var query = _dbContext.Brands
                .AsQueryable();

            var brands = await query.ToListAsync();

            //await _cacheService.SetAsync(cacheKey, vehicles, TimeSpan.FromMinutes(10));

            return brands;

        }

        public async Task<Brand?> GetById(Guid brandId)
        {
            return await _dbContext
              .Brands
              .SingleOrDefaultAsync(u => u.Id == brandId);
        }

        public async Task Add(Brand brand) => await _dbContext.Brands.AddAsync(brand);

        public void Update(Brand brand) => _dbContext.Brands.Update(brand);

        public async Task Delete(Guid brandId)
        {
            var brand = await _dbContext.Brands.FindAsync(brandId);

            _dbContext.Brands.Remove(brand!);
        }
    }
}
