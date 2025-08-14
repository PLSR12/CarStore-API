using CarStore.Domain.Entities;
using CarStore.Domain.Repositories.Brand;
using Moq;

namespace CommonTestUtilies.Repositories
{
    public class BrandRepositoryBuilder
    {
        private readonly Mock<IBrandRepository> _repository;

        public BrandRepositoryBuilder() => _repository = new Mock<IBrandRepository>();



        public BrandRepositoryBuilder GetById(Guid id, Brand? brand)
        {
            _repository
                .Setup(r => r.GetById(id))
                .ReturnsAsync(brand);

            return this;
        }

        public BrandRepositoryBuilder WithGetByIdReturn(Brand? brand)
        {
            _repository.Setup(m => m.GetById(It.IsAny<Guid>()))
                 .ReturnsAsync(brand);
            return this;
        }

        public IBrandRepository Build() => _repository.Object;
    }
}
