namespace CarStore.Application.UseCases.Brand.Delete
{
    public interface IDeleteBrandUseCase
    {
        public Task Execute(Guid brandId);
    }
}
