using AutoMapper;
using MyCookBook.Application.Services.Cryptographies;
using MyCookBook.Application.Services.Token;
using MyCookBook.Communication.Requests;
using MyCookBook.Communication.Responses;
using MyCookBook.Domain.Repositories;
using MyCookBook.Exceptions.ExceptionsBase;

namespace MyCookBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly EncryptPassword _encryptPassword;
        private readonly TokenController _tokenController;

        public RegisterUserUseCase(IUserWriteOnlyRepository repository, IMapper mapper, IUnitOfWork unitOfWork, 
            EncryptPassword encryptPassword, TokenController tokenController)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _encryptPassword = encryptPassword;
            _tokenController = tokenController;
        }

        public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request) 
        {
            Validate(request);

            var entity = _mapper.Map<Domain.Entities.User>(request);
            entity.Password = _encryptPassword.Encrypt(request.Password);

            await _repository.Add(entity);
            await _unitOfWork.Commit();

            var token = _tokenController.GenerateToken(entity.Email);

            return new ResponseRegisterUserJson 
            {
                Token = token
            };
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
