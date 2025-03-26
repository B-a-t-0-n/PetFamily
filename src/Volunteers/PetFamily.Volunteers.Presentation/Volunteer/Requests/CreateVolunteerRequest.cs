using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.Create.Commands;

namespace PetFamily.Volunteers.Presentation.Volunteer.Requests;

public record CreateVolunteerRequest(
    string? Description,
    string PhoneNumber)
{
    public CreateVolunteerCommand ToCommand() =>
        new(Description, PhoneNumber);
}
