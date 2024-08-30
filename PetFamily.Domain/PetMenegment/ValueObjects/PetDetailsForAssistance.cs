using CSharpFunctionalExtensions;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class PetDetailsForAssistance : ValueObject
    {
        private PetDetailsForAssistance() { }
        private PetDetailsForAssistance(IReadOnlyList<DetailsForAssistance> detailsForAssistance)
        {
            DetailsForAssistance = detailsForAssistance;
        }

        public IReadOnlyList<DetailsForAssistance> DetailsForAssistance { get; } = default!;

        public static Result<PetDetailsForAssistance> Create(IReadOnlyList<DetailsForAssistance> detailsForAssistance)
        {
            if (detailsForAssistance == null)
                return Result.Failure<PetDetailsForAssistance>("detailsForAssistance is null");

            var volunteerSocialNetwork = new PetDetailsForAssistance(detailsForAssistance!);

            return volunteerSocialNetwork;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DetailsForAssistance;
        }
    }
}
