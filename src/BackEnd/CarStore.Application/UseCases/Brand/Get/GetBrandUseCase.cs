using AutoMapper;
using CarStore.Communication.Response;
using CarStore.Domain.Dtos;
using CarStore.Domain.Repositories.Brand;

namespace CarStore.Application.UseCases.Brand.GetAll
{
    public class GetBrandUseCase : IGetBrandUseCase
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _repository;

        public GetBrandUseCase(
        IMapper mapper,
        IBrandRepository repository
        )
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<ResponseBrandJson>> Execute(BrandFilterDto filter)
        {
            var brands = await _repository.Get(filter);

            var mapped = _mapper.Map<List<ResponseBrandJson>>(brands);

            return mapped;
        }
    }
}
