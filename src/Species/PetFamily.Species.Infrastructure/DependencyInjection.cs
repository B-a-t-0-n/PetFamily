using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.SharedKernel;
using PetFamily.Species.Application;
using PetFamily.Species.Infrastructure.DbContexts;
using PetFamily.Species.Infrastructure.Repositories;

namespace PetFamily.Species.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddDatabase(configuration)
            .AddRepositories();
    }

    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddScoped<ISpeciesRepository, SpeciesRepository>();
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(_ =>
            new WriteSpeciesDbContext(configuration.GetConnectionString(Constants.DATABASE)!));

        services.AddScoped<IReadSpeciesDbContext, ReadSpeciesDbContext>(_ =>
            new ReadSpeciesDbContext(configuration.GetConnectionString(Constants.DATABASE)!));

        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Species);

        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;
    }
}
