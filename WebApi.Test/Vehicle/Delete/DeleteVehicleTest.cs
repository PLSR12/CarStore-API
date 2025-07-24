using System.Net;
using System.Text.Json;
using CarStore.Exceptions;
using CommonTestUtilies.Tokens;
using FluentAssertions;

namespace WebApi.Test.Vehicle.Delete
{
    public class DeleteVehicleTest : CarStoreClassFixture
    {

        private const string METHOD = "vehicle";
        private readonly Guid _userIdentifier;
        private readonly string _name;
        private readonly Guid _vehicleId;


        public DeleteVehicleTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _name = factory.GetName();
            _userIdentifier = factory.GetUserIdentifier();
            _vehicleId = factory.GetVehicleId();
        }

        [Fact]
        public async Task Success()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            var response = await DoDelete($"{METHOD}/{_vehicleId}", token);
            response = await DoGet($"{METHOD} / {_vehicleId}", token);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Vehicle_Not_Found()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            var response = await DoDelete($"{METHOD}/1", token);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();
            var expectedMessage = ResourceMessagesException.ResourceManager.GetString("VEHICLE_NOT_FOUND");
            errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
        }
    }
}
