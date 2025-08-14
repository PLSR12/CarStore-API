using CarStore.Domain.Entities;
using CarStore.Domain.Repositories.TypeVehicle;
using Moq;

namespace CommonTestUtilies.Repositories
{
    public class TypeVehicleRepositoryBuilder
    {
        private readonly Mock<ITypeVehicleRepository> _repository;

        public TypeVehicleRepositoryBuilder() => _repository = new Mock<ITypeVehicleRepository>();



        public TypeVehicleRepositoryBuilder GetById(Guid id, TypesVehicle? typesVehicle)
        {
            _repository
                .Setup(r => r.GetById(id))
                .ReturnsAsync(typesVehicle);

            return this;
        }

        public TypeVehicleRepositoryBuilder WithGetByIdReturn(TypesVehicle? typesVehicle)
        {
            _repository.Setup(m => m.GetById(It.IsAny<Guid>()))
                 .ReturnsAsync(typesVehicle);
            return this;
        }

        public ITypeVehicleRepository Build() => _repository.Object;
    }
}
