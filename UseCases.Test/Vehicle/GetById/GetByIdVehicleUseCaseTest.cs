using CarStore.Application.UseCases.Vehicle;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;
using CommonTestUtilies.Entities;
using CommonTestUtilies.Mapper;
using CommonTestUtilies.Repositories;
using FluentAssertions;

namespace UseCases.Test.Vehicle.GetById
{
    public class GetByIdVehicleUseCaseTest
    {

        [Fact]
        public async Task Success()
        {
            var vehicle = VehicleBuilder.Build();
            var useCase = CreateUseCase(vehicle.Id, vehicle);
            var result = await useCase.Execute(vehicle.Id);
            result.Should().NotBeNull();
            result.Model.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Error()
        {
            var vehicle = VehicleBuilder.Build();
            var useCase = CreateUseCase(Guid.NewGuid(), vehicle);
            Func<Task> act = async () => await useCase.Execute(vehicle.Id);

            await act.Should()
               .ThrowAsync<ErrorOnValidationException>()
               .Where(ex => ex.ErrorsMessages.Contains(ResourceMessagesException.VEHICLE_NOT_FOUND));
        }

        private static GetByIdVehicleUseCase CreateUseCase(Guid vehicleId, CarStore.Domain.Entities.Vehicle vehicle)
        {
            var mapper = MapperBuilder.Build();
            var vehicleRepository = new VehicleRepositoryBuilder().GetById(vehicleId, vehicle).Build();
            return new GetByIdVehicleUseCase(mapper, vehicleRepository);
        }
    }
}
