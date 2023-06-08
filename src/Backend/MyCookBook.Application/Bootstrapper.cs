using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCookBook.Application.Services.Cryptographies;
using MyCookBook.Application.Services.LoggedUsers;
using MyCookBook.Application.Services.Token;
using MyCookBook.Application.UseCases.Login.DoLogin;
using MyCookBook.Application.UseCases.User.ChangePassword;
using MyCookBook.Application.UseCases.User.Register;

namespace MyCookBook.Application
{
    public static class Bootstrapper
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddPasswordKeySetting(services, configuration);
            AddJwtToken(services, configuration);
            AddUseCase(services);
            AddLoggedUser(services);
        }

        private static void AddPasswordKeySetting(IServiceCollection services, IConfiguration configuration) 
        {
            var section = configuration.GetRequiredSection("Configurations:password:AdditionalKeyPassword");

            services.AddScoped(option => new EncryptPassword(section.Value));
        }

        private static void AddJwtToken(IServiceCollection services, IConfiguration configuration)
        {
            var lifetimeSection = configuration.GetRequiredSection("Configurations:Jwt:TokenLifetimeMinutes");
            var keySection = configuration.GetRequiredSection("Configurations:Jwt:KeyToken");

            services.AddScoped(option => new TokenController(int.Parse(lifetimeSection.Value), keySection.Value));
        }

        private static void AddUseCase(IServiceCollection services) 
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()
                .AddScoped<ILoginUseCase, LoginUseCase>()
                .AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        }

        private static void AddLoggedUser(IServiceCollection services)
        {
            services.AddScoped<ILoggedUser, LoggedUser>();
        }
    }
}
