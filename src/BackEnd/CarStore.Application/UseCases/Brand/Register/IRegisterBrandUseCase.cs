using CarStore.Application.Dtos.Requests;
using CarStore.Communication.Response;

namespace CarStore.Application.UseCases.Brand.Register
{
    public interface IRegisterBrandUseCase
    {
        public Task<ResponseBrandJson> Execute(RequestBrandJson request);

    }
}
