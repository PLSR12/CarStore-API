using CarStore.Domain.Repositories;

namespace CarStore.Infrastructure.DataAccess
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarStoreDbContext _dbContext;

        public UnitOfWork(CarStoreDbContext dbContext) => _dbContext = dbContext;

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
