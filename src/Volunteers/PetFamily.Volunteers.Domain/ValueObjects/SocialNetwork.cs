using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.Volunteers.Domain.ValueObjects;

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

    public static Result<SocialNetwork, Error> Create(string? name, string? link)
    {

        if (name != null && name.Length > MAX_HIGHT_NAME_LENGTH)
            return Errors.General.ValueIsRequired("name");

        var socialNetwork = new SocialNetwork(name, link);

        return socialNetwork;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Name == null ? "" : Name;
        yield return Link == null ? "" : Link;
    }
}
