using MyCookBook.Application.UseCases.User.Register;
using Utils.ForTest.Mapper;
using Utils.ForTest.Repositories;

namespace UseCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public void Validate_Success() 
        {
            
        }

        private RegisterUserUseCase CreateUSeCase() 
        {
            var mapper = MapperBuilder.Instance();
            var repository = UserWriteOnlyRepositoryBuilder.Instance();
            var unitOfWork = UnitOfWorkBuilder.Instance();

            return new RegisterUserUseCase(repository, mapper, unitOfWork);
        }
    }
}
