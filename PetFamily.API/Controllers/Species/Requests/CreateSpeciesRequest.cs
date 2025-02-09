using PetFamily.Application.SpeciesManagment.Commands.SpeciesHandlers.Create.Commands;

namespace PetFamily.API.Controllers.Species.Requests
{
    public record CreateSpeciesRequest(string Name)
    {
        public CreateSpeciesCommand ToCommand() =>
            new(Name);
    }
}
