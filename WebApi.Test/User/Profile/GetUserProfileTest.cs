using System.Net;
using System.Text.Json;
using CommonTestUtilies.Tokens;
using FluentAssertions;

namespace WebApi.Test.User.Profile
{
    public class GetUserProfileTest : CarStoreClassFixture
    {
        private readonly string METHOD = "user";
        private readonly string _name;
        private readonly string _email;
        private readonly Guid _userIdentifier;
        public GetUserProfileTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _name = factory.GetName();
            _email = factory.GetEmail();
            _userIdentifier = factory.GetUserIdentifier();
        }

        [Fact]
        public async Task Success()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            var url = $"{METHOD}/{_userIdentifier}";

            var response = await DoGet(url, token: token);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_name);
            responseData.RootElement.GetProperty("email").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_email);
        }
    }
}
