using System.Net;
using CommonTestUtilies.Requests;
using FluentAssertions;

namespace WebApi.Test.User.Update
{
    public class UpdateUserInvalidTokenTest : CarStoreClassFixture
    {
        private const string METHOD = "user";
        private Guid _userIdentifier;

        public UpdateUserInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
        }

        [Fact]
        public async Task Error_Token_Invalid()
        {
            var request = RequestUpdateUserJsonBuilder.Build();
            var url = $"{METHOD}/{_userIdentifier}";

            var response = await DoPut(url, request, token: "tokenInvalid");
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestUpdateUserJsonBuilder.Build();
            var url = $"{METHOD}/{_userIdentifier}";

            var response = await DoPut(url, request, token: string.Empty);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        }


    }
}
