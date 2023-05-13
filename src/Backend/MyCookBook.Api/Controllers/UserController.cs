using Microsoft.AspNetCore.Mvc;
using MyCookBook.Application.UseCases.User.Register;
using MyCookBook.Communication.Requests;
using MyCookBook.Communication.Responses;

namespace MyCookBook.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
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
    }
}