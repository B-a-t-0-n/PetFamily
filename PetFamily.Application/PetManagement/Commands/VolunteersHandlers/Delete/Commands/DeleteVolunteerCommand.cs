using PetFamily.Application.Abstraction;

namespace PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.Delete.Commands
{
    public record DeleteVolunteerCommand(Guid Id) : ICommand;
}
