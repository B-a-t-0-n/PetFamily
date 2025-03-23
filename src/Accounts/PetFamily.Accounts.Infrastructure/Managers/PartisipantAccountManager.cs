using PetFamily.Accounts.Application.Managers;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.DbContexts;

namespace PetFamily.Accounts.Infrastructure.Managers;

public class PartisipantAccountManager : IPartisipantAccountManager
{
    private readonly AccountsDbContext _accountsDbContext;

    public PartisipantAccountManager(AccountsDbContext accountsDbContext)
    {
        _accountsDbContext = accountsDbContext;
    }

    public async Task CreatePartisipantAccount(PartisipantAccount partisipantAccount)
    {
        await _accountsDbContext.PartisipantAccounts.AddAsync(partisipantAccount);
        await _accountsDbContext.SaveChangesAsync();
    }
}