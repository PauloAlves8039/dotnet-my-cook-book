using FluentAssertions;
using MyCookBook.Application.UseCases.User.ChangePassword;
using MyCookBook.Exceptions;
using Utils.ForTest.Requests;

namespace Validators.Test.User.ChangePassword
{
    public class ChangePasswordValidatorTest
    {
        [Fact]
        public void Validate_Success()
        {
            var validador = new ChangePasswordValidator();

            var request = RequestChangePasswordUserBuilder.Build();

            var result = validador.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Validate_Error_Invalid_Password(int passwordLength)
        {
            var validador = new ChangePasswordValidator();

            var request = RequestChangePasswordUserBuilder.Build(passwordLength);

            var result = validador.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErroMessages.INVALID_PASSWORD));
        }

        [Fact]
        public void Validate_Error_Empty_Password()
        {
            var validador = new ChangePasswordValidator();

            var request = RequestChangePasswordUserBuilder.Build();
            request.NewPassword = string.Empty;

            var result = validador.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErroMessages.EMPTY_PASSWORD));
        }
    }
}
