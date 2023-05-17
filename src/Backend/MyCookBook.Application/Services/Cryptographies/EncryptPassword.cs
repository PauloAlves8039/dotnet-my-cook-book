using System.Security.Cryptography;
using System.Text;

namespace MyCookBook.Application.Services.Cryptographies
{
    public class EncryptPassword
    {
        private readonly string _additionalKey;

        public EncryptPassword(string encryptionKey)
        {
            _additionalKey = encryptionKey;
        }

        public string Encrypt(string password) 
        {
            var passwordWithAdditionalKey = $"{password}{_additionalKey}";

            var bytes = Encoding.UTF8.GetBytes(passwordWithAdditionalKey);
            var sha512 = SHA512.Create();
            byte[] hashBytes = sha512.ComputeHash(bytes);
            return StringBytes(hashBytes);
        }

        private static string StringBytes(byte[] bytes) 
        {
            var stringBuilder = new StringBuilder();
            foreach (byte item in bytes)
            {
                var hex = item.ToString("x2");
                stringBuilder.Append(hex);
            }
            return stringBuilder.ToString();
        }
    }
}
