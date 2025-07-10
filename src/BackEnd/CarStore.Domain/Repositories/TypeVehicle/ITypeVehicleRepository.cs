namespace CarStore.Domain.Repositories.TypeVehicle
{
    public interface ITypeVehicleRepository
    {
        public Task<Entities.TypesVehicle?> GetById(Guid typeId);

    }
}
