using CSharpFunctionalExtensions;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class SocialNetwork : ValueObject
    {
        public const int MAX_HIGHT_NAME_LENGTH = 50;

        private SocialNetwork() { }
        private SocialNetwork(string name, string link)
        {
            Name = name;
            Link = link;
        }

        public string Name { get; } = default!;
        public string Link { get; } = default!;

        public static Result<SocialNetwork> Create(string name, string link)
        {
            if (string.IsNullOrWhiteSpace(name))
                Result.Failure<SocialNetwork>("name is null or white space");

            if (name.Length > MAX_HIGHT_NAME_LENGTH)
                Result.Failure<SocialNetwork>($"name > {MAX_HIGHT_NAME_LENGTH}");

            if (string.IsNullOrWhiteSpace(link))
                Result.Failure<SocialNetwork>("link is null or white space");

            var socialNetwork = new SocialNetwork(name, link);

            return socialNetwork;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Link;
        }
    }
}
