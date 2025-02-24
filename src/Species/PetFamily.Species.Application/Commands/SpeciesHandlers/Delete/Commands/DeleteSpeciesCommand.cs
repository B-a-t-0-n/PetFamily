using PetFamily.Core.Abstractions;

namespace PetFamily.Species.Application.Commands.SpeciesHandlers.Delete.Commands;

public record DeleteSpeciesCommand(Guid Id) : ICommand;
