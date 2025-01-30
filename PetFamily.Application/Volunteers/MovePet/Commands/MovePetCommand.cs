namespace PetFamily.Application.Volunteers.MovePet.Commands
{
    public record MovePetCommand(Guid VolunteerId, Guid PetId, int SerialNumber);
}
