using PetFamily.Application.Abstraction;

namespace PetFamily.Application.PetManagement.Commands.PetHandlers.HardDeletePet.Commands
{
    public record HardDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;
}
