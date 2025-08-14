using CarStore.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilies.Repositories
{
    public class UserWriteOnlyRepositoryBuilder
    {

        public static IUserWriteOnlyRepository Build()
        {
            var mock = new Mock<IUserWriteOnlyRepository>();

            return mock.Object;
        }
    }
}
