using PetFamily.Core.Dtos;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.Create.Commands;

namespace PetFamily.Volunteers.Presentation.Volunteer.Requests;

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
