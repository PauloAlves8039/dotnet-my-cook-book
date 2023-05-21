namespace MyCookBook.Exceptions.ExceptionsBase
{
    public class LoginInvalidException : MyCookBookException
    {
        public LoginInvalidException() : base(ResourceErroMessages.INVALID_LOGIN) { }
    }
}
