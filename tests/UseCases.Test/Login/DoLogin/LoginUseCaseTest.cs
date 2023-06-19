using FluentAssertions;
using MyCookBook.Application.UseCases.Login.DoLogin;
using MyCookBook.Communication.Requests;
using MyCookBook.Exceptions;
using MyCookBook.Exceptions.ExceptionsBase;
using Utils.ForTest.Cryptographies;
using Utils.ForTest.Entities;
using Utils.ForTest.Repositories;
using Utils.ForTest.Token;

namespace UseCases.Test.Login.DoLogin
{
    public class LoginUseCaseTest
    {
        [Fact]
        public async Task Validate_Success() 
        {
            (var user, var password) = UserBuilder.Build();

            var useCase = CreateUseCase(user);

            var response = await useCase.Execute(new RequestLoginJson 
            {
                Email = user.Email,
                Password = password
            });

            response.Should().NotBeNull();
            response.Name.Should().Be(user.Name);
            response.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Validate_Error_Invalid_Password() 
        {
            (var user, var password) = UserBuilder.Build();

            var useCase = CreateUseCase(user);

            Func<Task> action = async () => 
            {
                await useCase.Execute(new RequestLoginJson 
                {
                    Email = user.Email,
                    Password = "invalidPassword"
                });
            };

            await action.Should().ThrowAsync<LoginInvalidException>()
                .Where(exception => exception.Message.Equals(ResourceErroMessages.INVALID_LOGIN));
        }

        [Fact]
        public async Task Validate_Error_Invalid_Email() 
        {
            (var user, var password) = UserBuilder.Build();

            var useCase = CreateUseCase(user);

            Func<Task> action = async () =>
            {
                await useCase.Execute(new RequestLoginJson
                {
                    Email = "email@invalid.com",
                    Password = password
                });
            };

            await action.Should().ThrowAsync<LoginInvalidException>()
                .Where(exception => exception.Message.Equals(ResourceErroMessages.INVALID_LOGIN));
        }

        [Fact]
        public async Task Validate_Error_Invalid_Email_Password() 
        {
            (var user, var password) = UserBuilder.Build();

            var useCase = CreateUseCase(user);

            Func<Task> action = async () =>
            {
                await useCase.Execute(new RequestLoginJson
                {
                    Email = "email@invalid.com",
                    Password = "invalidPassword"
                });
            };

            await action.Should().ThrowAsync<LoginInvalidException>()
                .Where(exception => exception.Message.Equals(ResourceErroMessages.INVALID_LOGIN));
        } 

        private static LoginUseCase CreateUseCase(MyCookBook.Domain.Entities.User user) 
        {
            var cryptography = EncryptPasswordBuilder.Instance();
            var token = TokenControllerBuilder.Instance();
            var repositoryReadOnly = UserReadOnlyRepositoryBuilder.Instance().RecoverPasswordByEmail(user).Build();

            return new LoginUseCase(repositoryReadOnly, cryptography, token);
        }
    }
}
