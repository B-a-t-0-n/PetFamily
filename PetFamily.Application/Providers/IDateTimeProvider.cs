namespace PetFamily.Application.Providers
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
    }
}
