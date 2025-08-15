using CarStore.Domain.Cache;
using CarStore.Domain.Repositories;
using CarStore.Domain.Repositories.Brand;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;

namespace CarStore.Application.UseCases.Brand.Delete
{
    public class DeleteBrandUseCase : IDeleteBrandUseCase
    {
        private readonly IBrandRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public DeleteBrandUseCase(
        IBrandRepository repository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService
        )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task Execute(Guid brandId)
        {
            var brand = await _repository.GetById(brandId);
            if (brand is null)
                throw new NotFoundException(ResourceMessagesException.BRAND_NOT_FOUND);


            await _repository.Delete(brandId);
            await _unitOfWork.Commit();
            await _cacheService.RemoveByPrefixAsync("brand:");
        }
    }
}
