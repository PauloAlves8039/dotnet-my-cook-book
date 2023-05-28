using MyCookBook.Application.Services.Cryptographies;
using MyCookBook.Application.Services.Token;
using MyCookBook.Communication.Requests;
using MyCookBook.Communication.Responses;
using MyCookBook.Domain.Repositories.User;
using MyCookBook.Exceptions.ExceptionsBase;

namespace MyCookBook.Application.UseCases.Login.DoLogin
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly EncryptPassword _encryptPassword;
        private readonly TokenController _tokenController;

        public LoginUseCase(IUserReadOnlyRepository userReadOnlyRepository, EncryptPassword encryptPassword, TokenController tokenController)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _encryptPassword = encryptPassword;
            _tokenController = tokenController;
        }

        public async Task<ResponseLoginJson> Execute(RequestLoginJson request)
        {
            var encryptPassword = _encryptPassword.Encrypt(request.Password);

            var user = await _userReadOnlyRepository.RecoverPasswordByEmail(request.Email, encryptPassword);

            if (user == null) 
            {
                throw new LoginInvalidException();
            }

            return new ResponseLoginJson
            {
                Name = user.Name,
                Token = _tokenController.GenerateToken(user.Email)
            };
        }
    }
}
