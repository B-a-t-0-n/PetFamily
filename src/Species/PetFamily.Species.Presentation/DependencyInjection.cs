using Microsoft.Extensions.DependencyInjection;
using PetFamily.Species.Application;
using PetFamily.Species.Contracts;
using PetFamily.Species.Infrastructure;

namespace PetFamily.Species.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesPresentation(this IServiceCollection services)
    {
        services
            .AddSpeciesInfrastructure()
            .AddSpeciesApplication();

        services.AddScoped<ISpeciesContract, SpeciesContract>();

        return services;
    }
}
