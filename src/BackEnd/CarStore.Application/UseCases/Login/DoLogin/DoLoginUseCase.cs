using CarStore.Communication.Requests;
using CarStore.Communication.Response;
using CarStore.Domain.Repositories.User;
using CarStore.Domain.Security.Cryptography;
using CarStore.Domain.Security.Tokens;
using CarStore.Exceptions.ExceptionsBase;

namespace CarStore.Application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator;

        public DoLoginUseCase(IUserReadOnlyRepository repository, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
        {
            _repository = repository;
            _passwordEncripter = passwordEncripter;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<ResponseLoginUserJson> Execute(RequestLoginUserJson request)
        {
            var encriptedPassword = _passwordEncripter.Encrypt(request.Password);

            var user = await _repository.GetByEmailAndPassword(request.Email, encriptedPassword);

            return user is null
                ? throw new InvalidLoginException()
                : new ResponseLoginUserJson
                {
                    Id = user.Id,
                    Name = user.Name,
                    OwnerCar = user.OwnerCar,
                    Tokens = new ResponseTokensJson
                    {
                        AccessToken = _accessTokenGenerator.Generate(user.Id, user.Name)
                    }
                };
        }
    }
}
