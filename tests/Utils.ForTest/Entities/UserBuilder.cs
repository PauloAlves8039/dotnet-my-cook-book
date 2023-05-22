using Bogus;
using MyCookBook.Domain.Entities;
using Utils.ForTest.Cryptographies;

namespace Utils.ForTest.Entities
{
    public class UserBuilder
    {
        public static (User user, string password) Build()
        {
            string password = string.Empty;

            var userGenerated = new Faker<User>()
                .RuleFor(c => c.Id, f => 1)
                .RuleFor(c => c.Name, f => f.Person.FullName)
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Password, f => 
                {
                    password = f.Internet.Password();

                    return EncryptPasswordBuilder.Instance().Encrypt(password);
                })
                .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"));

            return (userGenerated, password);
        }
    }
}
