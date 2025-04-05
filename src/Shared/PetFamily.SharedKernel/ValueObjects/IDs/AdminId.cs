using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.ValueObjects.IDs;

public class AdminId : ComparableValueObject
{
    private AdminId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static AdminId NewPetId() => new AdminId(Guid.NewGuid());

    public static AdminId Empty() => new AdminId(Guid.Empty);

    public static AdminId Create(Guid id) => new(id);

    public static implicit operator Guid(AdminId id) => id.Value;

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}