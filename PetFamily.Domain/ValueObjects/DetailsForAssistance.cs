using CSharpFunctionalExtensions;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.ValueObjects
{
    public class DetailsForAssistance : ValueObject
    {
        private DetailsForAssistance() { }
        private DetailsForAssistance(string name, string descriptuon)
        {
            Name = name;
            Description = descriptuon;
        }

        public string Name { get; } = default!;
        public string Description { get; } = default!;

        public static Result<DetailsForAssistance> Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                Result.Failure<DetailsForAssistance>("name is null or white space");

            if (string.IsNullOrWhiteSpace(description))
                Result.Failure<DetailsForAssistance>("description is null or white space");

            var detailsForAssistance = new DetailsForAssistance(name, description);

            return detailsForAssistance;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Description;
        }
    }
}
