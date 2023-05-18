using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCookBook.Infrastructure.RepositoryAccess;

namespace WebApi.Test
{
    public class MyCookBookWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder) 
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services => 
                {
                    var descritor = services.SingleOrDefault(d => d.ServiceType == typeof(MyCookBookContext));

                    if (descritor != null) 
                    {
                        services.Remove(descritor);
                    }

                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    services.AddDbContext<MyCookBookContext>(options => 
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(provider);
                    });

                    var serviceProvider = services.BuildServiceProvider();

                    using var scope = serviceProvider.CreateScope();
                    var scopeService = scope.ServiceProvider;

                    var database = scopeService.GetRequiredService<MyCookBookContext>();
                    database.Database.EnsureCreated();
                });
        }
    }
}
