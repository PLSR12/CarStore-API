using CarStore.Communication.Requests;

namespace CarStore.Application.UseCases.User.Update;
public interface IUpdateUserUseCase
{
    public Task Execute(RequestUpdateUserJson request, Guid id);
}
