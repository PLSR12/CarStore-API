using CarStore.Domain.Entities;
using CarStore.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
    {
        private readonly CarStoreDbContext _dbContext;

        public UserRepository(CarStoreDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }
        public async Task<bool> ExistActiveUserWithEmail(string email)
        {
            return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
        }
        public async Task<User?> GetByEmailAndPassword(string email, string password)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email) && user.Password.Equals(password));
        }

        public async Task<bool> ExistActiveUserWithIdentifier(Guid userId)
        {
            return await _dbContext.Users.AnyAsync(user => user.Id.Equals(userId) && user.Active);
        }
        public async Task<User> GetById(Guid userId)
        {
            return await _dbContext
            .Users
            .FirstAsync(user => user.Id == userId);
        }
    }
}
