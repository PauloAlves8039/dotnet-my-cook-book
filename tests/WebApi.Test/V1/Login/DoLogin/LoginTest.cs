using FluentAssertions;
using MyCookBook.Communication.Requests;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.V1.Login.DoLogin
{
    public class LoginTest : ControllerBase
    {
        private const string METHOD = "login";
        private MyCookBook.Domain.Entities.User _user;
        private string _password;

        public LoginTest(MyCookBookWebApplicationFactory<Program> factory) : base(factory) 
        {
            _user = factory.RecoverUser();
            _password = factory.RecoverPassword();
        }

        [Fact]
        public async Task Validate_Success()
        {
            var request = new RequestLoginJson 
            {
                Email = _user.Email, 
                Password = _password
            };

            var response = await PostRequest(METHOD, request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var respondeData = await JsonDocument.ParseAsync(responseBody);

            respondeData.RootElement.GetProperty("nome").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_user.Name);
            respondeData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        }
    }
}
