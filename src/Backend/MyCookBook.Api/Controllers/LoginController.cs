using Microsoft.AspNetCore.Mvc;
using MyCookBook.Application.UseCases.Login.DoLogin;
using MyCookBook.Communication.Requests;
using MyCookBook.Communication.Responses;

namespace MyCookBook.Api.Controllers
{
    public class LoginController : MyCookBookController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromServices] ILoginUseCase useCase, [FromBody] RequestLoginJson request) 
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }
    }
}
