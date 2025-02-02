using PetFamily.Application.Dtos;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.UpdateDetailsForAssistance.Commands;

namespace PetFamily.API.Controllers.Volunteer.Requests
{
    public record UpdateDetailsForAssistanceRequest(IEnumerable<DetailsForAssistanceDto> DetailsForAssistance)
    {
        public UpdateDetailsForAssistanceCommand ToCommand(Guid volunteerId) =>
            new(volunteerId, DetailsForAssistance);
    }

}
