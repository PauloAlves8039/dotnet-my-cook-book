using MyCookBook.Application.Services.LoggedUsers;
using MyCookBook.Communication.Requests;
using MyCookBook.Domain.Repositories.User;

namespace MyCookBook.Application.UseCases.User.ChangePassword
{
    public class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _repository;

        public ChangePasswordUseCase(IUserUpdateOnlyRepository repository, ILoggedUser loggedUser)
        {
            _repository = repository;
            _loggedUser = loggedUser;
        }

        public async Task Execute(RequestChangePasswordJson request)
        {

            var loggedUser = await _loggedUser.RetriveUSer();
            _repository.Update();
        }
    }
}
