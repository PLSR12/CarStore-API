using System.Net;
using System.Text.Json;
using CarStore.Exceptions;
using CommonTestUtilies.Requests;
using CommonTestUtilies.Tokens;
using FluentAssertions;

namespace WebApi.Test.Vehicle.Register
{
    public class UpdateVehicleTest : CarStoreClassFixture
    {

        private const string METHOD = "vehicle";
        private readonly Guid _userIdentifier;
        private readonly string _name;
        private readonly Guid _brandId;
        private readonly Guid _typeVehicleId;


        public UpdateVehicleTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _name = factory.GetName();
            _userIdentifier = factory.GetUserIdentifier();
            _brandId = factory.GetBrandId();
            _typeVehicleId = factory.GetTypeVehicleId();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestVehicleJsonBuilder.Build(_brandId.ToString(), _typeVehicleId.ToString());
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            var response = await DoPost(method: METHOD, request: request, token: token);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            responseData.RootElement.GetProperty("model").GetString().Should().Be(request.Model);
            responseData.RootElement.GetProperty("id").GetString().Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Error_Model_Empty()
        {
            var request = RequestVehicleJsonBuilder.Build(_brandId.ToString(), _typeVehicleId.ToString());
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            request.Model = string.Empty;
            var response = await DoPost(method: METHOD, request: request, token: token);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();
            var expectedMessage = ResourceMessagesException.ResourceManager.GetString("VEHICLE_MODEL_EMPTY");
            errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
        }
    }
}
