using AutoMapper;
using CarStore.Communication.Requests;
using CarStore.Communication.Response;
using CarStore.Domain.Repositories;
using CarStore.Domain.Repositories.Brand;
using CarStore.Domain.Repositories.TypeVehicle;
using CarStore.Domain.Repositories.User;
using CarStore.Domain.Repositories.Vehicle;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;
using CarStore.Infrastructure.Services.LoggedUser;

namespace CarStore.Application.UseCases.Vehicle.Update
{
    public class UpdateVehicleUseCase : IUpdateVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ITypeVehicleRepository _typeVehicleRepository;
        private readonly IUserReadOnlyRepository _userRepository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateVehicleUseCase(
        ILoggedUser LoggedUser,
        IVehicleRepository vehicleRepository,
        IBrandRepository brandRepository,
        ITypeVehicleRepository typeVehicleRepository,
        IUserReadOnlyRepository userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper
        )
        {
            _vehicleRepository = vehicleRepository;
            _brandRepository = brandRepository;
            _typeVehicleRepository = typeVehicleRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggedUser = LoggedUser;
        }
        public async Task<ResponseVehicleJson> Execute(RequestVehicleJson request, Guid vehicleId)
        {
            Validate(request);
            var loggedUser = await _loggedUser.User();
            var vehicleMapped = _mapper.Map<Domain.Entities.Vehicle>(request);
            vehicleMapped.OwnerId = loggedUser.Id;

            var errors = new List<string>();

            if (await _userRepository.GetById(loggedUser.Id) == null)
                errors.Add(ResourceMessagesException.USER_NOT_FOUND);

            if (!Guid.TryParse(request.BrandId, out var brandId) || await _brandRepository.GetById(brandId) == null)
                errors.Add(ResourceMessagesException.BRAND_NOT_FOUND);

            if (!Guid.TryParse(request.TypeId, out var typeId) || await _typeVehicleRepository.GetById(typeId) == null)
                errors.Add(ResourceMessagesException.TYPE_VEHICLE_NOT_FOUND);

            if (errors.Any())
                throw new ErrorOnValidationException(errors);

            var vehicle = await _vehicleRepository.GetById(vehicleId);
            if (vehicle is null)
                throw new NotFoundException(ResourceMessagesException.VEHICLE_NOT_FOUND);

            _vehicleRepository.Update(vehicleMapped);
            await _unitOfWork.Commit();
            return _mapper.Map<ResponseVehicleJson>(vehicleMapped);
        }

        private static void Validate(RequestVehicleJson request)
        {
            var result = new VehicleValidator().Validate(request);
            if (!result.IsValid)
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
        }
    }
}
