using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Application.Common.Behaviors;
using PostBinar.Application.Services;

namespace PostBinar.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
            this IServiceCollection services)
    {
        //Services
        services.AddTransient<IUserService, UserService >();
        services.AddTransient<IProjectService, ProjectService>();


        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
