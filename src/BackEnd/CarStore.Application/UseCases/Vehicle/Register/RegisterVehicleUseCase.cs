
using AutoMapper;
using CarStore.Communication.Requests;
using CarStore.Communication.Response;
using CarStore.Domain.Cache;
using CarStore.Domain.Repositories;
using CarStore.Domain.Repositories.Brand;
using CarStore.Domain.Repositories.TypeVehicle;
using CarStore.Domain.Repositories.User;
using CarStore.Domain.Repositories.Vehicle;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;
using CarStore.Infrastructure.Services.LoggedUser;

namespace CarStore.Application.UseCases.Vehicle.Register
{
    public class RegisterVehicleUseCase : IRegisterVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ITypeVehicleRepository _typeVehicleRepository;
        private readonly IUserReadOnlyRepository _userRepository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;


        public RegisterVehicleUseCase(
        ILoggedUser LoggedUser,
        IVehicleRepository vehicleRepository,
        IBrandRepository brandRepository,
        ITypeVehicleRepository typeVehicleRepository,
        IUserReadOnlyRepository userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService

        )
        {
            _vehicleRepository = vehicleRepository;
            _brandRepository = brandRepository;
            _typeVehicleRepository = typeVehicleRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggedUser = LoggedUser;
            _cacheService = cacheService;
        }
        public async Task<ResponseVehicleJson> Execute(RequestVehicleJson request)
        {
            Validate(request);
            var loggedUser = await _loggedUser.User();
            var vehicle = _mapper.Map<Domain.Entities.Vehicle>(request);
            vehicle.OwnerId = loggedUser.Id;

            var errors = new List<string>();

            if (await _userRepository.GetById(loggedUser.Id) == null)
                errors.Add(ResourceMessagesException.USER_NOT_FOUND);

            if (!Guid.TryParse(request.BrandId, out var brandId) || await _brandRepository.GetById(brandId) == null)
                errors.Add(ResourceMessagesException.BRAND_NOT_FOUND);

            if (!Guid.TryParse(request.TypeId, out var typeId) || await _typeVehicleRepository.GetById(typeId) == null)
                errors.Add(ResourceMessagesException.TYPE_VEHICLE_NOT_FOUND);

            if (errors.Any())
                throw new ErrorOnValidationException(errors);

            await _vehicleRepository.Add(vehicle);
            await _unitOfWork.Commit();
            await _cacheService.RemoveByPrefixAsync("vehicle:");
            return _mapper.Map<ResponseVehicleJson>(vehicle);
        }

        private static void Validate(RequestVehicleJson request)
        {
            var result = new VehicleValidator().Validate(request);
            if (!result.IsValid)
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
        }
    }
}
