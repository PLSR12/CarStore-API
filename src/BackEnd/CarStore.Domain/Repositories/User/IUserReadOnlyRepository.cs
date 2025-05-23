namespace CarStore.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserWithEmail(string email);
        public Task<bool> ExistActiveUserWithIdentifier(Guid userId);
        public Task<Entities.User?> GetByEmailAndPassword(string email, string password);
        public Task<Entities.User?> GetById(Guid userId);
    }
}
