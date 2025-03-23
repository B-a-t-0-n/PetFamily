using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Domain;
using PetFamily.SharedKernel;

namespace PetFamily.Accounts.Infrastructure.DbContexts;

public class AccountsDbContext : IdentityDbContext<User, Role, Guid>
{
    private readonly string _connectionString;

    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

    public DbSet<Permission> Permissions => Set<Permission>();

    public DbSet<AdminAccount> AdminAccounts => Set<AdminAccount>();

    public DbSet<PartisipantAccount> PartisipantAccounts => Set<PartisipantAccount>();

    public DbSet<VolunteerAccount> VolunteerAccounts => Set<VolunteerAccount>();


    public AccountsDbContext(string connectionString)
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
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("accounts");

        modelBuilder.Entity<Role>()
            .ToTable("roles");

        modelBuilder.Entity<IdentityUserClaim<Guid>>()
            .ToTable("user_claims");

        modelBuilder.Entity<IdentityUserToken<Guid>>()
            .ToTable("user_tokens");

        modelBuilder.Entity<IdentityUserLogin<Guid>>()
            .ToTable("user_logins");

        modelBuilder.Entity<IdentityRoleClaim<Guid>>()
            .ToTable("role_claims");

        modelBuilder.Entity<IdentityUserRole<Guid>>()
            .ToTable("user_roles");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountsDbContext).Assembly);

        modelBuilder.Entity<AdminAccount>()
            .ToTable("admin_accounts");

        modelBuilder.Entity<AdminAccount>()
            .HasOne(v => v.User)
            .WithOne()
            .HasForeignKey<AdminAccount>(v => v.UserId);

        modelBuilder.Entity<PartisipantAccount>()
            .ToTable("partisipant_accounts");

        modelBuilder.Entity<PartisipantAccount>()
            .HasOne(v => v.User)
            .WithOne()
            .HasForeignKey<PartisipantAccount>(v => v.UserId);

        modelBuilder.Entity<Permission>()
            .ToTable("permissions");

        modelBuilder.Entity<Permission>()
            .HasIndex(p => p.Code)
            .IsUnique();

        modelBuilder.Entity<RolePermission>()
            .ToTable("role_permissions");

        modelBuilder.Entity<RolePermission>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);

        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany()
            .HasForeignKey(rp => rp.PermissionId);
    }

    private ILoggerFactory CreateLogerFactory() => LoggerFactory.Create(builder => { builder.AddConsole(); });

}
