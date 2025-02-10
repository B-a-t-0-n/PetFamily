using PetFamily.Application.Abstraction;
using PetFamily.Application.PetManagement.UseCases.PetHandlers.MovePet.Commands;

namespace PetFamily.Application.PetManagement.Commands.PetHandlers.SetMainPhotoPet.Commands
{
    public record SetMainPhotoPetCommand(Guid VolunteerId, Guid PetId, string Path) : ICommand;
}
