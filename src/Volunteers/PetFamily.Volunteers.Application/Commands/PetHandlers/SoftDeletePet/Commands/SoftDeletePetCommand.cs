using PetFamily.Core.Abstractions;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.SoftDeletePet.Commands;

public record SoftDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;
