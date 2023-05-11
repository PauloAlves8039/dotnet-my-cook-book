using MyCookBook.Communication.Requests;

namespace MyCookBook.Application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        Task Execute(RequestRegisterUserJson request);
    }
}
