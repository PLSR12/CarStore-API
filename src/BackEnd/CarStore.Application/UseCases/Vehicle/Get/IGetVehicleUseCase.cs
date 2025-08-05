using CarStore.Communication.Response;
using CarStore.Domain.Dtos;

namespace CarStore.Application.UseCases.Vehicle.GetAll
{
    public interface IGetVehicleUseCase
    {
        public Task<List<ResponseVehicleJson>> Execute(VehicleFilterDto filter);

    }
}
