using FluentAssertions;
using MyCookBook.Application.UseCases.User.Register;
using MyCookBook.Exceptions;
using Utils.ForTest.Requests;

namespace Validators.Test.User.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Validate_Success()
        {
            var validador = new RegisterUserValidator();

            var request = RequestRegisterUserBuilder.Build();
            
            var result = validador.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_Error_Empty_Name() 
        {
            var validador = new RegisterUserValidator();

            var request = RequestRegisterUserBuilder.Build();
            request.Name = string.Empty;

            var result = validador.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErroMessages.EMPTY_NAME));
        }

        [Fact]
        public void Validate_Error_Empty_Email()
        {
            var validador = new RegisterUserValidator();

            var request = RequestRegisterUserBuilder.Build();
            request.Email = string.Empty;

            var result = validador.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErroMessages.EMPTY_EMAIL));
        }

        [Fact]
        public void Validate_Error_Empty_Password()
        {
            var validador = new RegisterUserValidator();

            var request = RequestRegisterUserBuilder.Build();
            request.Password = string.Empty;

            var result = validador.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErroMessages.EMPTY_PASSWORD));
        }

        [Fact]
        public void Validate_Error_Empty_Phone()
        {
            var validador = new RegisterUserValidator();

            var request = RequestRegisterUserBuilder.Build();
            request.Phone = string.Empty;

            var result = validador.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErroMessages.EMPTY_PHONE));
        }

        [Fact]
        public void Validate_Error_Invalid_Email()
        {
            var validador = new RegisterUserValidator();

            var request = RequestRegisterUserBuilder.Build();
            request.Email = "pj";

            var result = validador.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErroMessages.INVALID_EMAIL));
        }

        [Fact]
        public void Validate_Error_Invalid_Phone()
        {
            var validador = new RegisterUserValidator();

            var request = RequestRegisterUserBuilder.Build();
            request.Phone = "81 9";

            var result = validador.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErroMessages.INVALID_PHONE));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Validate_Error_Invalid_Password(int passwordLength)
        {
            var validador = new RegisterUserValidator();

            var request = RequestRegisterUserBuilder.Build(passwordLength);

            var result = validador.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErroMessages.INVALID_PASSWORD));
        }
    }
}
