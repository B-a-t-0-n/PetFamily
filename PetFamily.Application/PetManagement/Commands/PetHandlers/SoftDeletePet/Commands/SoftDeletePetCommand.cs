using PetFamily.Application.Abstraction;

namespace PetFamily.Application.PetManagement.Commands.PetHandlers.SoftDeletePet.Commands
{
    public record SoftDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;
}
