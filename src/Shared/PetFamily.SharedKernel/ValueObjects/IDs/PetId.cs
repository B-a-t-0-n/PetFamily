using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.ValueObjects.IDs;

public class PetId : ComparableValueObject
{
    private PetId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PetId NewPetId() => new PetId(Guid.NewGuid());

    public static PetId Empty() => new PetId(Guid.Empty);

    public static PetId Create(Guid id) => new(id);

    public static implicit operator Guid(PetId id) => id.Value;

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}