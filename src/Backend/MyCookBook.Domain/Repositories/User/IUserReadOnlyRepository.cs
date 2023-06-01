namespace MyCookBook.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        Task<bool> ExistsUserWithEmail(string email);
        Task<Entities.User> RecoverPasswordByEmail(string email, string password);
        Task<Entities.User> RecoverByEmail(string email);
    }
}
