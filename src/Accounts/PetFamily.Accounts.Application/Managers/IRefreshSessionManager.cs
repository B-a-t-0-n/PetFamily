using CSharpFunctionalExtensions;
using PetFamily.Accounts.Domain;
using PetFamily.SharedKernel;

namespace PetFamily.Accounts.Infrastructure.Managers
{
    public interface IRefreshSessionManager
    {
        Task CreateRefreshSession(RefreshSession refreshSession, CancellationToken cancellationToken = default);
        Task<Result<RefreshSession, Error>> GetByRefrashToken(Guid refrashToken, CancellationToken cancellationToken = default);
        Task Delete(RefreshSession refreshSession, CancellationToken cancellationToken = default);
    }
}