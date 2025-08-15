using CarStore.Application.Dtos.Requests;
using CarStore.Communication.Response;

namespace CarStore.Application.UseCases.Brand.Update
{
    public interface IUpdateBrandUseCase
    {
        public Task<ResponseBrandJson> Execute(RequestBrandJson request, Guid vehicleId);

    }
}
