namespace PetFamily.Application.Volunteers.DeletePetPhoto.Commands
{
    public record DeletePetPhotoCommand(Guid VolunteerId, Guid PetId, Guid PetPhotoId);
}
