using CarStore.Domain.Dtos;
using CarStore.Domain.Entities;
using CarStore.Domain.Repositories.Vehicle;
using Moq;

namespace CommonTestUtilies.Repositories
{
    public class VehicleRepositoryBuilder
    {
        private readonly Mock<IVehicleRepository> _repository;

        public VehicleRepositoryBuilder() => _repository = new Mock<IVehicleRepository>();

        public VehicleRepositoryBuilder Get(IList<Vehicle> vehicles, VehicleFilterDto filter)
        {
            //_repository
            //    .Setup(r => r.Get(It.IsAny<VehicleFilterDto>())) // <== aqui!
            //    .ReturnsAsync(vehicles);
            //return this;
            _repository
            .Setup(r => r.Get(It.IsAny<VehicleFilterDto>()))
            .ReturnsAsync((VehicleFilterDto f) =>
            {
                return vehicles
                    .Where(v =>
                        (string.IsNullOrWhiteSpace(f.Model) || v.Model == f.Model) &&
                        (string.IsNullOrWhiteSpace(f.OwnerName) || v.Owner?.Name == f.OwnerName) &&
                        (string.IsNullOrWhiteSpace(f.BrandName) || v.Brand?.Name == f.BrandName) &&
                        (!f.YearFabrication.HasValue || v.YearFabrication == f.YearFabrication))
                    .ToList();
            });

            return this;
        }

        public void GetById(Guid id, Vehicle? vehicle)
        {
            _repository
                .Setup(r => r.GetById(id))
                .ReturnsAsync(vehicle);
        }

        public void Add()
        {
            _repository
                .Setup(r => r.Add(It.IsAny<Vehicle>()))
                .Returns(Task.CompletedTask);
        }

        public void Update()
        {
            _repository
                .Setup(r => r.Update(It.IsAny<Vehicle>()));
        }

        public void Delete(Guid vehicleId)
        {
            _repository
                .Setup(r => r.Delete(vehicleId))
                .Returns(Task.CompletedTask);
        }

        public IVehicleRepository Build() => _repository.Object;
    }
}
