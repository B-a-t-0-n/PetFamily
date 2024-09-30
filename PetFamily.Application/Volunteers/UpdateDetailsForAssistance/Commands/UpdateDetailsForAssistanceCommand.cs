using PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Dtos;

namespace PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Commands
{
    public record UpdateDetailsForAssistanceCommand(Guid Id, UpdateDetailsForAssistanceDto DetailsForAssistanceDto);
}
