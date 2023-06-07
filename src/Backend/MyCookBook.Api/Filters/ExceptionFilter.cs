using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyCookBook.Communication.Responses;
using MyCookBook.Exceptions;
using MyCookBook.Exceptions.ExceptionsBase;
using System.Net;

namespace MyCookBook.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is MyCookBookException)
            {
                TreatMyCookBookException(context);
            }
        }

        private void TreatMyCookBookException(ExceptionContext context) 
        {
            if (context.Exception is ValidationErrorsException)
            {
                TreatValidationErrorException(context);
            }
            else if (context.Exception is LoginInvalidException) 
            {
                TreatLoginException(context);
            }
        }

        private void TreatValidationErrorException(ExceptionContext context) 
        {
            var errorValidationException = context.Exception as ValidationErrorsException;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult(new ErrorResponseJson(errorValidationException.ErrorMessage));
        }

        private void TreatLoginException(ExceptionContext context)
        {
            var errorLogin = context.Exception as LoginInvalidException;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Result = new ObjectResult(new ErrorResponseJson(errorLogin.Message));
        }

        private void ThrowUnknownError(ExceptionContext context) 
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ErrorResponseJson(ResourceErroMessages.UNKNOWN_ERROR));
        }
    }
}
