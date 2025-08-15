using CarStore.Application.Dtos.Requests;
using CarStore.Exceptions;
using FluentValidation;

namespace CarStore.Application.UseCases.Brand
{
    public class BrandValidator : AbstractValidator<RequestBrandJson>
    {
        public BrandValidator()
        {
            RuleFor(brand => brand.Name).NotEmpty()
                   .WithMessage(ResourceMessagesException.NAME_EMPTY);
            RuleFor(brand => brand.Description)
                .NotEmpty().WithMessage(ResourceMessagesException.DESCRIPTION_EMPTY);
        }
    }
}
