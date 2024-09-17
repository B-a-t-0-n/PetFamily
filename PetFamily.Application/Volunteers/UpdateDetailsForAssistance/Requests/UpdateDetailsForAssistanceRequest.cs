using PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Dtos;

namespace PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Requests
{
    public record UpdateDetailsForAssistanceRequest(Guid Id, UpdateDetailsForAssistanceDto DetailsForAssistanceDto);
}
