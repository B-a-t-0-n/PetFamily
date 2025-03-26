namespace PetFamily.Accounts.Infrastructure.Options;

public class RefreshSessionOptions
{
    public static string RefreshSession { get; } = nameof(RefreshSession);

    public int ExpiredDaysTime { get; init; }
}
