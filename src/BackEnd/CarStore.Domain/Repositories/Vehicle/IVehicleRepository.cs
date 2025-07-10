namespace CarStore.Domain.Repositories.Vehicle
{
    public interface IVehicleRepository
    {

        public Task<Entities.Vehicle?> GetById(Guid vehicleId);

        public Task<List<Entities.Vehicle>> GetAll();
        public Task Add(Entities.Vehicle vehicle);

    }
}
