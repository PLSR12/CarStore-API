using CarStore.Application.UseCases.User.Profile;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;
using CommonTestUtilies.Entities;
using CommonTestUtilies.Mapper;
using CommonTestUtilies.Repositories;
using FluentAssertions;

namespace UseCases.Test.User.Profile
{

    public class GetUserProfileUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, var _) = UserBuilder.Build();
            var repositoryBuilder = new UserReadOnlyRepositoryBuilder();
            repositoryBuilder.GetById(user.Id, user);

            var useCase = CreateUseCase(repositoryBuilder);

            var result = await useCase.Execute(user.Id);

            result.Should().NotBeNull();
            result.Name.Should().Be(user.Name);
            result.Email.Should().Be(user.Email);
        }

        [Fact]
        public async Task Should_Throw_When_User_Not_Found()
        {
            var (user, _) = UserBuilder.Build();
            var repositoryBuilder = new UserReadOnlyRepositoryBuilder();
            repositoryBuilder.GetById(Guid.NewGuid(), null);

            var useCase = CreateUseCase(repositoryBuilder);

            Func<Task> act = async () => await useCase.Execute(user.Id);

            var exception = await act.Should()
                .ThrowAsync<ErrorOnValidationException>();

            exception.Which.ErrorsMessages.Should()
                .Contain(ResourceMessagesException.USER_NOT_FOUND);
        }


        private static GetUserProfileUseCase CreateUseCase(UserReadOnlyRepositoryBuilder repositoryBuilder)
        {
            var mapper = MapperBuilder.Build();

            return new GetUserProfileUseCase(mapper, repositoryBuilder.Build());
        }
    }
}
