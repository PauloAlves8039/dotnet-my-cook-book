using System.Runtime.Serialization;

namespace MyCookBook.Exceptions.ExceptionsBase
{
    [Serializable]
    public class LoginInvalidException : MyCookBookException
    {
        public LoginInvalidException() : base(ResourceErroMessages.INVALID_LOGIN) { }

        protected LoginInvalidException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
