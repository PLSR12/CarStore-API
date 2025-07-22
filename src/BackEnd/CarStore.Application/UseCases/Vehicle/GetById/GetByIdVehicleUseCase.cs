using AutoMapper;
using CarStore.Communication.Response;
using CarStore.Domain.Repositories.Vehicle;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;

namespace CarStore.Application.UseCases.Vehicle
{
    public class GetByIdVehicleUseCase : IGetByIdVehicleUseCase
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _repository;

        public GetByIdVehicleUseCase(
        IMapper mapper,
        IVehicleRepository repository
        )
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseVehicleJson> Execute(Guid id)
        {
            var vehicle = await _repository.GetById(id);

            if (vehicle == null)
            {
                throw new ErrorOnValidationException([ResourceMessagesException.VEHICLE_NOT_FOUND]);
            }

            return _mapper.Map<ResponseVehicleJson>(vehicle);
        }
    }
}
