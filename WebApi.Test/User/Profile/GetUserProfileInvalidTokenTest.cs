using System.Net;
using CommonTestUtilies.Tokens;
using FluentAssertions;

namespace WebApi.Test.User.Profile
{
    public class GetUserProfileInvalidTokenTest : CarStoreClassFixture
    {
        private readonly Guid _userIdentifier;
        private readonly string METHOD = "user";
        public GetUserProfileInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
        }


        [Fact]
        public async Task Error_Token_Invalid()
        {
            var url = $"{METHOD}/{_userIdentifier}";

            var response = await DoGet(url, token: "tokenInvalid");
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var url = $"{METHOD}/{_userIdentifier}";

            var response = await DoGet(url, token: string.Empty);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Token_With_User_NotFound()
        {
            var url = $"{METHOD}/{_userIdentifier}";

            var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid(), "Teste");
            var response = await DoGet(url, token);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
