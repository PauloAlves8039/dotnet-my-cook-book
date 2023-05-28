namespace MyCookBook.Domain.Repositories.User
{
    public interface IUpdateOnlyRepository
    {
        void Update(Entities.User user);
    }
}
