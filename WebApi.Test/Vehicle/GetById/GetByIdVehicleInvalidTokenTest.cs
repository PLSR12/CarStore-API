using System.Net;
using CommonTestUtilies.Tokens;
using FluentAssertions;

namespace WebApi.Test.Vehicle.GetById
{
    public class GetByIdVehicleInvalidTokenTest : CarStoreClassFixture
    {
        private readonly Guid _vehicleId;

        public GetByIdVehicleInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _vehicleId = factory.GetVehicleId();
        }

        private readonly string METHOD = "vehicle";


        [Fact]
        public async Task Error_Token_Invalid()
        {
            var url = $"{METHOD}/{_vehicleId}";

            var response = await DoGet(url, token: "tokenInvalid");
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var url = $"{METHOD}/{_vehicleId}";

            var response = await DoGet(url, token: string.Empty);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Token_With_User_NotFound()
        {
            var url = $"{METHOD}/{_vehicleId}";

            var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid(), "Teste");
            var response = await DoGet(url, token);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
