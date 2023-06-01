namespace MyCookBook.Domain.Repositories.User
{
    public interface IUserUpdateOnlyRepository
    {
        void Update(Entities.User user);
        Task<Entities.User> RecoverById(long id);
    }
}
