using System.Net;
using CarStore.Communication.Requests;
using CommonTestUtilies.Tokens;
using FluentAssertions;

namespace WebApi.Test.User.ChangePassword
{
    public class ChangePasswordInvalidTokenTest : CarStoreClassFixture
    {
        private readonly string METHOD = "user/change-password";
        public ChangePasswordInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory) { }


        [Fact]
        public async Task Error_Token_Invalid()
        {
            var request = new RequestChangePasswordJson();
            var response = await DoPut(METHOD, request, token: "tokenInvalid");
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = new RequestChangePasswordJson();

            var response = await DoPut(METHOD, request, token: string.Empty);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Token_With_User_NotFound()
        {
            var request = new RequestChangePasswordJson();

            var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid(), "Teste");
            var response = await DoPut(METHOD, request, token);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
