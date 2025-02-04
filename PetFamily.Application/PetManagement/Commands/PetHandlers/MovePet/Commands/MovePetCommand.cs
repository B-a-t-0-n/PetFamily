using PetFamily.Application.Abstraction;

namespace PetFamily.Application.PetManagement.UseCases.PetHandlers.MovePet.Commands
{
    public record MovePetCommand(Guid VolunteerId, Guid PetId, int SerialNumber) : ICommand;
}
