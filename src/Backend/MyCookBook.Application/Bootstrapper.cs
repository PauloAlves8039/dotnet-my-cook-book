using Microsoft.Extensions.DependencyInjection;
using MyCookBook.Application.UseCases.User.Register;

namespace MyCookBook.Application
{
    public static class Bootstrapper
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        }
    }
}
