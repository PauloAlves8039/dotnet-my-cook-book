using MyCookBook.Infrastructure.RepositoryAccess;
using Utils.ForTest.Entities;

namespace WebApi.Test
{
    public class ContextSeedToMemory
    {
        public static (MyCookBook.Domain.Entities.User user, string password) Seed(MyCookBookContext context) 
        {
            (var user, string password) = UserBuilder.Build();

            context.Users.Add(user);

            context.SaveChanges();

            return (user, password);
        }
    }
}
