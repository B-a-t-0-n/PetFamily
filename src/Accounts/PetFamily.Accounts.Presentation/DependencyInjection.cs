using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Infrastructure;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Contracts;
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

        services.AddScoped<IAccountsContract, AccountsContract>();

        return services;
    }
}

