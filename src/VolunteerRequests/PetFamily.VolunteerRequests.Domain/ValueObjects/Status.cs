using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequests.Domain.ValueObjects;

public class Status : ValueObject
{
    private Status() { }
    private Status(string status)
    {
        Value = status;
    }

    public static readonly Status Submitted = new(nameof(Submitted));
    public static readonly Status Rejected = new(nameof(Rejected));
    public static readonly Status RevisionRequired = new(nameof(RevisionRequired));
    public static readonly Status Approved = new(nameof(Approved));
    public static readonly Status OnReview = new(nameof(OnReview));

    private static readonly Status[] _all = [
        Submitted!,
        Rejected!,
        RevisionRequired!,
        Approved!,
        OnReview!];

    public string Value { get; } = default!;

    public static Result<Status, Error> Create(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            return Errors.General.ValueIsInvalid("status");

        var statusInput = status.Trim().ToLower();

        if (_all.Any(s => s.Value.ToLower() == statusInput) == false)
            return Errors.General.ValueIsInvalid("status");

        var assistanceStatus = new Status(statusInput);

        return assistanceStatus;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}