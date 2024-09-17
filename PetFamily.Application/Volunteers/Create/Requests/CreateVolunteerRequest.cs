using PetFamily.Application.Dtos;

namespace PetFamily.Application.Volunteers.Create.Requests
{
    public record CreateVolunteerRequest(FullNameDto FullName,
        string? Description,
        int YearsExperience,
        string PhoneNumber,
        IEnumerable<DetailsForAssistanceDto>? DetailsForAssistance,
        IEnumerable<SocialNetworkDto>? SocialNetworks);
}