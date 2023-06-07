using Moq;
using MyCookBook.Application.Services.LoggedUsers;

namespace Utils.ForTest.LoggedUsers
{
    public class LoggedUserBuilder
    {
        private static LoggedUserBuilder _instance;
        private readonly Mock<ILoggedUser> _repository;

        private LoggedUserBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<ILoggedUser>();
            }
        }

        public static LoggedUserBuilder Instance()
        {
            _instance = new LoggedUserBuilder();
            return _instance;
        }

        public LoggedUserBuilder RecoverUSer(MyCookBook.Domain.Entities.User user)
        {
            _repository.Setup(c => c.RecoverUSer()).ReturnsAsync(user);
            return this;
        }

        public ILoggedUser Build()
        {
            return _repository.Object;
        }
    }
}
