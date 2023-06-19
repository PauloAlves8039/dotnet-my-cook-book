using System.Runtime.Serialization;

namespace MyCookBook.Exceptions.ExceptionsBase
{
    [Serializable]
    public class ValidationErrorsException : MyCookBookException
    {
        public List<string> ErrorMessage { get; set; }

        public ValidationErrorsException(List<string> errorMessage) : base(string.Empty)
        {
            ErrorMessage = errorMessage;
        }

        protected ValidationErrorsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
