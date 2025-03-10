using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using NSubstitute;
using PetFamily.Core.Dtos;
using PetFamily.Core.Files;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Species.Contracts;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Infrastructure.DbContexts;
using Respawn;
using System.Data.Common;
using Testcontainers.PostgreSql;

namespace PetFamily.Volunteers.IntegrationTests;

public class IntegrationTestsWebFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:13")
        .WithDatabase("PetFamily")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private Respawner _respawner = null!;
    private DbConnection _dbConnection = null!;

    private readonly ISpeciesContract _speciesContractMock = Substitute.For<ISpeciesContract>();
    private readonly IFileProvider _fileProviderMock = Substitute.For<IFileProvider>();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(ConfigureDefaultServices);
    }

    protected virtual void ConfigureDefaultServices(IServiceCollection services)
    {
        var writeContext = services.SingleOrDefault(s =>
            s.ServiceType == typeof(WriteVolunteersDbContext));

        var readContext = services.SingleOrDefault(s =>
            s.ServiceType == typeof(IReadVolunteersDbContext));

        var speciesContract = services.SingleOrDefault(s =>
            s.ServiceType == typeof(ISpeciesContract));

        var fileProvider = services.SingleOrDefault(s =>
            s.ServiceType == typeof(IFileProvider));

        if (writeContext is not null)
            services.Remove(writeContext);

        if (readContext is not null)
            services.Remove(readContext);

        if (speciesContract is not null)
            services.Remove(speciesContract);

        if (fileProvider is not null)
            services.Remove(fileProvider);

        services.AddScoped<WriteVolunteersDbContext>(_ =>
            new WriteVolunteersDbContext(_dbContainer.GetConnectionString()));

        services.AddScoped<IReadVolunteersDbContext, ReadVolunteersDbContext>(_ =>
            new ReadVolunteersDbContext(_dbContainer.GetConnectionString()));

        services.AddTransient<ISpeciesContract>(_ => _speciesContractMock);

        services.AddTransient<IFileProvider>(_ => _fileProviderMock);
    }

    private async Task InitializeRespawner()
    {
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public", "volunteers"]
        }
        );
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WriteVolunteersDbContext>();
        await dbContext.Database.EnsureCreatedAsync();

        _dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
        await InitializeRespawner();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();
    }

    public void SetupSuccessSpeciesContractMock()
    {
        _speciesContractMock.GetSpeciesById(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(
                callInfo => Task.FromResult(
                    Result.Success<SpeciesDto, ErrorList>(
                        new SpeciesDto()
                        {
                            Id = Guid.NewGuid(),
                            Name = "test"
                        }
                    )
                )
            );

        _speciesContractMock.GetBreedById(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(
                callInfo => Task.FromResult(
                    Result.Success<BreedDto, ErrorList>(
                        new BreedDto()
                        {
                            Id = Guid.NewGuid(),
                            Name = "test"
                        }
                    )
                )
            );
    }

    public void SetupSuccessFileProviderMock()
    {
        _fileProviderMock.UploadFiles(Arg.Any<IEnumerable<FileData>>(), Arg.Any<CancellationToken>())
            .Returns(
                callInfo => Task.FromResult(
                    Result.Success<IReadOnlyList<PhotoPath>, Error>(
                        new List<PhotoPath>
                        {
                            PhotoPath.Create("testPath.png").Value,
                        }
                    )
                )
            );

        _fileProviderMock.Deletefile(Arg.Any<FileMetadata>(), Arg.Any<CancellationToken>())
            .Returns(
                callInfo => Task.FromResult(Result.Success<string, Error>("test"))
            );
    }
}
