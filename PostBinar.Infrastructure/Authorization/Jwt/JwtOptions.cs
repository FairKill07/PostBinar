namespace PostBinar.Infrastructure.Authorization.Jwt;

public class JwtOptions
{
    public string SecretKey { get; set; } = string.Empty;
    public int TokenExpirationInHours { get; set; }
}
