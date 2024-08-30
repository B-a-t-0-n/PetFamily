using PetFamily.Application.Volunteers.CreateVolunteer.Dtos;

namespace PetFamily.Application.Volunteers.CreateVolunteer.Requests
{
    public record CreateVolunteerRequest(FullNameDto FullName,
        string? Description,
        int YearsExperience,
        NumberPetsDto NumberPets,
        string PhoneNumber,
        IEnumerable<DetailsForAssistanceDto>? DetailsForAssistance,
        IEnumerable<SocialNetworkDto>? SocialNetworks);
}