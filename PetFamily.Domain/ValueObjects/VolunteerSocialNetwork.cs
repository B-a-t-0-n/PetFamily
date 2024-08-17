using CSharpFunctionalExtensions;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.ValueObjects
{
    public class VolunteerSocialNetwork : ValueObject
    {
        private VolunteerSocialNetwork(IReadOnlyList<SocialNetwork> socialNetwork)
        {
            SocialNetwork = socialNetwork;
        }

        public IReadOnlyList<SocialNetwork> SocialNetwork { get; }

        public static Result<VolunteerSocialNetwork> Create(IReadOnlyList<SocialNetwork> socialNetwork)
        {
            if (socialNetwork == null)
                Result.Failure<DetailsForAssistance>("socialNetwork is null");

            var volunteerSocialNetwork = new VolunteerSocialNetwork(socialNetwork!);

            return volunteerSocialNetwork;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SocialNetwork;
            
        }
    }
}
