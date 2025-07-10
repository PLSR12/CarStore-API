using CarStore.Communication.Response;

namespace CarStore.Application.UseCases.Vehicle.GetAll
{
    public interface IGetAllVehicleUseCase
    {
        public Task<ResponseVehiclesJson> Execute();

    }
}
