﻿namespace MyCookBook.Domain.Repositories
{
    public interface IUserReadOnlyRepository
    {
        Task<bool> ExistsUserWithEmail(string email);
    }
}
