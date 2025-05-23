using CarStore.Communication.Requests;
using CarStore.Domain.Repositories;
using CarStore.Domain.Repositories.User;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;

namespace CarStore.Application.UseCases.User.Update
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserUseCase(
        IUserUpdateOnlyRepository repository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task Execute(RequestUpdateUserJson request, Guid id)
        {
            Validate(request);
            var user = await _repository.GetById(id);
            if (user == null)
            {
                throw new ErrorOnValidationException([ResourceMessagesException.USER_NOT_FOUND]);
            }
            user.Name = request.Name;
            _repository.Update(user);
            await _unitOfWork.Commit();

        }

        private static void Validate(RequestUpdateUserJson request)
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
