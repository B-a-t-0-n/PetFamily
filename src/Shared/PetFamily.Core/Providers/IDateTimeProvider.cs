namespace PetFamily.Core.Providers;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
