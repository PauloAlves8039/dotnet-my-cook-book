namespace MyCookBook.Exceptions.ExceptionsBase
{
    public class ValidationErrorsException : MyCookBookException
    {
        public List<string> ErrorMessage { get; set; }

        public ValidationErrorsException(List<string> errorMessage) : base(string.Empty)
        {
            ErrorMessage = errorMessage;
        }
    }
}
