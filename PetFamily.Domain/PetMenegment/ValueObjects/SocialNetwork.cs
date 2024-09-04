using CSharpFunctionalExtensions;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class SocialNetwork : ValueObject
    {
        public const int MAX_HIGHT_NAME_LENGTH = 50;

        private SocialNetwork() { }
        private SocialNetwork(string? name, string? link)
        {
            Name = name;
            Link = link;
        }

        public string? Name { get; } = default!;
        public string? Link { get; } = default!;

        public static Result<SocialNetwork> Create(string? name, string? link)
        {

            if (name != null && name.Length > MAX_HIGHT_NAME_LENGTH)
                return Result.Failure<SocialNetwork>($"name > {MAX_HIGHT_NAME_LENGTH}");

            var socialNetwork = new SocialNetwork(name, link);

            return socialNetwork;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name == null ? "" : Name;
            yield return Link == null ? "" : Link;
        }
    }
}
