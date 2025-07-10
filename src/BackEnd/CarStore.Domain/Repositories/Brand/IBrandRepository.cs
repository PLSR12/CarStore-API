namespace CarStore.Domain.Repositories.Brand
{
    public interface IBrandRepository
    {
        public Task<Entities.Brand?> GetById(Guid brandId);

    }
}
