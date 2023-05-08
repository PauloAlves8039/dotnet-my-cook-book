using Microsoft.EntityFrameworkCore;
using MyCookBook.Domain.Entities;

namespace MyCookBook.Infrastructure.RepositoryAccess
{
    public class MyCookBookContext : DbContext
    {
        public MyCookBookContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyCookBookContext).Assembly);
        }
    }
}
