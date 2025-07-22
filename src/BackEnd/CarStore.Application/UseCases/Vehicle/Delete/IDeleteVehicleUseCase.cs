namespace CarStore.Application.UseCases.Vehicle.Delete
{
    public interface IDeleteVehicleUseCase
    {
        public Task Execute(Guid vehicleId);
    }
}
