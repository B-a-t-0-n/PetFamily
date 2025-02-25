using PetFamily.Core.Abstractions;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.SetMainPhotoPet.Commands;

public record SetMainPhotoPetCommand(Guid VolunteerId, Guid PetId, string Path) : ICommand;
