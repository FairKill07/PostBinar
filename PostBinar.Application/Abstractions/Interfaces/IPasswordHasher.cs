namespace PostBinar.Application.Abstractions.Interfaces;

public interface IPasswordHasher
{
    string HashPasssword(string password);
    bool VerifyHashedPassword(string hashedPassword, string password);
}
