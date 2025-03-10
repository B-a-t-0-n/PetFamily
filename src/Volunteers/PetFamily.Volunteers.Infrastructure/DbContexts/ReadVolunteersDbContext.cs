using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Dtos;
using PetFamily.SharedKernel;
using PetFamily.Volunteers.Application;

namespace PetFamily.Volunteers.Infrastructure.DbContexts;


public class ReadVolunteersDbContext : DbContext, IReadVolunteersDbContext
{
    private readonly string _connectionString;

    public IQueryable<VolunteerDto> Volunteers => Set<VolunteerDto>();

    public IQueryable<PetDto> Pets => Set<PetDto>();

    public IQueryable<PetPhotoDto> PetPhotos => Set<PetPhotoDto>();

    public ReadVolunteersDbContext(string connectionString)
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
        modelBuilder.HasDefaultSchema("volunteers");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteVolunteersDbContext).Assembly,
            type => type.FullName?.Contains("Configuration.Read") ?? false);
    }

    private ILoggerFactory CreateLogerFactory() => LoggerFactory.Create(builder => { builder.AddConsole(); });
}
