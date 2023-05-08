using Microsoft.EntityFrameworkCore;
using MyCookBook.Domain.Entities;
using MyCookBook.Domain.Repositories;

namespace MyCookBook.Infrastructure.RepositoryAccess.Repository
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
    {
        private readonly MyCookBookContext _context;

        public UserRepository(MyCookBookContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> ExistsUserWithEmail(string email)
        {
            return await _context.Users.AnyAsync(c => c.Email.Equals(email));
        }
    }
}
