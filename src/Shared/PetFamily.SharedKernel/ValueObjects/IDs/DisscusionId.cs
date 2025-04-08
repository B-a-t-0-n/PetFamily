using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.ValueObjects.IDs;

public class DisscusionId : ComparableValueObject
{
    private DisscusionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static DisscusionId NewDisscusionId() => new DisscusionId(Guid.NewGuid());

    public static DisscusionId Empty() => new DisscusionId(Guid.Empty);

    public static DisscusionId Create(Guid id) => new(id);

    public static implicit operator Guid(DisscusionId id) => id.Value;

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}