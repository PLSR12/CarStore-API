using CarStore.Domain.Repositories;
using CarStore.Domain.Repositories.Vehicle;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;

namespace CarStore.Application.UseCases.Vehicle.Delete
{
    public class DeleteVehicleUseCase : IDeleteVehicleUseCase
    {
        private readonly IVehicleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVehicleUseCase(
        IVehicleRepository repository,
        IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid vehicleId)
        {
            var vehicle = await _repository.GetById(vehicleId);
            if (vehicle is null)
                throw new NotFoundException(ResourceMessagesException.VEHICLE_NOT_FOUND);


            await _repository.Delete(vehicleId);
            await _unitOfWork.Commit();
        }
    }
}
