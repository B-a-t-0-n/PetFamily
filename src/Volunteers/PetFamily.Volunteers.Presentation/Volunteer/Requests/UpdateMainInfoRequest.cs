using PetFamily.Core.Dtos;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateMainInfo.Commands;

namespace PetFamily.Volunteers.Presentation.Volunteer.Requests;

public record UpdateMainInfoRequest(
    FullNameDto FullName,
    string? Description,
    int YearsExperience,
    string PhoneNumber)
{
    public UpdateMainInfoCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, FullName, Description, YearsExperience, PhoneNumber);
}
