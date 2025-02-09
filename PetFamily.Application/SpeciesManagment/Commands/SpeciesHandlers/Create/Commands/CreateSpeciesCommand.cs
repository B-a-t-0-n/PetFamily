using PetFamily.Application.Abstraction;

namespace PetFamily.Application.SpeciesManagment.Commands.SpeciesHandlers.Create.Commands
{
    public record CreateSpeciesCommand(string Name) : ICommand;
}
