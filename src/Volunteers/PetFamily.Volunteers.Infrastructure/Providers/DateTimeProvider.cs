using PetFamily.Core.Providers;

namespace PetFamily.Volunteers.Infrastructure.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
