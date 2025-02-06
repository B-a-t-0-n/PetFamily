using PetFamily.Application.PetManagement.UseCases.PetHandlers.MovePet.Commands;

namespace PetFamily.API.Controllers.Volunteer.Requests
{
    public record MovePetRequest(int SerialNumber)
    {
        public MovePetCommand ToCommand(Guid volunteerId, Guid PetId) =>
            new(volunteerId, PetId, SerialNumber);
    }
    
}
