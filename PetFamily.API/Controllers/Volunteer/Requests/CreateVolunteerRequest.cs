using PetFamily.Application.Dtos;
using PetFamily.Application.Volunteers.Create.Commands;

namespace PetFamily.API.Controllers.Volunteer.Requests
{
    public record CreateVolunteerRequest(
        FullNameDto FullName,
        string? Description,
        int YearsExperience,
        string PhoneNumber,
        IEnumerable<DetailsForAssistanceDto>? DetailsForAssistance,
        IEnumerable<SocialNetworkDto>? SocialNetworks)
    {
        public CreateVolunteerCommand ToCommand() =>
            new(FullName, Description, YearsExperience, PhoneNumber, DetailsForAssistance, SocialNetworks);
    }
}
