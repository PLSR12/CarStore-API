using CarStore.Communication.Response;

namespace CarStore.Application.UseCases.Vehicle
{
    public interface IGetByIdVehicleUseCase
    {
        public Task<ResponseVehicleJson> Execute(Guid id);
    }
}
