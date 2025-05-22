using CarStore.Application.UseCases.Login.DoLogin;
using CarStore.Communication.Requests;
using CarStore.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.API.Controllers
{
    public class LoginController : CarStoreController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseLoginUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginUserJson request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }
    }
}
