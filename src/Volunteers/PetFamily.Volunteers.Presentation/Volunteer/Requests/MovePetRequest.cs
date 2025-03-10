using PetFamily.Volunteers.Application.Commands.PetHandlers.MovePet.Commands;

namespace PetFamily.Volunteers.Presentation.Volunteer.Requests;

public record MovePetRequest(int SerialNumber)
{
    public MovePetCommand ToCommand(Guid volunteerId, Guid PetId) =>
        new(volunteerId, PetId, SerialNumber);
}
