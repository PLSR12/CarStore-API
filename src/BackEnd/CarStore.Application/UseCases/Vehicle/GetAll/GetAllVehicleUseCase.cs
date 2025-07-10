using AutoMapper;
using CarStore.Communication.Response;
using CarStore.Domain.Repositories.Vehicle;
using CarStore.Infrastructure.Services.LoggedUser;

namespace CarStore.Application.UseCases.Vehicle.GetAll
{
    public class GetAllVehicleUseCase : IGetAllVehicleUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IVehicleRepository _repository;

        public GetAllVehicleUseCase(
        IMapper mapper,
        ILoggedUser LoggedUser,
        IVehicleRepository repository
        )
        {
            _mapper = mapper;
            _loggedUser = LoggedUser;
            _repository = repository;
        }

        public async Task<ResponseVehiclesJson> Execute()
        {
            var vehicles = await _repository.GetAll();

            var mapped = _mapper.Map<List<ResponseVehicleJson>>(vehicles);

            return new ResponseVehiclesJson
            {
                Vehicles = mapped
            };
        }
    }
}
