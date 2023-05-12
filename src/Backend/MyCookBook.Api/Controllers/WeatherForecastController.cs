using Microsoft.AspNetCore.Mvc;
using MyCookBook.Application.UseCases.User.Register;
using MyCookBook.Communication.Requests;

namespace MyCookBook.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get([FromServices] IRegisterUserUseCase useCase)
        {
            var resposta = await useCase.Execute(new RequestRegisterUserJson
            {
                Email = "paulo@gmail.com",
                Name = "Paulo",
                Password = "123456",
                Phone = "81 9 9999-8888"
            });

            return Ok(resposta);
        }
    }
}