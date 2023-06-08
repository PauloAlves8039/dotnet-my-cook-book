using FluentAssertions;
using MyCookBook.Communication.Requests;
using MyCookBook.Exceptions;
using System.Net;
using System.Text.Json;
using Utils.ForTest.Requests;

namespace WebApi.Test.V1.ChangePassword
{
    public class ChangePasswordTest : ControllerBase
    {
        private const string METHOD = "user/change-password";
        private MyCookBook.Domain.Entities.User _user;
        private string _password;

        public ChangePasswordTest(MyCookBookWebApplicationFactory<Program> factory) : base(factory)
        {
            _user = factory.RecoverUser();
            _password = factory.RecoverPassword();
        }

        [Fact]
        public async Task Validate_Success()
        {
            var token = await Login(_user.Email, _password);
            
            var request = RequestChangePasswordUserBuilder.Build();
            
            request.CurrentPassword = _password;

            var response = await PutRequest(METHOD, request, token);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Validate_Error_Empty_Password() 
        {
            var token = await Login(_user.Email, _password);

            var request = RequestChangePasswordUserBuilder.Build();

            request.CurrentPassword = _password;
            request.NewPassword = string.Empty;

            var response = await PutRequest(METHOD, request, token);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var respondeData = await JsonDocument.ParseAsync(responseBody);

            var errors = respondeData.RootElement.GetProperty("messages").EnumerateArray();
            errors.Should().ContainSingle().And.Contain(x => x.GetString().Equals(ResourceErroMessages.EMPTY_PASSWORD));
        }
    }
}
