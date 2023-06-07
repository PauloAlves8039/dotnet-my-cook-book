using Moq;
using MyCookBook.Domain.Repositories.User;

namespace Utils.ForTest.Repositories
{
    public class UserUpdateOnlyRepositorBuilder
    {
        private static UserUpdateOnlyRepositorBuilder _instance;
        private readonly Mock<IUserUpdateOnlyRepository> _repository;

        private UserUpdateOnlyRepositorBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IUserUpdateOnlyRepository>();
            }
        }

        public static UserUpdateOnlyRepositorBuilder Instance()
        {
            _instance = new UserUpdateOnlyRepositorBuilder();
            return _instance;
        }
        
        public UserUpdateOnlyRepositorBuilder RecoverById(MyCookBook.Domain.Entities.User user) 
        {
            _repository.Setup(c => c.RecoverById(user.Id)).ReturnsAsync(user);
            return this;
        }

        public IUserUpdateOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
