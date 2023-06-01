using MyCookBook.Application.Services.Cryptographies;
using MyCookBook.Application.Services.LoggedUsers;
using MyCookBook.Communication.Requests;
using MyCookBook.Domain.Repositories.User;

namespace MyCookBook.Application.UseCases.User.ChangePassword
{
    public class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly EncryptPassword _encryptPassword;

        public ChangePasswordUseCase(IUserUpdateOnlyRepository repository, ILoggedUser loggedUser, EncryptPassword encryptPassword)
        {
            _repository = repository;
            _loggedUser = loggedUser;
            _encryptPassword = encryptPassword;
        }

        public async Task Execute(RequestChangePasswordJson request)
        {
            var loggedUser = await _loggedUser.RecoverUSer();

            var user = await _repository.RecoverById(loggedUser.Id);

            Validate();

            user.Password = _encryptPassword.Encrypt(request.NewPassword);

            _repository.Update(user);
        }

        private void Validate() 
        {
            
        }
    }
}
