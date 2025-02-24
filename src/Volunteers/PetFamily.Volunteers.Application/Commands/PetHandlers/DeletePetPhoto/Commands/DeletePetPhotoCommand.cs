using PetFamily.Core.Abstractions;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.DeletePetPhoto.Commands;

public record DeletePetPhotoCommand(Guid VolunteerId, Guid PetId, Guid PetPhotoId) : ICommand;
