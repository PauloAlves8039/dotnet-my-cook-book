using MyCookBook.Communication.Requests;

namespace MyCookBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public Task Execute(RequestRegisterUserJson request) 
        {
            
        }

        private void Validate(RequestRegisterUserJson request) 
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);

            if (result.IsValid) 
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage);
                throw new Exception();
            }
        }
    }
}
