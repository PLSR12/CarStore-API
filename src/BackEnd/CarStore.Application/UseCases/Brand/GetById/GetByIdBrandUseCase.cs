using AutoMapper;
using CarStore.Communication.Response;
using CarStore.Domain.Repositories.Brand;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;

namespace CarStore.Application.UseCases.Brand
{
    public class GetByIdBrandUseCase : IGetByIdBrandUseCase
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _repository;

        public GetByIdBrandUseCase(
        IMapper mapper,
        IBrandRepository repository
        )
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseBrandJson> Execute(Guid id)
        {
            var brand = await _repository.GetById(id);

            if (brand == null)
            {
                throw new ErrorOnValidationException([ResourceMessagesException.BRAND_NOT_FOUND]);
            }

            return _mapper.Map<ResponseBrandJson>(brand);
        }
    }
}
