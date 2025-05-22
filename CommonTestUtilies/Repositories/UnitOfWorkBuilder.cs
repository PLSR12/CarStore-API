using CarStore.Domain.Repositories;
using Moq;

namespace CommonTestUtilies.Repositories
{
    public class UnitOfWorkBuilder
    {

        public static IUnitOfWork Build()
        {
            var mock = new Mock<IUnitOfWork>();

            return mock.Object;
        }
    }
}
