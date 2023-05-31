using MyCookBook.Communication.Requests;

namespace MyCookBook.Application.UseCases.User.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        Task Execute(RequestChangePasswordJson request);
    }
}
