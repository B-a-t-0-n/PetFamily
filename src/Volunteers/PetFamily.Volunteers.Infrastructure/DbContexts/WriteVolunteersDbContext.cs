using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.SharedKernel;
using PetFamily.Volunteers.Domain.Entity;

namespace PetFamily.Volunteers.Infrastructure.DbContexts;

public class WriteVolunteersDbContext : DbContext
{
    private readonly string _connectionString;

    public DbSet<Volunteer> Volunteers => Set<Volunteer>();

    public WriteVolunteersDbContext(string connectionString)
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
        modelBuilder.HasDefaultSchema("volunteers");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteVolunteersDbContext).Assembly,
            type => type.FullName?.Contains("Configuration.Write") ?? false);
    }

    private ILoggerFactory CreateLogerFactory() => LoggerFactory.Create(builder => { builder.AddConsole(); });
}
