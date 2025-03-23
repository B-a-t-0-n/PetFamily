using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.Options;
using PetFamily.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetFamily.Accounts.Infrastructure.Providers;

public class JwtTokenProvider : ITokenProvider
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateAccessToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roleClaims = user.Roles.Select(r => new Claim(CustomClaims.Role, r.Name ?? string.Empty));

        Claim[] claims = [
            new Claim(CustomClaims.Id, user.Id.ToString()),
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

        return stringToken;
    }
}
