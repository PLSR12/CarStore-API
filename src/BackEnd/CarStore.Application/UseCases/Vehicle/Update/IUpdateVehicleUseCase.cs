using CarStore.Communication.Requests;
using CarStore.Communication.Response;

namespace CarStore.Application.UseCases.Vehicle.Update
{
    public interface IUpdateVehicleUseCase
    {
        public Task<ResponseVehicleJson> Execute(RequestVehicleJson request, Guid vehicleId);

    }
}
