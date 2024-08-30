using CSharpFunctionalExtensions;
using PetFamily.Domain.SpeciesMenegment.ValueObjects;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class VolunteerDetailsForAssistance : ValueObject
    {
        private VolunteerDetailsForAssistance() { }
        private VolunteerDetailsForAssistance(IReadOnlyList<DetailsForAssistance>? detailsForAssistance)
        {
            DetailsForAssistance = detailsForAssistance;
        }

        public IReadOnlyList<DetailsForAssistance>? DetailsForAssistance { get; } = default!;

        public static Result<VolunteerDetailsForAssistance> Create(IReadOnlyList<DetailsForAssistance>? detailsForAssistance)
        {
            if (detailsForAssistance == null)
                return Result.Failure<VolunteerDetailsForAssistance>("detailsForAssistance is null");

            var volunteerSocialNetwork = new VolunteerDetailsForAssistance(detailsForAssistance!);

            return volunteerSocialNetwork;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DetailsForAssistance == null ? "" : DetailsForAssistance;

        }
    }
}
