using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.ValueObjects.IDs;

public class BreedId : ComparableValueObject
{
    private BreedId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static BreedId NewBreedId() => new BreedId(Guid.NewGuid());

    public static BreedId Empty() => new BreedId(Guid.Empty);

    public static BreedId Create(Guid id) => new(id);

    public static implicit operator Guid(BreedId id) => id.Value;

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}
