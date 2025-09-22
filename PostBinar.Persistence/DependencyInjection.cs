using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Persistence.DbContects;
using PostBinar.Persistence.Repositories;

namespace PostBinar.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        //Services
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IProjectRepository, ProjectRepository>();
        services.AddTransient<IProjectMembershipRepository, ProjectMembershipRepository>();
        services.AddTransient<IMembershipRoleRepository, MembershipRoleRepository>();

        services.AddDbContext<PostBinarDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            options.UseNpgsql(
                connectionString,
                b => b.MigrationsAssembly("PostBinar.Persistence"));
        });

        services.AddScoped<IUnitOfWork>(serviceProvider =>
                serviceProvider.GetRequiredService<PostBinarDbContext>());

        return services;
    }
}
