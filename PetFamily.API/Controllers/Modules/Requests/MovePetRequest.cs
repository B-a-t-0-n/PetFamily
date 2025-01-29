using PetFamily.Application.Volunteers.MovePet.Commands;

namespace PetFamily.API.Controllers.Modules.Requests
{
    public record MovePetRequest(int SerialNumber)
    {
        public MovePetCommand ToCommand(Guid volunteerId, Guid PetId) =>
            new(volunteerId, PetId, SerialNumber);
    }
    
}
