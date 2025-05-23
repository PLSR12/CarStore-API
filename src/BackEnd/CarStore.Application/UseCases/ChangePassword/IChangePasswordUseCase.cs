using CarStore.Communication.Requests;

namespace CarStore.Application.UseCases.User.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        public Task Execute(RequestChangePasswordJson request);

    }
}
