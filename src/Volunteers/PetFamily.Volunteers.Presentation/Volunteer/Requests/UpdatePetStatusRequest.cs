using PetFamily.Volunteers.Application.Commands.PetHandlers.UpdatePetStatus.Commands;

namespace PetFamily.Volunteers.Presentation.Volunteer.Requests;

public record UpdatePetStatusRequest(string AssistanceStatus)
{
    public UpdatePetStatusCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId, AssistanceStatus);
}
