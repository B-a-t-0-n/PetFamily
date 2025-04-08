using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.ValueObjects.IDs;

public class UserId : ComparableValueObject
{
    private UserId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static UserId NewUserId() => new UserId(Guid.NewGuid());

    public static UserId Empty() => new UserId(Guid.Empty);

    public static UserId Create(Guid id) => new(id);

    public static implicit operator Guid(UserId id) => id.Value;

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}
