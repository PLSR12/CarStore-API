using CarStore.Communication.Response;

namespace CarStore.Application.UseCases.Brand.GetAll
{
    public interface IGetBrandUseCase
    {
        public Task<List<ResponseBrandJson>> Execute();

    }
}
