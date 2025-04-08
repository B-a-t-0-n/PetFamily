using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.IDs;

namespace PetFamily.Disscusions.Domain.Entity;

public class Message : SharedKernel.Entity<MessageId>
{
    //ef core
    private Message(MessageId id) : base(id) { }

    private Message(MessageId id, Text text, DateTime createdAt, UserId userId) : base(id)
    {
        Text = text;
        CreatedAt = createdAt;
        UserId = userId;
    }

    public Text Text { get; private set; } = default!;

    public DateTime CreatedAt { get; private set; }

    public bool IsEdited { get; private set; } = false;

    public UserId UserId { get; private set; } = default!;

    public static Result<Message, Error> Create(
        MessageId id,
        Text text,
        DateTime createdAt,
        UserId userId)
    {
        var message = new Message(id, text, createdAt, userId);
        return message;
    }

    internal UnitResult<Error> Edit(Text text, UserId userId)
    {
        if (userId != UserId)
            return UnitResult.Failure(Errors.General.NotFound());

        Text = text;
        IsEdited = true;

        return UnitResult.Success<Error>();
    }
}
