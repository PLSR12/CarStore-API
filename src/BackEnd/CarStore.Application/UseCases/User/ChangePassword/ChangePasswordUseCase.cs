using CarStore.Communication.Requests;
using CarStore.Domain.Repositories;
using CarStore.Domain.Repositories.User;
using CarStore.Domain.Security.Cryptography;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;
using CarStore.Infrastructure.Services.LoggedUser;

namespace CarStore.Application.UseCases.User.ChangePassword
{
    public class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordEncripter _passwordEncripter;
        public ChangePasswordUseCase(
            ILoggedUser loggedUser,
            IPasswordEncripter passwordEncripter,
            IUserUpdateOnlyRepository repository,
            IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _passwordEncripter = passwordEncripter;
        }



        public async Task Execute(RequestChangePasswordJson request)
        {
            var loggedUser = await _loggedUser.User();
            Validate(request, loggedUser);
            var user = await _repository.GetById(loggedUser.Id);
            user.Password = _passwordEncripter.Encrypt(request.NewPassword);
            _repository.Update(user);
            await _unitOfWork.Commit();

        }

        private void Validate(RequestChangePasswordJson request, Domain.Entities.User loggedUser)
        {
            var result = new ChangePasswordValidator().Validate(request);
            var currentPasswordEncripted = _passwordEncripter.Encrypt(request.Password);
            if (!currentPasswordEncripted.Equals(loggedUser.Password))
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.PASSWORD_INVALID));
            if (!result.IsValid)
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());

        }
    }
}
