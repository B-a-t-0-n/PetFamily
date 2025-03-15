using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetFamily.Species.Infrastructure.DbContexts;

public class WriteSpeciesDbContext : DbContext
{
    private readonly string _connectionString;

    public DbSet<Domain.Entity.Species> Species => Set<Domain.Entity.Species>();

    public WriteSpeciesDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLogerFactory());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("species");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteSpeciesDbContext).Assembly,
            type => type.FullName?.Contains("Configuration.Write") ?? false);
    }

    private ILoggerFactory CreateLogerFactory() => LoggerFactory.Create(builder => { builder.AddConsole(); });
}
