using PostBinar.Application;
using PostBinar.Persistence;
using PostBinar.Infrastructure.Authorization;
using PostBinar.Infrastructure;
using PostBinar.Infrastructure.Authorization.Jwt;
using PostBinar.Application.Common.Mappings;
using System.Reflection;
using PostBinar.Application.Abstractions.Interfaces;

namespace PostBinar.Api;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Controllers
        services.AddControllers();

        // Options
        services.Configure<AuthorizationOptions>(_configuration.GetSection(nameof(AuthorizationOptions)));
        services.Configure<JwtOptions>(_configuration.GetSection(nameof(JwtOptions)));

        //AutoMapper
        services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new AssemblyMappingProfile(typeof(IPostBinarDbContext).Assembly));
        });

        // Persistence
        services.AddPersistence(_configuration);
        services.AddInfrastructure();
        services.AddApplication();

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    // Здесь middleware и пайплайн
    public void Configure(WebApplication app)
    {
        // Swagger
        app.UseSwagger();
        app.UseSwaggerUI();

        // HTTPS
        app.UseHttpsRedirection();

        // Authorization
        app.UseAuthorization();

        // Controllers
        app.MapControllers();
    }
}
