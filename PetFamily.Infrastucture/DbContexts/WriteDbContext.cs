using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.SpeciesMenegment.Entity;

namespace PetFamily.Infrastucture.DbContexts
{
    public class WriteDbContext(IConfiguration configuration) : DbContext
    {
        public DbSet<Volunteer> Volunteers => Set<Volunteer>();
        public DbSet<Species> Species => Set<Species>();

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
                type => type.FullName?.Contains("Configuration.Write") ?? false);
        }

        private ILoggerFactory CreateLogerFactory() => LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
