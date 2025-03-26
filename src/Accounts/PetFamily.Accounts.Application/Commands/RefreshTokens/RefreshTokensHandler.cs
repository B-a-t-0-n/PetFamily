using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Application.Commands.RefreshTokens.Command;
using PetFamily.Accounts.Contracts.Responses;
using PetFamily.Accounts.Infrastructure.Managers;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Models;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel;

namespace PetFamily.Accounts.Application.Commands.RefreshTokens;

public class RefreshTokensHandler : ICommandHandler<LoginResponse, RefreshTokensCommand>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IRefreshSessionManager _refreshSessionManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<RefreshTokensHandler> _logger;

    public RefreshTokensHandler(
        IDateTimeProvider dateTimeProvider,
        IRefreshSessionManager refreshSessionManager,
        ITokenProvider tokenProvider,
        ILogger<RefreshTokensHandler> logger
        )
    {
        _dateTimeProvider = dateTimeProvider;
        _refreshSessionManager = refreshSessionManager;
        _tokenProvider = tokenProvider;
        _logger = logger;
    }

    public async Task<Result<LoginResponse, ErrorList>> Handle(
        RefreshTokensCommand command,
        CancellationToken cancellationToken = default)
    {
        var oldRefrashSession = await _refreshSessionManager
            .GetByRefrashToken(command.RefreshToken, cancellationToken);
        if (oldRefrashSession.IsFailure)
            return oldRefrashSession.Error.ToErrorList();

        if (oldRefrashSession.Value.ExpiresIn < _dateTimeProvider.UtcNow)
            return Errors.Tokens.ExpiredToken().ToErrorList();

        var userClaimsResult = await _tokenProvider.GetUserClaims(command.AccessToken, cancellationToken);
        if(userClaimsResult.IsFailure)
            return userClaimsResult.Error.ToErrorList();

        var userIdString = userClaimsResult.Value.FirstOrDefault(c => c.Type == CustomClaims.Id)?.Value;
        if (!Guid.TryParse(userIdString, out var userId))
            return Errors.Tokens.InvalidToken().ToErrorList();

        if(oldRefrashSession.Value.UserId != userId)
            return Errors.Tokens.InvalidToken().ToErrorList();

        var userJtiString = userClaimsResult.Value.FirstOrDefault(c => c.Type == CustomClaims.Jti)?.Value;
        if (!Guid.TryParse(userJtiString, out var userJtiGuid))
            return Errors.Tokens.InvalidToken().ToErrorList();

        if (oldRefrashSession.Value.Jti != userJtiGuid)
            return Errors.Tokens.InvalidToken().ToErrorList();

        await _refreshSessionManager.Delete(oldRefrashSession.Value, cancellationToken);

        var accessToken = _tokenProvider.GenerateAccessToken(oldRefrashSession.Value.User);
        var refrashToken = await _tokenProvider.GenerateRefreshToken(oldRefrashSession.Value.User, accessToken.Jti, cancellationToken);

        return new LoginResponse(accessToken.AccessToken, refrashToken);
    }
}
