using CarStore.Communication.Requests;
using CarStore.Communication.Response;

namespace CarStore.Application.UseCases.Login.DoLogin
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseLoginUserJson> Execute(RequestLoginUserJson request);

    }
}
