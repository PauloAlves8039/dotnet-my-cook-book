﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using MyCookBook.Application.Services.Token;
using MyCookBook.Communication.Responses;
using MyCookBook.Domain.Repositories.User;
using MyCookBook.Exceptions;
using MyCookBook.Exceptions.ExceptionsBase;

namespace MyCookBook.Api.Filters
{
    public class AuthenticatedUserAttribute : AuthorizeAttribute,IAsyncAuthorizationFilter
    {
        private readonly TokenController _tokenController;
        private readonly IUserReadOnlyRepository _repository;

        public  AuthenticatedUserAttribute(TokenController tokenController, IUserReadOnlyRepository repository)
        {
            _tokenController = tokenController;
            _repository = repository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenInRequest(context);
                var userEmail = _tokenController.RecoverEmail(token);

                var user = await _repository.RecoverByEmail(userEmail);

                if (user is null)
                {
                    throw new MyCookBookException(string.Empty);
                }
            }
            catch (SecurityTokenExpiredException)
            {
                ExpiredToken(context);
            }
            catch 
            {
                UserWithoutPermission(context);
            }
        }

        private static string TokenInRequest(AuthorizationFilterContext context) 
        {
            var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authorization)) 
            {
                throw new MyCookBookException(string.Empty);
            }

            return authorization["Bearer".Length..].Trim();
        }

        private static void ExpiredToken(AuthorizationFilterContext context) 
        {
            context.Result = new UnauthorizedObjectResult(new ErrorResponseJson(ResourceErroMessages.EXPIRED_TOKEN));
        }
        
        private static void UserWithoutPermission(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult(new ErrorResponseJson(ResourceErroMessages.USER_WITHOUT_PERMISSION));
        }
    }
}
