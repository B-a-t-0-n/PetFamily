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

    public async Task<Permission?> FindByCode(string code) =>
        await _accountsDbContext.Permissions.FirstOrDefaultAsync(p => p.Code == code);

    public async Task AddRangeIfExist(IEnumerable<string> permissions)
    {
        foreach (var permissionCode in permissions)
        {
            var isPermissionsexist = await _accountsDbContext.Permissions.AnyAsync(p => p.Code == permissionCode);

            if (isPermissionsexist)
                continue;

            await _accountsDbContext.Permissions.AddAsync(new Permission { Code = permissionCode });

            _logger.LogInformation("Added permission with code {code}", permissionCode);
        }

        _accountsDbContext.SaveChanges();
    }

    public async Task<HashSet<string>> GetUserPermissionCodes(Guid userId)
    {
        var permissions = await _accountsDbContext.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles)
            .SelectMany(r => r.RolePermissions)
            .Select(rp => rp.Permission.Code)
            .Distinct()
            .ToListAsync();

        return permissions.ToHashSet();
    }
}
