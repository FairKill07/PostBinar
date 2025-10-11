using PostBinar.Application;
using PostBinar.Persistence;
using PostBinar.Infrastructure.Authorization;
using PostBinar.Infrastructure;
using PostBinar.Infrastructure.Authorization.Jwt;
using PostBinar.Application.Common.Mappings;
using System.Reflection;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Infrastructure.MinIO;

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
        services.Configure<MinioOptions>(_configuration.GetSection(nameof(MinioOptions)));

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

        //Cors
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:5173") // <-- твой фронтенд
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });
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

        //Cors
        app.UseCors();


        // Controllers
        app.MapControllers();
    }
}
