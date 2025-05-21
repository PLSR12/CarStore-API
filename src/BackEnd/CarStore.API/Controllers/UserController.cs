using CarStore.Application.UseCases.User.Register;
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

    }
}
