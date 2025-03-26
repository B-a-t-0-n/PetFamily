using CSharpFunctionalExtensions;
using PetFamily.Accounts.Application.Models;
using PetFamily.Accounts.Domain;
using PetFamily.SharedKernel;
using System.Security.Claims;

namespace PetFamily.Accounts.Application;

public interface ITokenProvider
{
    TokenResult GenerateAccessToken(User user);
    Task<Guid> GenerateRefreshToken(User user, Guid accessTokenJti, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyList<Claim>, Error>> GetUserClaims(string accessToken, CancellationToken cancellationToken = default);
}
