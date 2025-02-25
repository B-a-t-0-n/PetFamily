using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Application.Commands.VolunteersHandlers.Create.Commands;

public record CreateVolunteerCommand(FullNameDto FullName,
    string? Description,
    int YearsExperience,
    string PhoneNumber,
    IEnumerable<DetailsForAssistanceDto>? DetailsForAssistance,
    IEnumerable<SocialNetworkDto>? SocialNetworks) : ICommand;