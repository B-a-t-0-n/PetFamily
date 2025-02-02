namespace PetFamily.Application.PetManagement.UseCases.PetHandlers.DeletePetPhoto.Commands
{
    public record DeletePetPhotoCommand(Guid VolunteerId, Guid PetId, Guid PetPhotoId);
}
