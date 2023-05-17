using MyCookBook.Application.Services.Cryptographies;

namespace Utils.ForTest.Cryptographies
{
    public class EncryptPasswordBuilder
    {
        public static EncryptPassword Instance()
        {
            return new EncryptPassword("ABCD123");
        }
    }
}
