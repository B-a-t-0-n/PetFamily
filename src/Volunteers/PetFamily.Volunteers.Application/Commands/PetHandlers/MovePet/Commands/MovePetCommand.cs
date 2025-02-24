using PetFamily.Core.Abstractions;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.MovePet.Commands;

public record MovePetCommand(Guid VolunteerId, Guid PetId, int SerialNumber) : ICommand;
