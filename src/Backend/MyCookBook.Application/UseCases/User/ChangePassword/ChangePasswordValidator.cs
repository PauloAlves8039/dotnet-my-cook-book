using FluentValidation;
using MyCookBook.Communication.Requests;
using MyCookBook.Exceptions;
using System.Text.RegularExpressions;

namespace MyCookBook.Application.UseCases.User.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
    {
        public ChangePasswordValidator()
        {
            RuleFor(c => c.NewPassword).SetValidator(new PasswordValidator());
        }
    }
}
