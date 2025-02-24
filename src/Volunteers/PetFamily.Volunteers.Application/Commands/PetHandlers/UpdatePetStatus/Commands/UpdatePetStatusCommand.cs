using PetFamily.Core.Abstractions;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.UpdatePetStatus.Commands;

public record UpdatePetStatusCommand(Guid VolunteerId, Guid PetId, string AssistanceStatus) : ICommand;
