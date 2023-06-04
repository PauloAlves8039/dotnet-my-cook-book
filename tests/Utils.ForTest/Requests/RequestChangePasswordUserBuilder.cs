using Bogus;
using MyCookBook.Communication.Requests;
using System.Drawing;

namespace Utils.ForTest.Requests
{
    public class RequestChangePasswordUserBuilder
    {
        public static RequestChangePasswordJson Build(int passwordLength = 10) 
        {
            return new Faker<RequestChangePasswordJson>()
                .RuleFor(c => c.CurrentPassword, f => f.Internet.Password(10))
                .RuleFor(c => c.NewPassword, f => f.Internet.Password(passwordLength));
        }
    }
}
