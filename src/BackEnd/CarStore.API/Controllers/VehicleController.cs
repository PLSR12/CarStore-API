using CarStore.API.Attributes;
using CarStore.Application.UseCases.Vehicle;
using CarStore.Application.UseCases.Vehicle.GetAll;
using CarStore.Application.UseCases.Vehicle.Register;
using CarStore.Communication.Requests;
using CarStore.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.API.Controllers
{
    [AuthenticatedUser]

    public class VehicleController : CarStoreController
    {
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ResponseVehiclesJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> GetAll([FromServices] IGetAllVehicleUseCase useCase)
        {
            var response = await useCase.Execute();

            if (response.Vehicles.Any())
                return Ok(response);
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseVehicleJson), StatusCodes.Status200OK)]
        [AuthenticatedUser]
        public async Task<IActionResult> GetById([FromServices] IGetByIdVehicleUseCase useCase, [FromRoute] Guid id)
        {
            var result = await useCase.Execute(id);
            return Ok(result);
        }


        [HttpPost()]
        [ProducesResponseType(typeof(ResponseVehicleJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Register([FromServices] IRegisterVehicleUseCase useCase, [FromBody] RequestVehicleJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }
    }
}
