using CarStore.Domain.Entities;
using CarStore.Domain.Repositories.Brand;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Infrastructure.DataAccess.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly CarStoreDbContext _dbContext;

        public BrandRepository(CarStoreDbContext dbContext) => _dbContext = dbContext;

        public async Task<Brand?> GetById(Guid brandId)
        {
            return await _dbContext
              .Brands
              .SingleOrDefaultAsync(u => u.Id == brandId);
        }
    }
}
