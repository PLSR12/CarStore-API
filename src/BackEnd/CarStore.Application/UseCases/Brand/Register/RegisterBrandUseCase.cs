using AutoMapper;
using CarStore.Application.Dtos.Requests;
using CarStore.Communication.Response;
using CarStore.Domain.Cache;
using CarStore.Domain.Repositories;
using CarStore.Domain.Repositories.Brand;
using CarStore.Exceptions.ExceptionsBase;
using CarStore.Infrastructure.Services.LoggedUser;

namespace CarStore.Application.UseCases.Brand.Register
{
    public class RegisterBrandUseCase : IRegisterBrandUseCase
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public RegisterBrandUseCase(
        ILoggedUser LoggedUser,
        IBrandRepository brandRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService
        )
        {
            _brandRepository = brandRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<ResponseBrandJson> Execute(RequestBrandJson request)
        {
            Validate(request);
            var brand = _mapper.Map<Domain.Entities.Brand>(request);

            await _brandRepository.Add(brand);
            await _unitOfWork.Commit();
            await _cacheService.RemoveByPrefixAsync("brand:");

            return _mapper.Map<ResponseBrandJson>(brand);
        }

        private static void Validate(RequestBrandJson request)
        {
            var result = new BrandValidator().Validate(request);
            if (!result.IsValid)
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
        }
    }
}

