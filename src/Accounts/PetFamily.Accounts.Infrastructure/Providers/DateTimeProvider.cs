using PetFamily.Core.Providers;

namespace PetFamily.Accounts.Infrastructure.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
