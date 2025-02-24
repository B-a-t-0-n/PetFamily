using PetFamily.Species.Application.Commands.SpeciesHandlers.Create.Commands;

namespace PetFamily.Species.Presentation.Species.Requests;

public record CreateSpeciesRequest(string Name)
{
    public CreateSpeciesCommand ToCommand() =>
        new(Name);
}
