using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Dtos;

namespace PetFamily.Infrastucture.DbContexts
{
    public class ReadDbContext(IConfiguration configuration) : DbContext
    {
        public DbSet<VolunteerDto> Volunteers => Set<VolunteerDto>();

        public DbSet<PetDto> Pets => Set<PetDto>();

        public DbSet<PetPhotoDto> PetPhotos => Set<PetPhotoDto>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(Constants.DATABASE));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLoggerFactory(CreateLogerFactory());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(WriteDbContext).Assembly,
                type => type.FullName?.Contains("Configuration.Read") ?? false);
        }

        private ILoggerFactory CreateLogerFactory() => LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
