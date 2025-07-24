using CarStore.Application.UseCases.Vehicle.Delete;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;
using CommonTestUtilies.Entities;
using CommonTestUtilies.Repositories;
using FluentAssertions;

namespace UseCases.Test.Vehicle.Delete
{
    public class DeleteVehicleUseCaseTest
    {

        [Fact]
        public async Task Success()
        {
            var vehicle = VehicleBuilder.Build();
            var useCase = CreateUseCase(vehicle);
            Func<Task> act = async () =>
            {
                await useCase.Execute(vehicle.Id);
                ;
            };
            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Error_Vehicle_Not_Found()
        {
            var vehicle = VehicleBuilder.Build();
            var useCase = CreateUseCase(vehicle);
            Func<Task> act = async () =>
            {
                await useCase.Execute(Guid.NewGuid());
                ;
            };
            (await act.Should().ThrowAsync<NotFoundException>()).Where(e => e.Message.Equals(ResourceMessagesException.VEHICLE_NOT_FOUND));
        }



        private static DeleteVehicleUseCase CreateUseCase(CarStore.Domain.Entities.Vehicle vehicle)
        {
            var (user, _) = UserBuilder.Build();
            var vehicleRepository = new VehicleRepositoryBuilder().GetById(vehicle.Id, vehicle).Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DeleteVehicleUseCase(vehicleRepository, unitOfWork);
        }
    }
}