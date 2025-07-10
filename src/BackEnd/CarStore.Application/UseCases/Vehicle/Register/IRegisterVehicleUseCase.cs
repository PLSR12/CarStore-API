using CarStore.Communication.Requests;
using CarStore.Communication.Response;

namespace CarStore.Application.UseCases.Vehicle.Register
{
    public interface IRegisterVehicleUseCase
    {
        public Task<ResponseVehicleJson> Execute(RequestVehicleJson request);

    }
}
