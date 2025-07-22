using CarStore.Domain.Repositories;
using CarStore.Domain.Repositories.Vehicle;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;
using CarStore.Infrastructure.Services.LoggedUser;

namespace CarStore.Application.UseCases.Vehicle.Delete
{
    public class DeleteVehicleUseCase : IDeleteVehicleUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IVehicleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVehicleUseCase(
        ILoggedUser loggedUser,
        IVehicleRepository repository,
        IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid vehicleId)
        {
            var loggedUser = await _loggedUser.User();
            var vehicle = await _repository.GetById(vehicleId);
            if (vehicle is null)
                throw new NotFoundException(ResourceMessagesException.VEHICLE_NOT_FOUND);


            await _repository.Delete(vehicleId);
            await _unitOfWork.Commit();
        }
    }
}
