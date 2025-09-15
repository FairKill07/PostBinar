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
        Claim[] claims = new Claim[]
        {
            new("UserId", user.Id.Value.ToString()),
            new("Email", user.Email.ToString()),
            new("FullName", user.FullName.ToString()),
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

    public bool ValidateToken(string token, out Guid userId, out string email, out string fullName)
    {
        throw new NotImplementedException();
    }

    //public bool ValidateToken(string token, out Guid userId, out string email, out string fullName)
    //{
    //    userId = Guid.Empty;
    //    email = string.Empty;
    //    fullName = string.Empty;

    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var validationParameters = new TokenValidationParameters
    //    {
    //        ValidateIssuer = true,
    //        ValidIssuer = "https://your-issuer.com",

    //        ValidateAudience = true,
    //        ValidAudience = "your-audience",

    //        ValidateLifetime = true,
    //        ClockSkew = TimeSpan.Zero,

    //        ValidateIssuerSigningKey = true,
    //        IssuerSigningKey = new SymmetricSecurityKey(
    //            Encoding.UTF8.GetBytes(_options.SecretKey))
    //    };

    //    try
    //    {
    //        var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

    //        if (validatedToken is not JwtSecurityToken jwtToken)
    //            return false;

    //        var subClaim = principal.FindFirst(ClaimTypes.NameIdentifier) ?? principal.FindFirst("sub");

    //        if (subClaim != null && Guid.TryParse(subClaim.Value, out var parsedId))
    //            userId = parsedId;

    //        email = principal.FindFirst(ClaimTypes.Email)?.Value
    //                 ?? principal.FindFirst("email")?.Value
    //                 ?? string.Empty;

    //        fullName = principal.FindFirst(ClaimTypes.Name)?.Value
    //                   ?? principal.FindFirst("name")?.Value
    //                   ?? string.Empty;

    //        return true;
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //}
}
