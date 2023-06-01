using MyCookBook.Domain.Entities;

namespace MyCookBook.Application.Services.LoggedUsers
{
    public interface ILoggedUser
    {
        Task<User> RecoverUSer();
    }
}
