using CarStore.Domain.Entities;
using CarStore.Domain.Repositories.TypeVehicle;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Infrastructure.DataAccess.Repositories
{
    public class TypeVehicleRepository : ITypeVehicleRepository
    {
        private readonly CarStoreDbContext _dbContext;
        public TypeVehicleRepository(CarStoreDbContext dbContext) => _dbContext = dbContext;


        public async Task<TypesVehicle?> GetById(Guid typeId)
        {
            return await _dbContext
              .TypesVehicle
              .SingleOrDefaultAsync(u => u.Id == typeId);
        }
    }
}
