using PetFamily.Application.PetManagement.Commands.PetHandlers.UpdatePetStatus.Commands;

namespace PetFamily.API.Controllers.Volunteer.Requests
{
    public record UpdatePetStatusRequest(string AssistanceStatus)
    {
        public UpdatePetStatusCommand ToCommand(Guid volunteerId, Guid petId) =>
            new(volunteerId, petId, AssistanceStatus);
    }
}
