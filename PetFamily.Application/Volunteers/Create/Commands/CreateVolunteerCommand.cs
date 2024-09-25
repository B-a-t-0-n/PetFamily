using PetFamily.Application.Dtos;

namespace PetFamily.Application.Volunteers.Create.Commands
{
    public record CreateVolunteerCommand(FullNameDto FullName,
        string? Description,
        int YearsExperience,
        string PhoneNumber,
        IEnumerable<DetailsForAssistanceDto>? DetailsForAssistance,
        IEnumerable<SocialNetworkDto>? SocialNetworks);
}