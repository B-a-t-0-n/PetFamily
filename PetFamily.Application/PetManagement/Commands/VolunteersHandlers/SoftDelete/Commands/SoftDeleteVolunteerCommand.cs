using PetFamily.Application.Abstraction;

namespace PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.Delete.Commands
{
    public record SoftDeleteVolunteerCommand(Guid Id) : ICommand;
}
