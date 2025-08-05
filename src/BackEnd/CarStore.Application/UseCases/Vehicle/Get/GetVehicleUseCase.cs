using AutoMapper;
using CarStore.Communication.Response;
using CarStore.Domain.Dtos;
using CarStore.Domain.Repositories.Vehicle;

namespace CarStore.Application.UseCases.Vehicle.GetAll
{
    public class GetVehicleUseCase : IGetVehicleUseCase
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _repository;

        public GetVehicleUseCase(
        IMapper mapper,
        IVehicleRepository repository
        )
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<ResponseVehicleJson>> Execute(VehicleFilterDto filter)
        {
            var vehicles = await _repository.Get(filter);

            var mapped = _mapper.Map<List<ResponseVehicleJson>>(vehicles);

            return mapped;
        }
    }
}
