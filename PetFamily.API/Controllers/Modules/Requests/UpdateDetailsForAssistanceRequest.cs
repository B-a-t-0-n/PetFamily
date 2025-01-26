using PetFamily.Application.Dtos;
using PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Commands;

namespace PetFamily.API.Controllers.Modules.Requests
{
    public record UpdateDetailsForAssistanceRequest(IEnumerable<DetailsForAssistanceDto> DetailsForAssistance)
    {
        public UpdateDetailsForAssistanceCommand ToCommand(Guid volunteerId) =>
            new(volunteerId, DetailsForAssistance);
    }

}
