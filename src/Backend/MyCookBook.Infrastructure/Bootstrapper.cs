using FluentMigrator.Runner;
using MyCookBook.Domain.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MyCookBook.Domain.Repositories;
using MyCookBook.Infrastructure.RepositoryAccess.Repository;
using MyCookBook.Infrastructure.RepositoryAccess;
using Microsoft.EntityFrameworkCore;

namespace MyCookBook.Infrastructure
{
    public static class Bootstrapper
    {
        public static void AddRepository(this IServiceCollection services, IConfiguration configurationManager) 
        {
            AddFluentMigrator(services, configurationManager);

            AddContext(services, configurationManager);
            AddUnitOfWork(services);
            AddRepositories(services);
        }

        private static void AddContext(IServiceCollection services, IConfiguration configurationManager) 
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));
            var connextionString = configurationManager.GetFullConnection();

            services.AddDbContext<MyCookBookContext>(dbContextOptions => 
            {
                dbContextOptions.UseMySql(connextionString, serverVersion);
            });
        }

        private static void AddUnitOfWork(IServiceCollection services) 
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddRepositories(IServiceCollection services) 
        {
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>()
                .AddScoped<IUserReadOnlyRepository, UserRepository>();
        }

        private static void AddFluentMigrator(IServiceCollection services, IConfiguration configurationManager) 
        {
            services.AddFluentMigratorCore().ConfigureRunner(c => 
                c.AddMySql5()
                .WithGlobalConnectionString(configurationManager.GetFullConnection()).ScanIn(Assembly.Load("MyCookBook.Infrastructure")).For.All());
        }
    }
}
