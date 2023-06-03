using FluentValidation;
using MyCookBook.Exceptions;

namespace MyCookBook.Application.UseCases.User
{
    public class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator()
        {
            RuleFor(password => password).NotEmpty().WithMessage(ResourceErroMessages.EMPTY_PASSWORD);

            When(password => !string.IsNullOrWhiteSpace(password), () =>
            {
                RuleFor(password => password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceErroMessages.INVALID_PASSWORD);
            });
        }
    }
}
