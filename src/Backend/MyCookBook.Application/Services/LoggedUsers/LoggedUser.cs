using Microsoft.AspNetCore.Http;
using MyCookBook.Application.Services.Token;
using MyCookBook.Domain.Entities;

namespace MyCookBook.Application.Services.LoggedUsers
{
    public class LoggedUser : ILoggedUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TokenController _tokenController;

        public LoggedUser(IHttpContextAccessor httpContextAccessor, TokenController tokenController)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenController = tokenController;
        }

        public async Task<User> RetrieveUSer()
        {
            var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            var token = authorization["Bearer".Length..].Trim();

            var userEmail =  _tokenController.RetrieveEmail(token);

            throw new NotImplementedException();
        }
    }
}
