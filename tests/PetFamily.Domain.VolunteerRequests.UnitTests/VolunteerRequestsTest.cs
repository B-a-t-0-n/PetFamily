using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequests.Domain.Entity;
using PetFamily.VolunteerRequests.Domain.ValueObjects;

namespace PetFamily.Domain.VolunteerRequests.UnitTests;

public class VolunteerRequestsTest
{
    private VolunteerRequest CreateVolunteer()
    {
        var volunteerInfo = VolunteerInfo.Create(
            Description.Create("1").Value,
            PhoneNumber.Create("1234567890").Value,
            YearsExperience.Create(5).Value,
            new List<Requisites> { Requisites.Create("name", "description").Value },
            new List<string> { "certificate1", "certificate2" }
        ).Value;

        return VolunteerRequest.Create(
            VolunteerRequestId.Create(Guid.NewGuid()),
            AdminId.Create(Guid.NewGuid()),
            UserId.Create(Guid.NewGuid()),
            DisscusionId.Create(Guid.NewGuid()),
            DateTime.Now,
            volunteerInfo).Value;
    }

    [Fact]
    public void CreateVolunteerRequest_ShouldCreateSuccessfully()
    {
        // Arrange & Act
        var volunteerRequest = CreateVolunteer();

        // Assert
        Assert.NotNull(volunteerRequest);
        Assert.Equal("1", volunteerRequest.VolunteerInfo.Description.Value);
        Assert.Equal("1234567890", volunteerRequest.VolunteerInfo.PhoneNumber.Number);
        Assert.Equal(5, volunteerRequest.VolunteerInfo.YearsExperience.Value);
        Assert.Single(volunteerRequest.VolunteerInfo.Requisites);
        Assert.Equal("name", volunteerRequest.VolunteerInfo.Requisites.First().Name);
        Assert.Equal("description", volunteerRequest.VolunteerInfo.Requisites.First().Description);
        Assert.Equal(2, volunteerRequest.VolunteerInfo.Certificates.Count());
    }

    [Fact]
    public void ApproveRequest_ShouldSetStatusToApproved()
    {
        // Arrange
        var volunteerRequest = CreateVolunteer();

        // Act
        volunteerRequest.ApproveRequest();

        // Assert
        Assert.Equal(Status.Approved, volunteerRequest.Status);
    }

    [Fact]
    public void RejectRequest_ShouldSetStatusToRejected()
    {
        // Arrange
        var volunteerRequest = CreateVolunteer();

        // Act
        volunteerRequest.RejectRequest();

        // Assert
        Assert.Equal(Status.Rejected, volunteerRequest.Status);
    }

    [Fact]
    public void SendRequestForRevisionRequired_ShouldSetStatusToRevisionRequired()
    {
        // Arrange
        var volunteerRequest = CreateVolunteer();
        var rejectionComment = "Needs more experience";

        // Act
        volunteerRequest.SendRequestForRevisionRequired(rejectionComment);

        // Assert
        Assert.Equal(Status.RevisionRequired, volunteerRequest.Status);
        Assert.Equal(rejectionComment, volunteerRequest.RejectionComment.Comment);
    }

    [Fact]
    public void TakeRequestOnReview_ShouldSetStatusToOnReview()
    {
        // Arrange
        var volunteerRequest = CreateVolunteer();

        // Act
        volunteerRequest.TakeRequestOnReview();

        // Assert
        Assert.Equal(Status.OnReview, volunteerRequest.Status);
    }
}
