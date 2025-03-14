using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Dtos;
using PetFamily.Species.Application;

namespace PetFamily.Species.Infrastructure.DbContexts;


public class ReadSpeciesDbContext : DbContext, IReadSpeciesDbContext
{
    private readonly string _connectionString;

    public IQueryable<SpeciesDto> Species => Set<SpeciesDto>();

    public IQueryable<BreedDto> Breeds => Set<BreedDto>();

    public ReadSpeciesDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLogerFactory());

        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("species");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteSpeciesDbContext).Assembly,
            type => type.FullName?.Contains("Configuration.Read") ?? false);
    }

    private ILoggerFactory CreateLogerFactory() => LoggerFactory.Create(builder => { builder.AddConsole(); });
}
