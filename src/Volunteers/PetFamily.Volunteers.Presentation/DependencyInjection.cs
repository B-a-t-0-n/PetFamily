using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Contracts;
using PetFamily.Volunteers.Infrastructure;

namespace PetFamily.Volunteers.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteersPresentation(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddVolunteersInfrastructure(configuration)
            .AddVolunteersApplication();

        services.AddScoped<IVolunteersContract, VolunteersContract>();

        return services;
    }
}
