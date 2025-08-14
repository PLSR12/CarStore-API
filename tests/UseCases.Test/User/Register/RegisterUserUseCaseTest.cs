using CarStore.Application.UseCases.User.Register;
using CarStore.Domain.Extensions;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;
using CommonTestUtilies.Cryptography;
using CommonTestUtilies.Mapper;
using CommonTestUtilies.Repositories;
using CommonTestUtilies.Requests;
using CommonTestUtilies.Tokens;
using FluentAssertions;

namespace UseCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var useCase = CreateUseCase();


            Func<Task> act = async () => await useCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>()).Where(error => error.ErrorsMessages.Count == 1 && error.ErrorsMessages.Contains(ResourceMessagesException.NAME_EMPTY));
        }

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase(request.Email);


            Func<Task> act = async () => await useCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>()).Where(error => error.ErrorsMessages.Count == 1 && error.ErrorsMessages.Contains(ResourceMessagesException.EMAIL_ALREADY_EXISTS));
        }

        private static RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();


            if (email.NotEmpty())
            {
                readRepositoryBuilder.ExistActiverUserWithEmail(email);
            }

            return new RegisterUserUseCase(writeRepository, readRepositoryBuilder.Build(), unitOfWork, mapper, passwordEncripter);
        }

    }
}
