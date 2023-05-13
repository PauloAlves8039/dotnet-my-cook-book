﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCookBook.Application.Services.Cryptographies;
using MyCookBook.Application.Services.Token;
using MyCookBook.Application.UseCases.User.Register;

namespace MyCookBook.Application
{
    public static class Bootstrapper
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {

            AddPasswordKeySetting(services, configuration);

            AddJwtToken(services, configuration);

            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        }

        private static void AddPasswordKeySetting(IServiceCollection services, IConfiguration configuration) 
        {
            var section = configuration.GetRequiredSection("Configurations:AdditionalKeyPassword");

            services.AddScoped(option => new EncryptPassword(section.Value));
        }

        private static void AddJwtToken(IServiceCollection services, IConfiguration configuration)
        {
            var lifetimeSection = configuration.GetRequiredSection("Configurations:LifetimeToken");
            var keySection = configuration.GetRequiredSection("Configurations:KeyToken");

            services.AddScoped(option => new TokenController(int.Parse(lifetimeSection.Value), keySection.Value));
        }
    }
}