using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Infrastructure;
using PetFamily.Accounts.Application;
namespace PetFamily.Accounts.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountPresentation(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAccountsInfrastructure(configuration)
            .AddAccountsApplication();

        return services;
    }
}

