using System.Net;
using System.Text.Json;
using CommonTestUtilies.Tokens;
using FluentAssertions;

namespace WebApi.Test.Vehicle.GetById
{
    public class DeleteVehicleTest : CarStoreClassFixture
    {

        private const string METHOD = "vehicle";
        private readonly Guid _userIdentifier;
        private readonly string _name;
        private readonly Guid _vehicleId;
        private readonly string _vehicleModel;


        public DeleteVehicleTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _name = factory.GetName();
            _userIdentifier = factory.GetUserIdentifier();
            _vehicleId = factory.GetVehicleId();
            _vehicleModel = factory.GetVehicleModel();
        }

        [Fact]
        public async Task Success()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            var response = await DoGet($"{METHOD}/{_vehicleId}", token);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("id").GetGuid().Should().Be(_vehicleId);
            responseData.RootElement.GetProperty("model").GetString().Should().Be(_vehicleModel);
        }
    }
}
