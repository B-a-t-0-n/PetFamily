using PetFamily.Core.Abstractions;

namespace PetFamily.Species.Application.Commands.BreedHandlers.AddBreed.Commands;

public record AddBreedCommand(Guid SpeciesId, string Name) : ICommand;
