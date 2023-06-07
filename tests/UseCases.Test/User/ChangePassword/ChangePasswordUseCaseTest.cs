using FluentAssertions;
using MyCookBook.Application.UseCases.User.ChangePassword;
using MyCookBook.Communication.Requests;
using MyCookBook.Exceptions;
using MyCookBook.Exceptions.ExceptionsBase;
using System.Drawing;
using Utils.ForTest.Cryptographies;
using Utils.ForTest.Entities;
using Utils.ForTest.LoggedUsers;
using Utils.ForTest.Repositories;
using Utils.ForTest.Requests;

namespace UseCases.Test.User.ChangePassword
{
    public class ChangePasswordUseCaseTest
    {
        [Fact]
        public async Task Validate_Success() 
        {
            (var user, var password) = UserBuilder.Build();

            var useCase = CreateUseCase(user);

            var request = RequestChangePasswordUserBuilder.Build();
            request.CurrentPassword = password;

            Func<Task> action = async () =>
            {
                await useCase.Execute(request);
            };

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Validate_Error_NewEmptyPassword()
        {
            (var user, var password) = UserBuilder.Build();

            var useCase = CreateUseCase(user);

            Func<Task> action = async () =>
            {
                await useCase.Execute(new RequestChangePasswordJson
                {
                    CurrentPassword = password,
                    NewPassword = ""
                });
            };

            await action.Should().ThrowAsync<ValidationErrorsException>()
                .Where(ex => ex.ErrorMessage.Count == 1 && ex.ErrorMessage.Contains(ResourceErroMessages.EMPTY_PASSWORD));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task Validate_Error_CurrentPassword_Invalid(int passwordSize)
        {
            (var user, var password) = UserBuilder.Build();

            var useCase = CreateUseCase(user);

            var request = RequestChangePasswordUserBuilder.Build(passwordSize);
            request.CurrentPassword = password;

            Func<Task> action = async () =>
            {
                await useCase.Execute(request);
            };

            await action.Should().ThrowAsync<ValidationErrorsException>()
                .Where(ex => ex.ErrorMessage.Count == 1 && ex.ErrorMessage.Contains(ResourceErroMessages.INVALID_PASSWORD));
        }

        private ChangePasswordUseCase CreateUseCase(MyCookBook.Domain.Entities.User user) 
        {
            var crypter = EncryptPasswordBuilder.Instance();
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();
            var repository = UserUpdateOnlyRepositorBuilder.Instance().RecoverById(user).Build();
            var loggedUser = LoggedUserBuilder.Instance().RecoverUSer(user).Build();

            return new ChangePasswordUseCase(repository, loggedUser, crypter, unitOfWork);
        }
    }
}
