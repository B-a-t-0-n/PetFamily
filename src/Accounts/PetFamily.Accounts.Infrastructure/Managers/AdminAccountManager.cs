using PetFamily.Accounts.Application.Managers;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.DbContexts;

namespace PetFamily.Accounts.Infrastructure.Managers;

public class AdminAccountManager : IAdminAccountManager
{
    private readonly AccountsDbContext _accountsDbContext;

    public AdminAccountManager(AccountsDbContext accountsDbContext)
    {
        _accountsDbContext = accountsDbContext;
    }

    public async Task CreateAdminAccount(AdminAccount adminAccount)
    {
        await _accountsDbContext.AdminAccounts.AddAsync(adminAccount);
        await _accountsDbContext.SaveChangesAsync();
    }
}
