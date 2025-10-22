using PostBinar.Application.Common.Models.Users;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces;

public interface IJwtProvider
{
    AccessTokenResponse GenerateToken(User user);
    bool ValidateToken(string token, out Guid userId, out string email, out string fullName);
}
