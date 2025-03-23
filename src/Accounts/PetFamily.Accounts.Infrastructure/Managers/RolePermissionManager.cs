using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.DbContexts;

namespace PetFamily.Accounts.Infrastructure.Managers;

public class RolePermissionManager
{
    private readonly AccountsDbContext _accountsDbContext;
    private readonly ILogger<RolePermissionManager> _logger;

    public RolePermissionManager(
        AccountsDbContext accountsDbContext,
        ILogger<RolePermissionManager> logger)
    {
        _logger = logger;

        _accountsDbContext = accountsDbContext;
    }

    public async Task AddRangeIfExist(Guid roleId, IEnumerable<string> permissions)
    {
        foreach (var permissionCode in permissions)
        {
            var permission = await _accountsDbContext.Permissions
                .FirstOrDefaultAsync(p => p.Code == permissionCode);

            if (permission == null)
                throw new ApplicationException($"Permission with code {permissionCode} not found");

            var rolePermissionExist = await _accountsDbContext.RolePermissions
                .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permission!.Id);

            if (rolePermissionExist)
                continue;

            _accountsDbContext.RolePermissions.Add(
                new RolePermission()
                {
                    RoleId = roleId,
                    PermissionId = permission!.Id
                });

            _logger.LogInformation("Added permission {permissionCode} to role by Id {roleId}", permissionCode, roleId);
        }

        await _accountsDbContext.SaveChangesAsync();

        _logger.LogInformation("Added permissions to role by Id {roleId}", roleId);
    }
}
