using PetFamily.Core.Abstractions;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.HardDeletePet.Commands;

public record HardDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;
