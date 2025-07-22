using CarStore.Application.UseCases.Vehicle.GetAll;
using CarStore.Domain.Dtos;
using CarStore.Domain.Entities;
using CommonTestUtilies.Entities;
using CommonTestUtilies.Mapper;
using CommonTestUtilies.Repositories;
using FluentAssertions;

namespace UseCases.Test.Vehicle.Get
{
    public class GetVehicleUseCaseTest
    {
        [Fact]
        public async Task Success_WithOut_Filter()
        {
            var vehicles = VehicleBuilder.Collection(UserBuilder.Build().user, count: 4);
            var filter = new VehicleFilterDto();
            var useCase = CreateUseCase(vehicles, filter);
            var result = await useCase.Execute(filter);
            result.Should().NotBeNull();
            result.Vehicles.Should().NotBeNullOrEmpty();
            result.Vehicles.Should().HaveCount(vehicles.Count);
        }

        [Fact]
        public async Task Success_With_Filter_Brand()
        {
            var fordBrand = new Brand
            {
                Name = "Ford",
                Description = "Ford Company"
            };
            var vehicles = VehicleBuilder.Collection(UserBuilder.Build().user, count: 4, brand: fordBrand);
            var filter = new VehicleFilterDto
            {
                BrandName = "Ford",
            };
            var useCase = CreateUseCase(vehicles, filter);
            var result = await useCase.Execute(filter);
            result.Should().NotBeNull();
            result.Vehicles.Should().NotBeNullOrEmpty();
            result.Vehicles.Should().HaveCount(vehicles.Count);
            result.Vehicles.Should().OnlyContain(v => v.Brand.Name == fordBrand.Name);
        }

        [Fact]
        public async Task Success_With_Filter_Owner()
        {
            var user = UserBuilder.Build().user;
            var vehicles = VehicleBuilder.Collection(user);
            var filter = new VehicleFilterDto
            {
                OwnerName = user.Name,
            };
            var useCase = CreateUseCase(vehicles, filter);
            var result = await useCase.Execute(filter);
            result.Should().NotBeNull();
            result.Vehicles.Should().NotBeNullOrEmpty();
            result.Vehicles.Should().HaveCount(vehicles.Count);
            result.Vehicles.Should().OnlyContain(v => v.Owner.Name == user.Name);
        }

        [Fact]
        public async Task Success_With_Filter_Model()
        {
            var user = UserBuilder.Build().user;

            var brand = BrandBuilder.Build();

            string modelFilter = "Focus";

            var vehicles = VehicleBuilder.Collection(user, brand: brand, model: modelFilter);

            var filter = new VehicleFilterDto
            {
                Model = modelFilter,
            };

            var useCase = CreateUseCase(vehicles, filter);

            var result = await useCase.Execute(filter);

            result.Should().NotBeNull();
            result.Vehicles.Should().NotBeNullOrEmpty();
            result.Vehicles.Should().OnlyContain(v => v.Model == modelFilter);
        }


        [Fact]
        public async Task Success_With_Filter_Year_Fabrication()
        {
            var user = UserBuilder.Build().user;

            var brand = BrandBuilder.Build();

            int yearFilter = 2015;

            var vehicles = VehicleBuilder.Collection(user, brand: brand, yearFabrication: yearFilter);

            var filter = new VehicleFilterDto
            {
                YearFabrication = yearFilter,
            };

            var useCase = CreateUseCase(vehicles, filter);

            var result = await useCase.Execute(filter);

            result.Should().NotBeNull();
            result.Vehicles.Should().NotBeNullOrEmpty();
            result.Vehicles.Should().OnlyContain(v => v.YearFabrication == yearFilter);
        }

        [Fact]
        public async Task Success_With_Filter_Model_And_Brand()
        {
            var user = UserBuilder.Build().user;

            var brand = new Brand
            {
                Name = "Chevrolet",
                Description = "Chevrolet Motors"
            };

            var model = "Onix";

            var vehicles = VehicleBuilder.Collection(user, count: 3, brand: brand, model: model);

            var filter = new VehicleFilterDto
            {
                BrandName = brand.Name,
                Model = model
            };

            var useCase = CreateUseCase(vehicles, filter);

            var result = await useCase.Execute(filter);

            result.Should().NotBeNull();
            result.Vehicles.Should().NotBeNullOrEmpty();
            result.Vehicles.Should().HaveCount(vehicles.Count);
            result.Vehicles.Should().OnlyContain(v => v.Brand.Name == brand.Name && v.Model == model);
        }

        [Fact]
        public async Task Error_NoVehiclesFound_WithMultipleInvalidBrandAndModel()
        {
            var brand = new Brand { Name = "Chevrolet" };
            var vehicles = VehicleBuilder.Collection(UserBuilder.Build().user, count: 3, brand: brand, model: "Onix");

            var filter = new VehicleFilterDto
            {
                BrandName = "Tesla",
                Model = "Model S"
            };

            var useCase = CreateUseCase(vehicles, filter);

            var result = await useCase.Execute(filter);

            result.Should().NotBeNull();
            result.Vehicles.Should().BeEmpty();
        }


        [Fact]
        public async Task Error_NoVehiclesFound_WithInvalidBrand()
        {
            var brand = new Brand { Name = "Chevrolet" };
            var vehicles = VehicleBuilder.Collection(UserBuilder.Build().user, count: 3, brand: brand);

            var filter = new VehicleFilterDto
            {
                BrandName = "BMW" // valor inexistente
            };

            var useCase = CreateUseCase(vehicles, filter);

            var result = await useCase.Execute(filter);

            result.Should().NotBeNull();
            result.Vehicles.Should().BeEmpty();
        }

        [Fact]
        public async Task Error_NoVehiclesFound_WithInvalidOwner()
        {
            var user = UserBuilder.Build().user;
            var vehicles = VehicleBuilder.Collection(user);

            var filter = new VehicleFilterDto
            {
                OwnerName = "NomeInexistente"
            };

            var useCase = CreateUseCase(vehicles, filter);

            var result = await useCase.Execute(filter);

            result.Should().NotBeNull();
            result.Vehicles.Should().BeEmpty();
        }

        [Fact]
        public async Task Error_NoVehiclesFound_WithInvalidYearFabrication()
        {
            var user = UserBuilder.Build().user;
            var vehicles = VehicleBuilder.Collection(user, yearFabrication: 2015);

            var filter = new VehicleFilterDto
            {
                YearFabrication = 1999
            };

            var useCase = CreateUseCase(vehicles, filter);

            var result = await useCase.Execute(filter);

            result.Should().NotBeNull();
            result.Vehicles.Should().BeEmpty();
        }


        [Fact]
        public async Task Error_NoVehiclesFound_WithInvalidModel()
        {
            var user = UserBuilder.Build().user;
            var vehicles = VehicleBuilder.Collection(user, model: "Focus");

            var filter = new VehicleFilterDto
            {
                Model = "ModeloInexistente"
            };

            var useCase = CreateUseCase(vehicles, filter);

            var result = await useCase.Execute(filter);

            result.Should().NotBeNull();
            result.Vehicles.Should().BeEmpty();
        }


        private static GetVehicleUseCase CreateUseCase(IList<CarStore.Domain.Entities.Vehicle> vehicles, VehicleFilterDto filter)
        {
            var mapper = MapperBuilder.Build();
            var vehicleRepository = new VehicleRepositoryBuilder().Get(vehicles, filter).Build();
            return new GetVehicleUseCase(mapper, vehicleRepository);
        }
    }
}
