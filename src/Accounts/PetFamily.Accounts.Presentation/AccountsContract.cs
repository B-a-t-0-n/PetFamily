using PetFamily.Accounts.Contracts;
using PetFamily.Accounts.Infrastructure.Managers;

namespace PetFamily.Accounts.Presentation;

public class AccountsContract : IAccountsContract
{
    private readonly PermissionManager _permissionManager;

    public AccountsContract(PermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
    }

    public async Task<HashSet<string>> GetUserPermissionCode(Guid userId) => 
        await _permissionManager.GetUserPermissionCodes(userId);
}


