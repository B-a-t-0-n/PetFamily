using PetFamily.Application.Abstraction;

namespace PetFamily.Application.PetManagement.Commands.VolunteersHandlers.HardDelete.Commands
{
    public record HardDeleteVolunteerCommand(Guid Id) : ICommand;
}
