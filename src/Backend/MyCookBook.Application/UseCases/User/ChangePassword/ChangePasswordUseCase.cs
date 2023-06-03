using MyCookBook.Application.Services.Cryptographies;
using MyCookBook.Application.Services.LoggedUsers;
using MyCookBook.Communication.Requests;
using MyCookBook.Domain.Repositories.User;
using MyCookBook.Exceptions;
using MyCookBook.Exceptions.ExceptionsBase;

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

            Validate(request, user);

            user.Password = _encryptPassword.Encrypt(request.NewPassword);

            _repository.Update(user);
        }

        private void Validate(RequestChangePasswordJson request, Domain.Entities.User user) 
        {
            var validator = new ChangePasswordValidator();
            var result = validator.Validate(request);
            
            var encryptedCurrentPassword = _encryptPassword.Encrypt(request.CurrentPassword);

            if (!user.Password.Equals(encryptedCurrentPassword)) 
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("currentPassword", ResourceErroMessages.INVALID_CURRENT_PASSWORD));
            }
            
            if (!result.IsValid) 
            {
                var messages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ValidationErrorsException(messages);
            }
        }
    }
}
