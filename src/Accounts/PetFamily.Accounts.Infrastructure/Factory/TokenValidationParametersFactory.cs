using Microsoft.IdentityModel.Tokens;
using PetFamily.Accounts.Infrastructure.Options;
using System.Text;

namespace PetFamily.Accounts.Infrastructure.Factory;

public static class TokenValidationParametersFactory
{
    public static TokenValidationParameters CreateWithLifeTime(JwtOptions jwtOptions) =>
        new()
        {
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

        };

    public static TokenValidationParameters CreateWithoutLifeTime(JwtOptions jwtOptions) =>
        new()
        {
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,

        };
}
