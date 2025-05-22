using CarStore.Domain.Entities;

namespace CarStore.Infrastructure.Services.LoggedUser
{
    public interface ILoggedUser
    {
        public Task<User> User();
    }
}
