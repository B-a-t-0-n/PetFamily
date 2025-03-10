using PetFamily.Species.Application.Commands.BreedHandlers.AddBreed.Commands;

namespace PetFamily.Species.Presentation.Species.Requests;

public record AddBreedRequest(string Name) 
{
    public AddBreedCommand ToCommand(Guid speciesId) =>
        new(speciesId, Name);
}
