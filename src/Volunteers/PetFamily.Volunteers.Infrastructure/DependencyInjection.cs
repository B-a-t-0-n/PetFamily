using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Files;
using PetFamily.Core.Messaging;
using PetFamily.Core.Providers;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Infrastructure.BackgroundServices;
using PetFamily.Volunteers.Infrastructure.DbContexts;
using PetFamily.Volunteers.Infrastructure.MessageQueues;
using PetFamily.Volunteers.Infrastructure.Providers;
using PetFamily.Volunteers.Infrastructure.Repositories;
using PetFamily.Volunteers.Infrastructure.Service;
using Minio;
using PetFamily.Volunteers.Infrastructure.Options;
using PetFamily.SharedKernel;

namespace PetFamily.Volunteers.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteersInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddDatabase(configuration)
            .AddMinio(configuration)
            .AddRepositories()
            .AddHostedServices()
            .AddProviders()
            .AddServices()
            .AddMessageQueues();
    }

    private static IServiceCollection AddMessageQueues(this IServiceCollection services)
    {
        services.AddSingleton<IMessageQueue<IEnumerable<FileMetadata>>, InMemoryMessageQueue<IEnumerable<FileMetadata>>>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFilesCleanerService, FilesCleanerService>();
        services.AddScoped<DeleteExpiredEntityService>();

        return services;
    }

    private static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    private static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<FilesCleanerBackgroundService>();
        services.AddHostedService<DeleteExpiredEntityBackgroundService>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();

        return services;
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(_ =>
            new WriteVolunteersDbContext(configuration.GetConnectionString(Constants.DATABASE)!));

        services.AddScoped<IReadVolunteersDbContext, ReadVolunteersDbContext>(_ =>
            new ReadVolunteersDbContext(configuration.GetConnectionString(Constants.DATABASE)!));

        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Volunteers);

        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;
    }

    private static IServiceCollection AddMinio(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>()
                ?? throw new ApplicationException("Missing minio configuration");

            options.WithEndpoint(minioOptions.Endpoint);

            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(minioOptions.WithSSL);
        });

        services.AddScoped<IFileProvider, MinioProvider>();

        return services;
    }
}
