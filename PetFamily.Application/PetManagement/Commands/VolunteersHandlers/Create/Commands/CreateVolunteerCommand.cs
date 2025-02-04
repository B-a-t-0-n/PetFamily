using PetFamily.Application.Abstraction;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.Create.Commands
{
    public record CreateVolunteerCommand(FullNameDto FullName,
        string? Description,
        int YearsExperience,
        string PhoneNumber,
        IEnumerable<DetailsForAssistanceDto>? DetailsForAssistance,
        IEnumerable<SocialNetworkDto>? SocialNetworks) : ICommand;
}