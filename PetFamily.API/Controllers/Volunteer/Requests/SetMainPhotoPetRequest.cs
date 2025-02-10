using PetFamily.Application.PetManagement.Commands.PetHandlers.SetMainPhotoPet.Commands;

namespace PetFamily.API.Controllers.Volunteer.Requests
{
    public record SetMainPhotoPetRequest(string Path)
    {
        public SetMainPhotoPetCommand ToCommand(Guid volunteerId, Guid petId) =>
            new(volunteerId, petId, Path);
    }
}
