using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Application.Models;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.DbContexts;
using PetFamily.Accounts.Infrastructure.Factory;
using PetFamily.Accounts.Infrastructure.Managers;
using PetFamily.Accounts.Infrastructure.Options;
using PetFamily.Core.Models;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetFamily.Accounts.Infrastructure.Providers;

public class JwtTokenProvider : ITokenProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly RefreshSessionOptions _refreshSessionOptions;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IRefreshSessionManager _refreshSessionManager;

    public JwtTokenProvider(
        IOptions<RefreshSessionOptions> refreshSessionOptions,
        IOptions<JwtOptions> jwtOptions,
        IDateTimeProvider dateTimeProvider,
        IRefreshSessionManager refreshSessionManager)
    {
        _jwtOptions = jwtOptions.Value;
        _refreshSessionOptions = refreshSessionOptions.Value;
        _dateTimeProvider = dateTimeProvider;
        _refreshSessionManager = refreshSessionManager;
    }

    public TokenResult GenerateAccessToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roleClaims = user.Roles.Select(r => new Claim(CustomClaims.Role, r.Name ?? string.Empty));

        var jti = Guid.NewGuid();

        Claim[] claims = [
            new Claim(CustomClaims.Id, user.Id.ToString()),
            new Claim(CustomClaims.Jti, jti.ToString()),
            new Claim(CustomClaims.Email, user.Email ?? ""),
        ];

        claims = claims.Concat(roleClaims).ToArray();

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_jwtOptions.ExpiredMinutiesTime)),
            signingCredentials: signingCredentials,
            claims: claims
        );

        var stringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return new TokenResult(stringToken, jti);
    }

    public async Task<Guid> GenerateRefreshToken(User user, Guid accessTokenJti, CancellationToken cancellationToken = default)
    {
        var refrashSession = new RefreshSession
        {
            UserId = user.Id,
            RefreshToken = Guid.NewGuid(),
            ExpiresIn = _dateTimeProvider.UtcNow.AddDays(_refreshSessionOptions.ExpiredDaysTime),
            CreatedAt = _dateTimeProvider.UtcNow,
            Jti = accessTokenJti
        };

        await _refreshSessionManager.CreateRefreshSession(refrashSession, cancellationToken);

        return refrashSession.RefreshToken;
    }

    public async Task<Result<IReadOnlyList<Claim>,Error>> GetUserClaims(string accessToken, CancellationToken cancellationToken = default)
    {
        var jwtHandler = new JwtSecurityTokenHandler();

        var validationParameters = TokenValidationParametersFactory.CreateWithoutLifeTime(_jwtOptions);

        var validationResult = await jwtHandler.ValidateTokenAsync(accessToken, validationParameters);
        if(validationResult.IsValid == false)
            return Errors.Tokens.InvalidToken();

        return validationResult.ClaimsIdentity.Claims.ToList();
    }
}
