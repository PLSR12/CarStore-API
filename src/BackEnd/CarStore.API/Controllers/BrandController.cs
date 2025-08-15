using CarStore.API.Attributes;
using CarStore.Application.Dtos.Requests;
using CarStore.Application.UseCases.Brand;
using CarStore.Application.UseCases.Brand.Delete;
using CarStore.Application.UseCases.Brand.GetAll;
using CarStore.Application.UseCases.Brand.Register;
using CarStore.Application.UseCases.Brand.Update;
using CarStore.Communication.Response;
using CarStore.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.API.Controllers
{
    [AuthenticatedUser]

    public class BrandController : CarStoreController
    {
        [HttpGet()]
        [ProducesResponseType(typeof(ResponseBrandJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> Get([FromServices] IGetBrandUseCase useCase, [FromQuery] BrandFilterDto filter)
        {
            var response = await useCase.Execute(filter);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseBrandJson), StatusCodes.Status200OK)]
        [AuthenticatedUser]
        public async Task<IActionResult> GetById([FromServices] IGetByIdBrandUseCase useCase, [FromRoute] Guid id)
        {
            var result = await useCase.Execute(id);
            return Ok(result);
        }


        [HttpPost()]
        [ProducesResponseType(typeof(ResponseBrandJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Register([FromServices] IRegisterBrandUseCase useCase, [FromBody] RequestBrandJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseBrandJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromServices] IUpdateBrandUseCase useCase, [FromBody] RequestBrandJson request, [FromRoute] Guid id)
        {
            var response = await useCase.Execute(request, id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseBrandJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromServices] IDeleteBrandUseCase useCase, [FromRoute] Guid id)
        {
            await useCase.Execute(id);
            return NoContent();
        }
    }
}
