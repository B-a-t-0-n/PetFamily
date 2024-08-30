using CSharpFunctionalExtensions;
using PetFamily.Domain.SpeciesMenegment.ValueObjects;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class VolunteerSocialNetwork : ValueObject
    {
        private VolunteerSocialNetwork() { }
        private VolunteerSocialNetwork(IReadOnlyList<SocialNetwork>? socialNetwork)
        {
            SocialNetwork = socialNetwork;
        }

        public IReadOnlyList<SocialNetwork>? SocialNetwork { get; }

        public static Result<VolunteerSocialNetwork> Create(IReadOnlyList<SocialNetwork>? socialNetwork)
        {
            if (socialNetwork == null)
                return Result.Failure<VolunteerSocialNetwork>("socialNetwork is null");

            var volunteerSocialNetwork = new VolunteerSocialNetwork(socialNetwork!);

            return volunteerSocialNetwork;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SocialNetwork == null ? "" : SocialNetwork;

        }
    }
}
