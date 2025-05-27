using CarStore.Communication.Requests;
using CarStore.Domain.Repositories;
using CarStore.Domain.Repositories.User;
using CarStore.Exceptions.ExceptionsBase;
using CarStore.Infrastructure.Services.LoggedUser;

namespace CarStore.Application.UseCases.User.Update
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserUseCase(
        ILoggedUser loggedUser,
        IUserUpdateOnlyRepository repository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userReadOnlyRepository = userReadOnlyRepository;
        }
        public async Task Execute(RequestUpdateUserJson request, Guid userId)
        {
            var loggedUser = await _loggedUser.User();
            Validate(request);
            var user = await _repository.GetById(userId);
            user.Name = request.Name;
            _repository.Update(user);
            await _unitOfWork.Commit();

        }

        private void Validate(RequestUpdateUserJson request)
        {
            var validator = new UpdateUserValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);

            }
        }
    }
}
