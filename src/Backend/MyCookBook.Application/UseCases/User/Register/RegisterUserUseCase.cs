using AutoMapper;
using MyCookBook.Application.Services.Cryptographies;
using MyCookBook.Application.Services.Token;
using MyCookBook.Communication.Requests;
using MyCookBook.Communication.Responses;
using MyCookBook.Domain.Entities;
using MyCookBook.Domain.Repositories;
using MyCookBook.Exceptions;
using MyCookBook.Exceptions.ExceptionsBase;

namespace MyCookBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUserWriteOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly EncryptPassword _encryptPassword;
        private readonly TokenController _tokenController;

        public RegisterUserUseCase(IUserWriteOnlyRepository repository, IMapper mapper, IUnitOfWork unitOfWork, 
            EncryptPassword encryptPassword, TokenController tokenController, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _encryptPassword = encryptPassword;
            _tokenController = tokenController;
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request) 
        {
            await Validate(request);

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

        private async Task Validate(RequestRegisterUserJson request) 
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);

            var existsUserWithEmail = await _userReadOnlyRepository.ExistsUserWithEmail(request.Email);

            if (existsUserWithEmail) 
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceErroMessages.REGISTERED_EMAIL));
            }

            if (!result.IsValid) 
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorsException(errorMessages);
            }
        }
    }
}
