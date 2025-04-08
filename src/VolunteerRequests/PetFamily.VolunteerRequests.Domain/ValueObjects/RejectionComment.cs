using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequests.Domain.ValueObjects;

public class RejectionComment : ValueObject
{
    private RejectionComment() { }
    private RejectionComment(string? сomment)
    {
        Comment = сomment;
    }

    public string? Comment { get; }

    public static Result<RejectionComment, Error> Create(string? value)
    {
        if (value != null && value.Length > Constants.MAX_HIGHT_TEXT_LENGTH)
            return Errors.General.ValueIsRequired("rejectionComment");

        var rejectionComment = new RejectionComment(value);

        return rejectionComment;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Comment == null ? "" : Comment;
    }
}