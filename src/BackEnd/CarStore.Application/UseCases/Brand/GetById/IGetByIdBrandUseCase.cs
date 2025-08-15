using CarStore.Communication.Response;

namespace CarStore.Application.UseCases.Brand
{
    public interface IGetByIdBrandUseCase
    {
        public Task<ResponseBrandJson> Execute(Guid id);
    }
}
