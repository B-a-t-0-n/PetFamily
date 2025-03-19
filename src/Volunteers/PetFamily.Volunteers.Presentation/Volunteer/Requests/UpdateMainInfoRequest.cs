using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateMainInfo.Commands;

namespace PetFamily.Volunteers.Presentation.Volunteer.Requests;

public record UpdateMainInfoRequest(
    string? Description,
    string PhoneNumber)
{
    public UpdateMainInfoCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, Description, PhoneNumber);
}
