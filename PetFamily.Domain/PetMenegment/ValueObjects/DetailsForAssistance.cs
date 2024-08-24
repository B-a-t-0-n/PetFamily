using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class DetailsForAssistance : ValueObject
    {
        public const int MAX_HIGHT_DESCRIPTION_LENGTH = 6000;

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

            if (name.Length > Constants.MAX_LOW_TEXT_LENGTH)
                Result.Failure<DetailsForAssistance>($"name > {Constants.MAX_LOW_TEXT_LENGTH}");

            if (string.IsNullOrWhiteSpace(description))
                Result.Failure<DetailsForAssistance>("description is null or white space");

            if (description.Length > MAX_HIGHT_DESCRIPTION_LENGTH)
                Result.Failure<DetailsForAssistance>($"description > {MAX_HIGHT_DESCRIPTION_LENGTH}");

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
