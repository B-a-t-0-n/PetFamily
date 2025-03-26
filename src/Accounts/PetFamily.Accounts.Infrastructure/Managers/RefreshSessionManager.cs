using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.DbContexts;
using PetFamily.SharedKernel;

namespace PetFamily.Accounts.Infrastructure.Managers;

public class RefreshSessionManager : IRefreshSessionManager
{
    private readonly AccountsDbContext _accountsDbContext;

    public RefreshSessionManager(AccountsDbContext accountsDbContext)
    {
        _accountsDbContext = accountsDbContext;
    }

    public async Task CreateRefreshSession(RefreshSession refreshSession, CancellationToken cancellationToken = default)
    {
        await _accountsDbContext.RefreshSessions.AddAsync(refreshSession, cancellationToken);
        await _accountsDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Result<RefreshSession, Error>> GetByRefrashToken(
        Guid refrashToken,
        CancellationToken cancellationToken = default)
    {
        var refreshSession = await _accountsDbContext.RefreshSessions
            .Include(rs => rs.User)
            .FirstOrDefaultAsync(rs => rs.RefreshToken == refrashToken, cancellationToken);
        if (refreshSession is null)
            return Error.NotFound("refreshSession.notFound", "Refresh session not found");

        return refreshSession;
    }

    public async Task Delete(RefreshSession refreshSession, CancellationToken cancellationToken = default)
    {
        _accountsDbContext.RefreshSessions.Remove(refreshSession);
        await _accountsDbContext.SaveChangesAsync(cancellationToken);
    }

}

