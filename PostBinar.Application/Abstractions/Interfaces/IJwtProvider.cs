using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(User user);
    bool ValidateToken(string token, out Guid userId, out string email, out string fullName);
}
