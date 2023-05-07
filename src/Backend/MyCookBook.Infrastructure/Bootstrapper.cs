using FluentMigrator.Runner;
using MyCookBook.Domain.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MyCookBook.Infrastructure
{
    public static class Bootstrapper
    {
        public static void AddRepository(this IServiceCollection services, IConfiguration configurationManager) 
        {
            AddFluentMigrator(services, configurationManager);
        }

        private static void AddFluentMigrator(IServiceCollection services, IConfiguration configurationManager) 
        {
            services.AddFluentMigratorCore().ConfigureRunner(c => 
                c.AddMySql5()
                .WithGlobalConnectionString(configurationManager.GetFullConnection()).ScanIn(Assembly.Load("MyCookBook.Infrastructure")).For.All());
        }
    }
}
