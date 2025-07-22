using CarStore.Application.UseCases.Vehicle.Register;
using CarStore.Domain.Entities;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;
using CommonTestUtilies.Entities;
using CommonTestUtilies.LoggedUser;
using CommonTestUtilies.Mapper;
using CommonTestUtilies.Repositories;
using CommonTestUtilies.Requests;
using FluentAssertions;

namespace UseCases.Test.Vehicle.Register
{
    public class RegisterVehicleUseCaseTest
    {

        [Fact]
        public async Task Success()
        {

            var brand = BrandBuilder.Build();
            var typeVehicle = TypesVehicleBuilder.Build();
            var vehicle = VehicleBuilder.Build(brand: brand, type: typeVehicle);


            var request = RequestVehicleJsonBuilder.Build(brand.Id.ToString(), typeVehicle.Id.ToString());

            var useCase = CreateUseCase(vehicle, brand, typeVehicle);

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Model.Should().Be(request.Model);
        }

        [Fact]
        public async Task Error_Model_Empty()
        {
            var brand = BrandBuilder.Build();
            var typeVehicle = TypesVehicleBuilder.Build();
            var vehicle = VehicleBuilder.Build(brand: brand, type: typeVehicle);

            var request = RequestVehicleJsonBuilder.Build(brand.Id.ToString(), typeVehicle.Id.ToString());
            request.Model = string.Empty;

            var useCase = CreateUseCase(vehicle, brand, typeVehicle);

            Func<Task> act = async () => await useCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>()).Where(error => error.ErrorsMessages.Count == 1 && error.ErrorsMessages.Contains(ResourceMessagesException.VEHICLE_MODEL_EMPTY));
        }

        [Fact]
        public async Task Error_BrandId_NotExists()
        {
            var brand = BrandBuilder.Build();
            var typeVehicle = TypesVehicleBuilder.Build();
            var vehicle = VehicleBuilder.Build(brand: brand, type: typeVehicle);


            var request = RequestVehicleJsonBuilder.Build(brand.Id.ToString(), typeVehicle.Id.ToString());
            request.BrandId = Guid.NewGuid().ToString();

            var useCase = CreateUseCase(vehicle, brand, typeVehicle);

            Func<Task> act = async () => await useCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>()).Where(error => error.ErrorsMessages.Count == 1 && error.ErrorsMessages.Contains(ResourceMessagesException.BRAND_NOT_FOUND));
        }

        [Fact]
        public async Task Error_TypeId_NotExists()
        {
            var brand = BrandBuilder.Build();
            var typeVehicle = TypesVehicleBuilder.Build();
            var vehicle = VehicleBuilder.Build(brand: brand, type: typeVehicle);

            var request = RequestVehicleJsonBuilder.Build(brand.Id.ToString(), typeVehicle.Id.ToString());

            request.TypeId = Guid.NewGuid().ToString();

            var useCase = CreateUseCase(vehicle, brand, typeVehicle);

            Func<Task> act = async () => await useCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>()).Where(error => error.ErrorsMessages.Count == 1 && error.ErrorsMessages.Contains(ResourceMessagesException.TYPE_VEHICLE_NOT_FOUND));
        }

        private static RegisterVehicleUseCase CreateUseCase(CarStore.Domain.Entities.Vehicle vehicle, Brand brand, TypesVehicle typesVehicle)
        {
            var (user, _) = UserBuilder.Build();
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var vehicleRepository = new VehicleRepositoryBuilder().GetById(vehicle.Id, vehicle).Build();
            var brandRepository = new BrandRepositoryBuilder().GetById(brand.Id, brand).Build();
            var typeVehicleRepository = new TypeVehicleRepositoryBuilder().GetById(typesVehicle.Id, typesVehicle).Build();
            var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder().GetById(user.Id, user).Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new RegisterVehicleUseCase(loggedUser, vehicleRepository, brandRepository, typeVehicleRepository, userReadOnlyRepository, unitOfWork, mapper);
        }
    }
}
