using PetFamily.Application.SpeciesManagment.Commands.BreedHandlers.AddBreed.Commands;

namespace PetFamily.API.Controllers.Species.Requests
{
    public record AddBreedRequest(string Name) 
    {
        public AddBreedCommand ToCommand(Guid speciesId) =>
            new(speciesId, Name);
    }
}
