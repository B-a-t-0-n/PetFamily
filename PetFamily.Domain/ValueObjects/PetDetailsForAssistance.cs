using CSharpFunctionalExtensions;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.ValueObjects
{
    public class PetDetailsForAssistance : ValueObject
    {
        private PetDetailsForAssistance(IReadOnlyList<DetailsForAssistance> detailsForAssistance)
        {
            DetailsForAssistance = detailsForAssistance;
        }

        public IReadOnlyList<DetailsForAssistance> DetailsForAssistance { get; }

        public static Result<PetDetailsForAssistance> Create(IReadOnlyList<DetailsForAssistance> detailsForAssistance)
        {
            if (detailsForAssistance == null)
                Result.Failure<PetDetailsForAssistance>("detailsForAssistance is null");

            var volunteerSocialNetwork = new PetDetailsForAssistance(detailsForAssistance!);

            return volunteerSocialNetwork;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DetailsForAssistance;
        }
    }
}
