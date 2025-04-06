using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.ValueObjects.IDs;

public class MessageId : ComparableValueObject
{
    private MessageId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static MessageId NewMessageId() => new MessageId(Guid.NewGuid());

    public static MessageId Empty() => new MessageId(Guid.Empty);

    public static MessageId Create(Guid id) => new(id);

    public static implicit operator Guid(MessageId id) => id.Value;

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}