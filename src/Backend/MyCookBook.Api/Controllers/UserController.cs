using Microsoft.AspNetCore.Mvc;
using MyCookBook.Api.Filters;
using MyCookBook.Application.UseCases.User.ChangePassword;
using MyCookBook.Application.UseCases.User.Register;
using MyCookBook.Communication.Requests;
using MyCookBook.Communication.Responses;

namespace MyCookBook.Api.Controllers
{
    public class UserController : MyCookBookController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUser(
            [FromServices] IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson request
        )
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }

        [HttpPut]
        [Route("change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ServiceFilter(typeof(AuthenticatedUserAttribute))]
        public async Task<IActionResult> ChangePassword(
            [FromServices] IChangePasswordUseCase useCase,
            [FromBody] RequestChangePasswordJson request
        )
        {
            await useCase.Execute(request);
            return NoContent();
        }
    }
}