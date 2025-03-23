using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;

namespace PetFamily.Accounts.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsApplication(this IServiceCollection services)
    {
        return services
            .AddCommands()
            .AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
    }

    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjection).Assembly)
           .AddClasses(classes => classes
           .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
           .AsSelfWithInterfaces()
           .WithScopedLifetime());

        return services;
    }
}
