using PetFamily.Application.Abstraction;

namespace PetFamily.Application.SpeciesManagment.Commands.SpeciesHandlers.Delete.Commands
{
    public record DeleteSpeciesCommand(Guid Id) : ICommand;
}
