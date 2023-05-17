using MyCookBook.Application.Services.Token;

namespace Utils.ForTest.Token
{
    public class TokenControllerBuilder
    {
        public static TokenController Instance()
        {
            return new TokenController(1000, "OUNHI0FjMDUhKm4lZVVtTXE3RE8qYXlPZzcwTXM4T3p0JFAjUUxEYUQjZDlKSCFBdzE=");
        }
    }
}
