using PetFamily.Volunteers.Application.Commands.PetHandlers.SetMainPhotoPet.Commands;

namespace PetFamily.Volunteers.Presentation.Volunteer.Requests;

public record SetMainPhotoPetRequest(string Path)
{
    public SetMainPhotoPetCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId, Path);
}
