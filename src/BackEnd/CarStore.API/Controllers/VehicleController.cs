using CarStore.API.Attributes;
using CarStore.Application.UseCases.Vehicle;
using CarStore.Application.UseCases.Vehicle.Delete;
using CarStore.Application.UseCases.Vehicle.GetAll;
using CarStore.Application.UseCases.Vehicle.Register;
using CarStore.Application.UseCases.Vehicle.Update;
using CarStore.Communication.Requests;
using CarStore.Communication.Response;
using CarStore.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.API.Controllers
{
    [AuthenticatedUser]

    public class VehicleController : CarStoreController
    {
        [HttpGet()]
        [ProducesResponseType(typeof(ResponseVehicleJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> Get([FromServices] IGetVehicleUseCase useCase, [FromQuery] VehicleFilterDto filter)
        {
            var response = await useCase.Execute(filter);

            return Ok(response);
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
        [ProducesResponseType(typeof(ResponseVehicleJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Register([FromServices] IRegisterVehicleUseCase useCase, [FromBody] RequestVehicleJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseVehicleJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromServices] IUpdateVehicleUseCase useCase, [FromBody] RequestVehicleJson request, [FromRoute] Guid id)
        {
            var response = await useCase.Execute(request, id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseVehicleJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromServices] IDeleteVehicleUseCase useCase, [FromRoute] Guid id)
        {
            await useCase.Execute(id);
            return NoContent();
        }
    }
}
