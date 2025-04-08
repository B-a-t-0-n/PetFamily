using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.IDs;
using System.Numerics;

namespace PetFamily.Disscusions.Domain.Entity;

public class Disscusion : SharedKernel.Entity<DisscusionId>
{
    private readonly List<UserId> _users = [];

    private readonly List<Message> _messages = [];

    //ef core
    private Disscusion(DisscusionId id) : base(id) { }

    private Disscusion(DisscusionId id, Guid relationId, List<UserId> users) : base(id)
    {
        RelationId = relationId;
        _users = users;
    }

    public Guid RelationId { get; private set; } = default!;

    public bool IsClosed { get; private set; } = false;

    public IReadOnlyList<UserId> Users => _users;

    public IReadOnlyList<Message> Messages => _messages;

    public static Result<Disscusion, Error> Create(
        DisscusionId id,
        Guid relationId,
        List<UserId> users)
    {
        if(users == null || users.Count != 2)
            return Result.Failure<Disscusion, Error>(Errors.General.ValueIsInvalid());

        return new Disscusion(id, relationId, users);
    }

    public UnitResult<Error> AddMessage(Message message)
    {
        if(_users.Any(u => u.Value == message.UserId.Value) == false)
            return UnitResult.Failure(Errors.General.NotFound());

        if(IsClosed)
            return UnitResult.Failure(Error.Validation("disscusion.is.closed", "disscusion is closed"));

        _messages.Add(message);

        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> Close()
    {
        if (IsClosed)
            return UnitResult.Failure(Errors.General.AlreadyExist());

        IsClosed = true;

        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> EditMessage(MessageId messageId, Text text, UserId userId)
    {
        if (_users.Any(u => u.Value == userId.Value) == false)
            return UnitResult.Failure(Errors.General.NotFound());

        var message = _messages.FirstOrDefault(m => m.Id == messageId);
        if (message is null)
            return UnitResult.Failure(Errors.General.NotFound());

        if (IsClosed)
            return UnitResult.Failure(Error.Validation("disscusion.is.closed", "disscusion is closed"));

        message.Edit(text, userId);

        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> DeleteMessage(MessageId messageId, UserId userId)
    {
        if (_users.Any(u => u.Value == userId.Value) == false)
            return UnitResult.Failure(Errors.General.NotFound());

        var message = _messages.FirstOrDefault(m => m.Id == messageId);
        if (message is null)
            return UnitResult.Failure(Errors.General.NotFound());

        if (IsClosed)
            return UnitResult.Failure(Error.Validation("disscusion.is.closed", "disscusion is closed"));

        _messages.Remove(message);

        return UnitResult.Success<Error>();
    }
}
