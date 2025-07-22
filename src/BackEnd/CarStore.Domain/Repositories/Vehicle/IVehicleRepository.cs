using CarStore.Domain.Dtos;

namespace CarStore.Domain.Repositories.Vehicle
{
    public interface IVehicleRepository
    {

        public Task<Entities.Vehicle?> GetById(Guid vehicleId);
        public Task<IList<Entities.Vehicle>> Get(VehicleFilterDto filter);
        public Task Add(Entities.Vehicle vehicle);
        public void Update(Entities.Vehicle vehicle);
        public Task Delete(Guid vehicleId);

    }
}
