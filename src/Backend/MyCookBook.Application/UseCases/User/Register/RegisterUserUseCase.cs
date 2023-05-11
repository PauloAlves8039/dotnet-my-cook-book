using AutoMapper;
using MyCookBook.Communication.Requests;
using MyCookBook.Domain.Repositories;
using MyCookBook.Exceptions.ExceptionsBase;

namespace MyCookBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserUseCase(IUserWriteOnlyRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(RequestRegisterUserJson request) 
        {
            Validate(request);

            var entity = _mapper.Map<Domain.Entities.User>(request);
            entity.Password = "cript";

            await _repository.Add(entity);
            await _unitOfWork.Commit();
        }

        private void Validate(RequestRegisterUserJson request) 
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);

            if (!result.IsValid) 
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorsException(errorMessages);
            }
        }
    }
}
