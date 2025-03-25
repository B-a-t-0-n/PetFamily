using PetFamily.Accounts.Application.Managers;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.DbContexts;

namespace PetFamily.Accounts.Infrastructure.Managers;

public class AccountsManager : IAccountsManager
{
    private readonly AccountsDbContext _accountsDbContext;

    public AccountsManager(AccountsDbContext accountsDbContext)
    {
        _accountsDbContext = accountsDbContext;
    }

    public async Task CreateAdminAccount(AdminAccount adminAccount, CancellationToken cancellationToken = default)
    {
        await _accountsDbContext.AdminAccounts.AddAsync(adminAccount, cancellationToken);
        await _accountsDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task CreatePartisipantAccount(PartisipantAccount partisipantAccount, CancellationToken cancellationToken = default)
    {
        await _accountsDbContext.PartisipantAccounts.AddAsync(partisipantAccount, cancellationToken);
        await _accountsDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateVolunteerAccount(VolunteerAccount volunteerAccount, CancellationToken cancellationToken = default)
    {
        await _accountsDbContext.VolunteerAccounts.AddAsync(volunteerAccount, cancellationToken);
        await _accountsDbContext.SaveChangesAsync(cancellationToken);
    }
}

