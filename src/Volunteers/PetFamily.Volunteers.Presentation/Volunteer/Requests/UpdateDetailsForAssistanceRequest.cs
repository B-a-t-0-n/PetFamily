using PetFamily.Core.Dtos;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateDetailsForAssistance.Commands;

namespace PetFamily.Volunteers.Presentation.Volunteer.Requests;

public record UpdateDetailsForAssistanceRequest(IEnumerable<DetailsForAssistanceDto> DetailsForAssistance)
{
    public UpdateDetailsForAssistanceCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, DetailsForAssistance);
}
