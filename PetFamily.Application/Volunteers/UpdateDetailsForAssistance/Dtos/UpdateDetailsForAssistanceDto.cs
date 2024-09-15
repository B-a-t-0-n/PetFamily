using PetFamily.Application.Dtos;

namespace PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Dtos
{
    public record UpdateDetailsForAssistanceDto(IEnumerable<DetailsForAssistanceDto> DetailsForAssistance);
}
