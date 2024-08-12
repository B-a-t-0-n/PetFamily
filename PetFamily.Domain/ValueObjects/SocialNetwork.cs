using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects
{
    public class SocialNetwork : ValueObject
    {
        private SocialNetwork(string name, string link)
        {
            Name = name;
            Link = link;
        }

        public string Name { get; protected set; } = default!;
        public string Link { get; protected set; } = default!;

        public static Result<SocialNetwork> Create(string name, string link)
        {
            if (string.IsNullOrWhiteSpace(name))
                Result.Failure<SocialNetwork>("name is null or white space");

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
