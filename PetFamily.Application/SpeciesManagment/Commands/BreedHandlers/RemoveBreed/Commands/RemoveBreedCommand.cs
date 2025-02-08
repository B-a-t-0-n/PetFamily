using PetFamily.Application.Abstraction;

namespace PetFamily.Application.SpeciesManagment.Commands.BreedHandlers.RemoveBreed.Commands
{
    public record RemoveBreedCommand(Guid SpeciesId, Guid BreedId) : ICommand;
}
