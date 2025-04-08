using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.ValueObjects.IDs;

public class VolunteerRequestId : ComparableValueObject
{
    private VolunteerRequestId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static VolunteerRequestId NewVolunteerRequestId() => new VolunteerRequestId(Guid.NewGuid());

    public static VolunteerRequestId Empty() => new VolunteerRequestId(Guid.Empty);

    public static VolunteerRequestId Create(Guid id) => new(id);

    public static implicit operator Guid(VolunteerRequestId id) => id.Value;

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}
