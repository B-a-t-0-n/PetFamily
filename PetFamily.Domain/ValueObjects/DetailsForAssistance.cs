using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects
{
    public class DetailsForAssistance : ValueObject
    {
        private DetailsForAssistance(string name, string descriptuon)
        {
            Name = name;
            Description = descriptuon;
        }

        public string Name { get; protected set; } = default!;
        public string Description { get; protected set; } = default!;

        public static Result<DetailsForAssistance> Create(string name, string descriptuon)
        {
            if (string.IsNullOrWhiteSpace(name))
                Result.Failure("name is null or white space");

            if (string.IsNullOrWhiteSpace(descriptuon))
                Result.Failure("descriptuon is null or white space");

            var detailsForAssistance = new DetailsForAssistance(name, descriptuon);

            return detailsForAssistance;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Description;
        }
    }
}
