using System.Net;
using System.Text.Json;
using CarStore.Exceptions;
using CommonTestUtilies.Requests;
using CommonTestUtilies.Tokens;
using FluentAssertions;

namespace WebApi.Test.User.Update
{
    public class UpdateUserTest : CarStoreClassFixture
    {
        private const string METHOD = "user";
        private readonly Guid _userIdentifier;
        private readonly string _name;

        public UpdateUserTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
            _name = factory.GetName();

        }

        [Fact]
        public async Task Success()
        {
            var request = RequestUpdateUserJsonBuilder.Build();
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            var url = $"{METHOD}/{_userIdentifier}";

            var response = await DoPut(url, request, token);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }


        [Fact]
        public async Task Error_Empty_Name()
        {
            var request = RequestUpdateUserJsonBuilder.Build();
            request.Name = string.Empty;
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            var url = $"{METHOD}/{_userIdentifier}";
            var response = await DoPut(url, request, token);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();
            var expectedMessage = ResourceMessagesException.ResourceManager.GetString("NAME_EMPTY");

        }
    }
}
