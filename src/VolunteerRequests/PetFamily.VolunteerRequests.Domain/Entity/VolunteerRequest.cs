using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.VolunteerRequests.Domain.ValueObjects;

namespace PetFamily.VolunteerRequests.Domain.Entity;

public class VolunteerRequest : SharedKernel.Entity<VolunteerRequestId>
{
    //ef core
    private VolunteerRequest(VolunteerRequestId id) : base(id) { }

    private VolunteerRequest(
        VolunteerRequestId id,
        AdminId adminId,
        UserId userId,
        DisscusionId disscusionId,
        DateTime createdAt,
        Status status,
        VolunteerInfo volunteerInfo,
        RejectionComment rejectionComment) : base(id)
    {
        AdminId = adminId;
        UserId = userId;
        DisscusionId = disscusionId;
        CreatedAt = createdAt;
        Status = status;
        VolunteerInfo = volunteerInfo;
        RejectionComment = rejectionComment;
    }

    public AdminId AdminId { get; private set; } = default!;

    public UserId UserId { get; private set; } = default!;

    public DisscusionId DisscusionId { get; private set; } = default!;

    public DateTime CreatedAt { get; private set; }

    public Status Status { get; private set; } = default!;

    public VolunteerInfo VolunteerInfo { get; private set; } = default!;

    public RejectionComment RejectionComment { get; private set; } = default!;

    public static Result<VolunteerRequest, Error> Create(
        VolunteerRequestId id,
        AdminId adminId,
        UserId userId,
        DisscusionId disscusionId,
        DateTime createdAt,
        VolunteerInfo volunteerInfo)
    {
        var status = Status.Submitted;

        var rejectionCommentResult = RejectionComment.Create(null);
        if (rejectionCommentResult.IsFailure)
            return Result.Failure<VolunteerRequest, Error>(rejectionCommentResult.Error);

        var volunteerRequest = new VolunteerRequest(
            id,
            adminId,
            userId,
            disscusionId,
            createdAt,
            status, 
            volunteerInfo,
            rejectionCommentResult.Value);

        return volunteerRequest;
    }

    public void TakeRequestOnReview()
    {
        Status = Status.OnReview;
    }

    public UnitResult<Error> SendRequestForRevisionRequired(string rejectionComment)
    {
        var rejectionCommentResult = RejectionComment.Create(rejectionComment);
        if (rejectionCommentResult.IsFailure)
            return Result.Failure<VolunteerRequest, Error>(rejectionCommentResult.Error);

        RejectionComment = rejectionCommentResult.Value;

        Status = Status.RevisionRequired;

        return UnitResult.Success<Error>();
    }

    public void RejectRequest()
    {
        Status = Status.Rejected;
    }

    public void ApproveRequest()
    {
        Status = Status.Approved;
    }
}
