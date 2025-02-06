using PetFamily.Application.Abstraction;

namespace PetFamily.Application.SpeciesManagment.Commands.BreedHandlers.AddBreed.Commands
{
    public record AddBreedCommand(Guid SpeciesId, string Name) : ICommand;
}
