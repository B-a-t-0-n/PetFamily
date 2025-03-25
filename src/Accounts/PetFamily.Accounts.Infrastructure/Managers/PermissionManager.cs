using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.DbContexts;

namespace PetFamily.Accounts.Infrastructure.Managers;

public class PermissionManager
{
    private readonly AccountsDbContext _accountsDbContext;
    private readonly ILogger<PermissionManager> _logger;

    public PermissionManager(
        AccountsDbContext accountsDbContext,
        ILogger<PermissionManager> logger)
    {
        _logger = logger;
        _accountsDbContext = accountsDbContext;
    }

    public async Task<Permission?> FindByCode(string code, CancellationToken cancellationToken = default) =>
        await _accountsDbContext.Permissions.FirstOrDefaultAsync(p => p.Code == code, cancellationToken);

    public async Task AddRangeIfExist(IEnumerable<string> permissions, CancellationToken cancellationToken = default)
    {
        foreach (var permissionCode in permissions)
        {
            var isPermissionsexist = await _accountsDbContext.Permissions.AnyAsync(p => p.Code == permissionCode, cancellationToken);

            if (isPermissionsexist)
                continue;

            await _accountsDbContext.Permissions.AddAsync(new Permission { Code = permissionCode }, cancellationToken);

            _logger.LogInformation("Added permission with code {code}", permissionCode);
        }

        await _accountsDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<HashSet<string>> GetUserPermissionCodes(Guid userId, CancellationToken cancellationToken = default)
    {
        var permissions = await _accountsDbContext.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles)
            .SelectMany(r => r.RolePermissions)
            .Select(rp => rp.Permission.Code)
            .Distinct()
            .ToListAsync(cancellationToken);

        return permissions.ToHashSet();
    }
}
