using CarStore.Communication.Requests;
using CarStore.Communication.Response;

namespace CarStore.Application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);


    }
}
