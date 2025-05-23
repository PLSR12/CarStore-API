using CarStore.Communication.Requests;
using CarStore.Exceptions;
using FluentValidation;

namespace CarStore.Application.UseCases.User.Update
{

    public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
    {
        public UpdateUserValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        }
    }
}
