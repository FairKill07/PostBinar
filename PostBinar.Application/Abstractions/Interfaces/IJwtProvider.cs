using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(User user);
    bool ValidateToken(string token, out string userId, out string email, out string name, out string phoneNumber);
}
