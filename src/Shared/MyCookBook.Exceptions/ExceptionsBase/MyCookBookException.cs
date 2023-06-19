using System.Runtime.Serialization;

namespace MyCookBook.Exceptions.ExceptionsBase
{
    [Serializable]
    public class MyCookBookException : SystemException
    {
        public MyCookBookException(string message) : base(message) { }

        protected MyCookBookException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
