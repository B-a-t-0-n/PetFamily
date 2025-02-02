using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetFamily.Application.Database;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Messaging;
using PetFamily.Application.PetManagement;
using PetFamily.Application.Providers;
using PetFamily.Application.Species;
using PetFamily.Infrastucture.BackgroundServices;
using PetFamily.Infrastucture.DbContexts;
using PetFamily.Infrastucture.Files;
using PetFamily.Infrastucture.MessageQueues;
using PetFamily.Infrastucture.Options;
using PetFamily.Infrastucture.Providers;
using PetFamily.Infrastucture.Repositories;

namespace PetFamily.Infrastucture
{
    public static class Inject
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<WriteDbContext>();
            services.AddScoped<ReadDbContext>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            services.AddScoped<ISpeciesRepository, SpeciesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            services.AddMinio(configuration);

            services.AddHostedService<FilesCleanerBackgroundService>();

            services.AddSingleton<IMessageQueue<IEnumerable<FileMetadata>>, InMemoryMessageQueue<IEnumerable<FileMetadata>>>();

            services.AddScoped<IFilesCleanerService, FilesCleanerService>();

            return services;
        }

        private static IServiceCollection AddMinio(
            this IServiceCollection services, IConfiguration configuration)
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
}
