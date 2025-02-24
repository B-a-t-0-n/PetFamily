using PetFamily.Core.Abstractions;

namespace PetFamily.Species.Application.Commands.SpeciesHandlers.Create.Commands;

public record CreateSpeciesCommand(string Name) : ICommand;
