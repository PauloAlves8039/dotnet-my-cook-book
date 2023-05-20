using MyCookBook.Domain.Entities;

namespace MyCookBook.Domain.Repositories
{
    public interface IUserReadOnlyRepository
    {
        Task<bool> ExistsUserWithEmail(string email);
        Task<User> RecoverPasswordByEmail(string email, string password);
    }
}
