using CarStore.API.Attributes;
using CarStore.Application.UseCases.User.ChangePassword;
using CarStore.Application.UseCases.User.Profile;
using CarStore.Application.UseCases.User.Register;
using CarStore.Application.UseCases.User.Update;
using CarStore.Communication.Requests;
using CarStore.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.API.Controllers
{
    public class UserController : CarStoreController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson request
        )
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
        [AuthenticatedUser]
        public async Task<IActionResult> GetUserProfile([FromServices] IGetUserProfileUseCase useCase, [FromRoute] Guid id)
        {
            var result = await useCase.Execute(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [AuthenticatedUser]
        public async Task<IActionResult> Update([FromServices] IUpdateUserUseCase useCase,
             [FromRoute] Guid id,
            [FromBody] RequestUpdateUserJson request)
        {
            await useCase.Execute(request, id);
            return NoContent();
        }

        [HttpPut("change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [AuthenticatedUser]
        public async Task<IActionResult> ChangePassword([FromServices] IChangePasswordUseCase useCase, [FromBody] RequestChangePasswordJson request)
        {
            await useCase.Execute(request);
            return NoContent();
        }

    }
}
