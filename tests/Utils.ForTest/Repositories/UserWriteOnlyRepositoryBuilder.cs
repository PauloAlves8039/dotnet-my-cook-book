﻿using Moq;
using MyCookBook.Domain.Repositories.User;

namespace Utils.ForTest.Repositories
{
    public class UserWriteOnlyRepositoryBuilder
    {
        private static UserWriteOnlyRepositoryBuilder _instance;
        private readonly Mock<IUserWriteOnlyRepository> _repository;

        private UserWriteOnlyRepositoryBuilder()
        {
            if (_repository == null) 
            {
                _repository = new Mock<IUserWriteOnlyRepository>();
            }
        }

        public static UserWriteOnlyRepositoryBuilder Instance() 
        {
            _instance = new UserWriteOnlyRepositoryBuilder();
            return _instance;
        }

        public IUserWriteOnlyRepository Build() 
        {
            return _repository.Object;
        }
    }
}
