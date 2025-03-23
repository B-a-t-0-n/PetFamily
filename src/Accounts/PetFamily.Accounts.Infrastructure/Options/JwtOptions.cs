namespace PetFamily.Accounts.Infrastructure.Options;

public class JwtOptions
{
    public static string JWT { get; } = nameof(JWT);

    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string Key { get; init; } = string.Empty;
    public string ExpiredMinutiesTime { get; init; } = string.Empty;
}