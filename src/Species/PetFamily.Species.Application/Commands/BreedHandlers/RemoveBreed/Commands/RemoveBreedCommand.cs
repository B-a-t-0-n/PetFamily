using PetFamily.Core.Abstractions;

namespace PetFamily.Species.Application.Commands.BreedHandlers.RemoveBreed.Commands;

public record RemoveBreedCommand(Guid SpeciesId, Guid BreedId) : ICommand;
