using PetFamily.Accounts.Application.Managers;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.DbContexts;

namespace PetFamily.Accounts.Infrastructure.Managers;

public class VolunteerAccountManager : IVolunteerAccountManager
{
    private readonly AccountsDbContext _accountsDbContext;

    public VolunteerAccountManager(AccountsDbContext accountsDbContext)
    {
        _accountsDbContext = accountsDbContext;
    }

    public async Task CreateVolunteerAccount(VolunteerAccount volunteerAccount)
    {
        await _accountsDbContext.VolunteerAccounts.AddAsync(volunteerAccount);
        await _accountsDbContext.SaveChangesAsync();
    }
}
