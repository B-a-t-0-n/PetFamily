using PetFamily.Application.Dtos;

namespace PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Commands
{
    public record UpdateDetailsForAssistanceCommand(Guid Id, IEnumerable<DetailsForAssistanceDto> DetailsForAssistance);
}
