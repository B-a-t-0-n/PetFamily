using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Abstractions;
using PetFamily.SharedKernel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Infrastructure.DbContexts;
using PetFamily.Accounts.Infrastructure.Options;
using PetFamily.Accounts.Infrastructure.Providers;
using PetFamily.Accounts.Infrastructure.Managers;
using PetFamily.Accounts.Infrastructure.Seeders;
using Microsoft.AspNetCore.Authorization;
using PetFamily.Framework.Authorization;
using PetFamily.Accounts.Application.Managers;
using PetFamily.Core.Providers;
using PetFamily.Accounts.Infrastructure.Factory;


namespace PetFamily.Accounts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddTransient<IDateTimeProvider, DateTimeProvider>()
            .AddIdentity()
            .AddJwt(configuration)
            .AddDbContext(configuration)
            .AddSeeding(configuration)
            .AddAuthorization()
            .AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>()
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>(); 
    }

    private static IServiceCollection AddSeeding(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AdminOptions>(
            configuration.GetSection(AdminOptions.ADMIN));

        services.AddScoped<AccountsSeederService>();
        services.AddSingleton<AccountsSeeder>();

        return services;
    }

    private static IServiceCollection AddJwt(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<ITokenProvider, JwtTokenProvider>();

        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.JWT));

        services.Configure<RefreshSessionOptions>(
            configuration.GetSection(RefreshSessionOptions.RefreshSession));

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtOptions = configuration.GetSection(JwtOptions.JWT).Get<JwtOptions>()
                                   ?? throw new ApplicationException("Missing jwt configuration");

                options.TokenValidationParameters = TokenValidationParametersFactory.CreateWithLifeTime(jwtOptions);
            });
        return services;
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AccountsDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<PermissionManager>();
        services.AddScoped<RolePermissionManager>();
        services.AddScoped<IAccountsManager, AccountsManager>();
        services.AddScoped<IRefreshSessionManager, RefreshSessionManager>();

        return services;
    }

    private static IServiceCollection AddDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IReadAccountsDbContext, AccountsDbContext>(_ =>
            new AccountsDbContext(configuration.GetConnectionString("Database")!));

        services.AddScoped<AccountsDbContext>(_ =>
            new AccountsDbContext(configuration.GetConnectionString("Database")!));

        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Accounts);

        return services;
    }
}
