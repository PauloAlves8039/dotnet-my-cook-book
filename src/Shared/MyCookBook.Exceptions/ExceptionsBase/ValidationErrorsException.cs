﻿namespace MyCookBook.Exceptions.ExceptionsBase
{
    public class ValidationErrorsException : MyCookBookException
    {
        public List<string> errorMessage { get; set; }

        public ValidationErrorsException(List<string> errorMessage)
        {
            this.errorMessage = errorMessage;
        }
    }
}