using FluentAssertions;
using MyCookBook.Exceptions;
using System.Net;
using System.Text.Json;
using Utils.ForTest.Requests;
using WebApi.Test.V1;

namespace WebApi.Test.User.Register
{
    public class RegisterUserTest : ControllerBase
    {
        private const string METHOD = "user";

        public RegisterUserTest(MyCookBookWebApplicationFactory<Program> factory) : base(factory) { }

        [Fact]
        public async Task Validate_Success() 
        {
            var request = RequestRegisterUserBuilder.Build();

            var response = await PostRequest(METHOD, request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var respondeData = await JsonDocument.ParseAsync(responseBody);

            respondeData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Validate_Error_Empty_Name()
        {
            var request = RequestRegisterUserBuilder.Build();
            request.Name = "";

            var response = await PostRequest(METHOD, request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var respondeData = await JsonDocument.ParseAsync(responseBody);

            var errors = respondeData.RootElement.GetProperty("messages").EnumerateArray();
            errors.Should().ContainSingle().And.Contain(c => c.GetString().Equals(ResourceErroMessages.EMPTY_NAME));
        }
    }
}
