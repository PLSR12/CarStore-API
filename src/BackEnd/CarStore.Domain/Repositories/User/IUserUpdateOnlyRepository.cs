namespace CarStore.Domain.Repositories.User
{

    public interface IUserUpdateOnlyRepository : IUserReadOnlyRepository
    {
        public void Update(Entities.User user);
    }
}
