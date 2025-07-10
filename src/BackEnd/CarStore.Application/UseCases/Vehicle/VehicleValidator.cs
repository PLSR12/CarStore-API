using CarStore.Communication.Requests;
using CarStore.Exceptions;
using FluentValidation;

namespace CarStore.Application.UseCases.Vehicle
{
    public class VehicleValidator : AbstractValidator<RequestVehicleJson>
    {
        public VehicleValidator()
        {
            RuleFor(vehicle => vehicle.Model).NotEmpty()
                   .WithMessage(ResourceMessagesException.VEHICLE_MODEL_EMPTY);
            RuleFor(vehicle => vehicle.LicensePlate)
                .NotEmpty().WithMessage(ResourceMessagesException.VEHICLE_LICENSE_PLATE_EMPTY)
                .Length(7).WithMessage(ResourceMessagesException.VEHICLE_LICENSE_PLATE_INVALID);
            RuleFor(vehicle => vehicle.YearFabrication).NotEmpty()
                .WithMessage(ResourceMessagesException.VEHICLE_YEAR_FABRICATION_EMPTY);
            RuleFor(vehicle => vehicle.Mileage).NotEmpty()
                .WithMessage(ResourceMessagesException.VEHICLE_MILEAGE_EMPTY);
            RuleFor(vehicle => vehicle.Color).NotEmpty()
                .WithMessage(ResourceMessagesException.VEHICLE_COLOR_EMPTY);
            RuleFor(vehicle => vehicle.EnginePower).NotEmpty()
                .WithMessage(ResourceMessagesException.VEHICLE_ENGINE_POWER_EMPTY);
            RuleFor(vehicle => vehicle.BrandId).NotEmpty()
                .WithMessage(ResourceMessagesException.VEHICLE_BRAND_ID_EMPTY);
            RuleFor(vehicle => vehicle.TypeId).NotEmpty()
                .WithMessage(ResourceMessagesException.VEHICLE_TYPE_ID_EMPTY);
        }
    }
}
