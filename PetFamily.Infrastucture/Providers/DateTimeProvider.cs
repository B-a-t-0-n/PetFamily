using PetFamily.Application.Providers;

namespace PetFamily.Infrastucture.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
