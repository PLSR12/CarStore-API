using CarStore.Domain.Cache;
using CarStore.Domain.Dtos;
using CarStore.Domain.Entities;
using CarStore.Domain.Repositories.Brand;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CarStore.Infrastructure.DataAccess.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly CarStoreDbContext _dbContext;
        private readonly ICacheService _cacheService;


        public BrandRepository(CarStoreDbContext context, ICacheService cacheService)
        {
            _dbContext = context;
            _cacheService = cacheService;
        }

        public async Task<IList<Brand>> Get(BrandFilterDto filter)
        {
            var filterJson = JsonConvert.SerializeObject(filter);
            var cacheKey = $"brand:filter:{filterJson}";

            var cachedBrands = await _cacheService.GetAsync<List<Brand>>(cacheKey);

            if (cachedBrands is not null)
            {
                return cachedBrands;
            }

            var query = _dbContext.Brands
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(v => v.Name.Contains(filter.Name));

            var brands = await query.ToListAsync();

            await _cacheService.SetAsync(cacheKey, brands, TimeSpan.FromMinutes(10));

            return brands;

        }

        public async Task<Brand?> GetById(Guid brandId)
        {

            var cacheKey = $"brand:{brandId}";

            var cachedBrand = await _cacheService.GetAsync<Brand>(cacheKey);

            if (cachedBrand is not null)
            {
                return cachedBrand;
            }

            var brand = await _dbContext
              .Brands
              .SingleOrDefaultAsync(u => u.Id == brandId);

            await _cacheService.SetAsync(cacheKey, brand, TimeSpan.FromMinutes(10));
            return brand;
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
