namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class VolunteerSocialNetwork
    {
        private VolunteerSocialNetwork() { }
        public VolunteerSocialNetwork(IReadOnlyList<SocialNetwork>? socialNetwork)
        {
            SocialNetwork = socialNetwork;
        }

        public IReadOnlyList<SocialNetwork>? SocialNetwork { get; }
    }
}
