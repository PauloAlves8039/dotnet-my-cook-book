﻿using FluentValidation;
using MyCookBook.Communication.Requests;
using MyCookBook.Exceptions;
using System.Text.RegularExpressions;

namespace MyCookBook.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator() 
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage(ResourceErroMessages.EMPTY_NAME);
            RuleFor(c => c.Email).NotEmpty().WithMessage(ResourceErroMessages.EMPTY_EMAIL);
            RuleFor(c => c.Password).NotEmpty().WithMessage(ResourceErroMessages.EMPTY_PHONE);
            RuleFor(c => c.Phone).NotEmpty().WithMessage(ResourceErroMessages.EMPTY_PASSWORD);
            When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
            {
                RuleFor(c => c.Email).EmailAddress().WithMessage(ResourceErroMessages.INVALID_EMAIL);
            });
            When(c => !string.IsNullOrWhiteSpace(c.Password), () =>
            {
                RuleFor(c => c.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceErroMessages.INVALID_PASSWORD);
            });
            When(c => !string.IsNullOrWhiteSpace(c.Phone), () =>
            {
                RuleFor(c => c.Phone).Custom((phone, context) => 
                {
                    string phonePattern = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                    var isMatch = Regex.IsMatch(phone, phonePattern);
                    if (!isMatch)
                    {
                        context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(phone), ResourceErroMessages.INVALID_PHONE));
                    }
                });
            });
        }
    }
}
