using FluentAssertions;
using MyCookBook.Application.UseCases.User.Register;
using MyCookBook.Exceptions;
using MyCookBook.Exceptions.ExceptionsBase;
using Utils.ForTest.Cryptographies;
using Utils.ForTest.Mapper;
using Utils.ForTest.Repositories;
using Utils.ForTest.Requests;
using Utils.ForTest.Token;

namespace UseCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Validate_Success() 
        {
            var request = RequestRegisterUserBuilder.Build();

            var useCase = CreateUSeCase();

            var response = await useCase.Execute(request);

            response.Should().NotBeNull();
            response.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Validate_Error_Registered_Email()
        {
            var request = RequestRegisterUserBuilder.Build();

            var useCase = CreateUSeCase(request.Email);

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().ThrowAsync<ValidationErrorsException>()
                .Where(exception => exception.errorMessage.Count == 1 && exception.errorMessage.Contains(ResourceErroMessages.REGISTERED_EMAIL));
        }

        [Fact]
        public async Task Validate_Error_Empty_Email()
        {
            var request = RequestRegisterUserBuilder.Build();
            request.Email = string.Empty;

            var useCase = CreateUSeCase();

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().ThrowAsync<ValidationErrorsException>()
                .Where(exception => exception.errorMessage.Count == 1 && exception.errorMessage.Contains(ResourceErroMessages.EMPTY_EMAIL));
        }

        private RegisterUserUseCase CreateUSeCase(string email = "") 
        {
            var mapper = MapperBuilder.Instance();
            var repository = UserWriteOnlyRepositoryBuilder.Instance().Build();
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();
            var cryptography = EncryptPasswordBuilder.Instance();
            var token = TokenControllerBuilder.Instance();
            var repositoryReadOnly = UserReadOnlyRepositoryBuilder.Instance().ExistsUserWithEmail(email).Build();

            return new RegisterUserUseCase(repository, mapper, unitOfWork, cryptography, token, repositoryReadOnly);
        }
    }
}
