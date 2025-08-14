using System.Net;
using System.Text.Json;
using CarStore.Communication.Requests;
using CarStore.Exceptions;
using CommonTestUtilies.Requests;
using CommonTestUtilies.Tokens;
using FluentAssertions;

namespace WebApi.Test.User.ChangePassword
{
    public class ChangePasswordTest : CarStoreClassFixture
    {
        private const string METHOD = "user/change-password";
        private readonly string _password;
        private readonly string _email;
        private readonly string _name;
        private readonly Guid _userIdentifier;

        public ChangePasswordTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _password = factory.GetPassword();
            _email = factory.GetEmail();
            _userIdentifier = factory.GetUserIdentifier();
            _name = factory.GetName();
        }
        [Fact]
        public async Task Success()
        {
            var request = RequestChangePasswordJsonBuilder.Build();
            request.Password = _password;
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            var response = await DoPut(METHOD, request, token);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var loginRequest = new RequestLoginUserJson
            {
                Email = _email,
                Password = _password,
            };
            response = await DoPost("login", loginRequest);
            loginRequest.Password = request.NewPassword;
            response = await DoPost("login", loginRequest);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Error_NewPassword_Empty()
        {

            var request = new RequestChangePasswordJson
            {

                Password = _password,
                NewPassword = string.Empty
            };

            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier, _name);
            var response = await DoPut(METHOD, request, token);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();
            var expectedMessage = ResourceMessagesException.ResourceManager.GetString("PASSWORD_INVALID");
            errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
        }
    }
}
