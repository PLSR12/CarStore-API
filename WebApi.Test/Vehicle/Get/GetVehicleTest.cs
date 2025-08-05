using System.Net;
using System.Text.Json;
using CarStore.Domain.Dtos;
using CommonTestUtilies.Tokens;
using FluentAssertions;

namespace WebApi.Test.Vehicle.Get
{
    public class GetVehicleTest : CarStoreClassFixture
    {
        private const string METHOD = "vehicle";
        private readonly Guid _userIdentifier;
        private readonly string _name;
        private readonly string _vehicleModel;
        private readonly string _vehicleBrand;
        private readonly int? _vehicleYearFabrication;


        public GetVehicleTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _name = factory.GetName();
            _userIdentifier = factory.GetUserIdentifier();
            _vehicleBrand = factory.GetVehicleBrand();
            _vehicleYearFabrication = factory.GetVehicleYearFabrication();
        }


        [Fact]
        public async Task SuccessWithOutFilter()
        {

            var filter = new VehicleFilterDto();
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            var response = await DoGet($"{METHOD}", token);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            var vehiclesList = responseData.RootElement;

            vehiclesList.ValueKind.Should().Be(JsonValueKind.Array);
            vehiclesList.GetArrayLength().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task SuccessWithFilterBrand()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            var response = await DoGet($"{METHOD}?BrandName={_vehicleBrand}", token);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            var vehiclesList = responseData.RootElement;

            vehiclesList.ValueKind.Should().Be(JsonValueKind.Array);
            vehiclesList.GetArrayLength().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task ErrorWithFilterBrand()
        {

            var filter = new VehicleFilterDto();
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            var response = await DoGet($"{METHOD}?BrandName=ABC", token);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            var vehiclesList = responseData.RootElement;

            vehiclesList.ValueKind.Should().Be(JsonValueKind.Array);
            vehiclesList.GetArrayLength().Should().BeLessThanOrEqualTo(0);
        }

        [Fact]
        public async Task SuccessWithFilterModel()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            var response = await DoGet($"{METHOD}?Model={_vehicleModel}", token);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            var vehiclesList = responseData.RootElement;

            vehiclesList.GetArrayLength().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task ErrorWithFilterModel()
        {

            var filter = new VehicleFilterDto();
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            var response = await DoGet($"{METHOD}?Model=ABC", token);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            var vehiclesList = responseData.RootElement;

            vehiclesList.ValueKind.Should().Be(JsonValueKind.Array);
            vehiclesList.GetArrayLength().Should().BeLessThanOrEqualTo(0);

        }

        [Fact]
        public async Task SuccessWithFilterYearFabrication()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            var response = await DoGet($"{METHOD}?YearFabrication={_vehicleYearFabrication}", token);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            var vehiclesList = responseData.RootElement;


            vehiclesList.GetArrayLength().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task ErrorWithFilterYearFabrication()
        {

            var filter = new VehicleFilterDto();
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            var response = await DoGet($"{METHOD}?YearFabrication=1000", token);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            var vehiclesList = responseData.RootElement;

            vehiclesList.GetArrayLength().Should().BeLessThanOrEqualTo(0);

        }
    }
}
