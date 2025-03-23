using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFamily.Accounts.Application.Managers;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.DbContexts;
using PetFamily.Accounts.Infrastructure.Managers;
using PetFamily.Accounts.Infrastructure.Options;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Files;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using System.Text.Json;

namespace PetFamily.Accounts.Infrastructure.Seeders;

public class AccountsSeederService
{
    private readonly AccountsDbContext _accountsDbContext;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly PermissionManager _permissionManager;
    private readonly RolePermissionManager _rolePermissionManager;
    private readonly IAdminAccountManager _adminAccountManager;
    private readonly AdminOptions _adminOptions;
    private readonly ILogger<AccountsSeederService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AccountsSeederService(
        AccountsDbContext accountsDbContext,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        PermissionManager permissionManager,
        RolePermissionManager rolePermissionManager,
        IAdminAccountManager adminAccountManager,
        IOptions<AdminOptions> adminOptions,
        ILogger<AccountsSeederService> logger,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork)
    {
        _accountsDbContext = accountsDbContext;
        _userManager = userManager;
        _roleManager = roleManager;
        _permissionManager = permissionManager;
        _rolePermissionManager = rolePermissionManager;
        _adminAccountManager = adminAccountManager;
        _adminOptions = adminOptions.Value;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task SeedAsync()
    {
        _logger.LogInformation("Seeding accounts...");

        var json = await File.ReadAllTextAsync(FilePaths.Accounts);

        var seedData = JsonSerializer.Deserialize<RolePermissionConfig>(json)
                       ?? throw new ApplicationException("Could not deserialize role permossion config.");

        await SeedPermissions(seedData);
        await SeedRoles(seedData);
        await SeedRolePermissions(seedData);
        await SeedAdmin();
    }

    private async Task SeedAdmin()
    {
        var transaction = await _unitOfWork.BeginTransaction();

        try
        {
            if (await _userManager.FindByEmailAsync(_adminOptions.Email) is not null)
                return;

            var adminRole = await _roleManager.FindByNameAsync(AdminAccount.ADMIN)
                ?? throw new ApplicationException("Could not find admin role");

            var fullName = FullName.Create(_adminOptions.Name, _adminOptions.Surname, null).Value;

            var userResult = User.CreateAdmin(
                _adminOptions.UserName,
                fullName,
                _adminOptions.Email,
                adminRole);
            if (userResult.IsFailure)
                throw new ApplicationException("Could not create user");

            await _userManager.CreateAsync(userResult.Value, _adminOptions.Password);

            var adminAccount = new AdminAccount()
            {
                Id = Guid.NewGuid(),
                UserId = userResult.Value.Id
            };

            await _adminAccountManager.CreateAdminAccount(adminAccount);

            transaction.Commit();

            _logger.LogInformation("admin seeded.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Could not create admin user in transaction");

            transaction.Rollback();

            throw new ApplicationException("Could not create admin user in transaction");
        }
    }


    private async Task SeedRolePermissions(RolePermissionConfig seedData)
    {
        foreach (var roleName in seedData.Roles.Keys)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            var rolePermissions = seedData.Roles[roleName];

            await _rolePermissionManager.AddRangeIfExist(role!.Id, rolePermissions);
        }

        _logger.LogInformation("Role permissions seeded.");
    }

    private async Task SeedRoles(RolePermissionConfig seedData)
    {
        foreach (var role in seedData.Roles.Keys)
        {
            var existingRole = await _roleManager.FindByNameAsync(role);

            if (existingRole is null)
            {
                await _roleManager.CreateAsync(new Role() { Name = role });

                _logger.LogInformation("Role {role} created.", role);
            }
        }

        _logger.LogInformation("Roles seeded.");
    }

    private async Task SeedPermissions(RolePermissionConfig seedData)
    {
        var permissionsToAdd = seedData.Permissions.SelectMany(permissionsGroup => permissionsGroup.Value);

        await _permissionManager.AddRangeIfExist(permissionsToAdd);

        _logger.LogInformation("Permissions seeded.");
    }
}
