using CarStore.Communication.Response;
using CarStore.Domain.Dtos;

namespace CarStore.Application.UseCases.Brand.GetAll
{
    public interface IGetBrandUseCase
    {
        public Task<List<ResponseBrandJson>> Execute(BrandFilterDto filter);

    }
}
