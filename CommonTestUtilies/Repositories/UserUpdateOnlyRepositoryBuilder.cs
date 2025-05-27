using CarStore.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilies.Repositories
{
    public class UserUpdateOnlyRepositoryBuilder
    {

        private readonly Mock<IUserUpdateOnlyRepository> _repository;
        public UserUpdateOnlyRepositoryBuilder() => _repository = new Mock<IUserUpdateOnlyRepository>();

        public IUserUpdateOnlyRepository Build() => _repository.Object;

    }
}
