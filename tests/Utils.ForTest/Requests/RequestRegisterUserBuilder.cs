using Bogus;
using MyCookBook.Communication.Requests;

namespace Utils.ForTest.Requests
{
    public class RequestRegisterUserBuilder
    {
        public static RequestRegisterUserJson Build(int passwordLength = 10) 
        {
            return new Faker<RequestRegisterUserJson>()
                .RuleFor(c => c.Name, f => f.Person.FullName)
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Password, f => f.Internet.Password(passwordLength))
                .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"));
        }
    }
}
