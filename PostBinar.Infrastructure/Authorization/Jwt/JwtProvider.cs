using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Domain.Users;

namespace PostBinar.Infrastructure.Authorization.Jwt;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }
    public string GenerateToken(User user)
    {
        Claim[] claims = new[]
        {
        new Claim("UserId", user.Id.Value.ToString()),
        new Claim("Admin", "true"),
        new Claim("Email", user.Email.ToString())
    };


        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_options.TokenExpirationInHours)
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidateToken(string token, out string userId, out string email, out string name, out string phoneNumber)
    {
        throw new NotImplementedException();
    }
}
