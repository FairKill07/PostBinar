using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Infrastructure.Authorization;
using PostBinar.Infrastructure.Authorization.Jwt;

namespace PostBinar.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}
