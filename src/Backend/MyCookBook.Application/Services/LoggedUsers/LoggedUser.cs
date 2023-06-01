using Microsoft.AspNetCore.Http;
using MyCookBook.Application.Services.Token;
using MyCookBook.Domain.Entities;
using MyCookBook.Domain.Repositories.User;

namespace MyCookBook.Application.Services.LoggedUsers
{
    public class LoggedUser : ILoggedUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TokenController _tokenController;
        private readonly IUserReadOnlyRepository _repository;

        public LoggedUser(IHttpContextAccessor httpContextAccessor, TokenController tokenController, IUserReadOnlyRepository repository)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenController = tokenController;
            _repository = repository;
        }

        public async Task<User> RecoverUSer()
        {
            var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            var token = authorization["Bearer".Length..].Trim();

            var userEmail =  _tokenController.RecoverEmail(token);

            var user = await _repository.RecoverByEmail(userEmail);

            return user;
        }
    }
}
