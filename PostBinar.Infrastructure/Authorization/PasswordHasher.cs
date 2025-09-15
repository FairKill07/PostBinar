using PostBinar.Application.Abstractions.Interfaces;

namespace PostBinar.Infrastructure.Authorization;

public sealed class PasswordHasher : IPasswordHasher
{
    public string HashPasssword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool VerifyHashedPassword(string hashedPassword, string password)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}
