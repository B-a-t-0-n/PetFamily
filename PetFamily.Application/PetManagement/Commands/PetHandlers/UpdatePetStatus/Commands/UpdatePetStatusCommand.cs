using PetFamily.Application.Abstraction;

namespace PetFamily.Application.PetManagement.Commands.PetHandlers.UpdatePetStatus.Commands
{
    public record UpdatePetStatusCommand(Guid VolunteerId, Guid PetId, string AssistanceStatus) : ICommand;
}
