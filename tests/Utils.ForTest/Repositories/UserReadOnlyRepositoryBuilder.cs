using Moq;
using MyCookBook.Domain.Entities;
using MyCookBook.Domain.Repositories;

namespace Utils.ForTest.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private static UserReadOnlyRepositoryBuilder _instance;
        private readonly Mock<IUserReadOnlyRepository> _repository;

        private UserReadOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IUserReadOnlyRepository>();
            }
        }

        public static UserReadOnlyRepositoryBuilder Instance()
        {
            _instance = new UserReadOnlyRepositoryBuilder();
            return _instance;
        }

        public UserReadOnlyRepositoryBuilder ExistsUserWithEmail(string email) 
        {
            if (!string.IsNullOrEmpty(email)) 
            {
                _repository.Setup(i => i.ExistsUserWithEmail(email)).ReturnsAsync(true);
            }
            return this;
        }

        public UserReadOnlyRepositoryBuilder RecoverPasswordByEmail(User user) 
        {
            _repository.Setup(i => i.RecoverPasswordByEmail(user.Email, user.Password)).ReturnsAsync(user);

            return this;   
        }

        public IUserReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
