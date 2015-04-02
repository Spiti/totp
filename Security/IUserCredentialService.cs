using System;

namespace Security
{
    public interface IUserCredentialService
    {
        string GeneratePassword(int userId, DateTime dateTime);
    }
}