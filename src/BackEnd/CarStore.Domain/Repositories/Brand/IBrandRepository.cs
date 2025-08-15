using CarStore.Domain.Dtos;

namespace CarStore.Domain.Repositories.Brand
{
    public interface IBrandRepository
    {
        public Task<IList<Entities.Brand>> Get(BrandFilterDto filter);
        public Task<Entities.Brand?> GetById(Guid brandId);
        public Task Add(Entities.Brand brand);
        public void Update(Entities.Brand brand);
        public Task Delete(Guid brandId);


    }
}
