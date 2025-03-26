
namespace PetFamily.Accounts.Contracts;

public interface IAccountsContract
{
    Task<HashSet<string>> GetUserPermissionCode(Guid userId);
}
