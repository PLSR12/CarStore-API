using System.Net;
using System.Text.Json;
using CarStore.Exceptions;
using CommonTestUtilies.Requests;
using FluentAssertions;

namespace WebApi.Test.User.Register
{
    public class RegisterUserTest : CarStoreClassFixture
    {

        private readonly string method = "user";


        public RegisterUserTest(CustomWebApplicationFactory factory) : base(factory) { }

        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var response = await DoPost(method, request);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(request.Name);
        }

        [Fact]
        public async Task Error_Empty_Name()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var response = await DoPost(method, request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

            var expectedMessage = ResourceMessagesException.ResourceManager.GetString("NAME_EMPTY");

            errors.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task ErrorEmptyEmail()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = string.Empty;


            var response = await DoPost(method, request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

            var expectedMessage = ResourceMessagesException.ResourceManager.GetString("EMAIL_EMPTY");

            errors.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));
            // response.Name.Should().Be(request.Name);
        }

    }
}
