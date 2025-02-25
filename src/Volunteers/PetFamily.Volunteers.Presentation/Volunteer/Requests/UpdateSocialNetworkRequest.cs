using PetFamily.Core.Dtos;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateSocialNetwork.Commands;
namespace PetFamily.Volunteers.Presentation.Volunteer.Requests;

public record UpdateSocialNetworkRequest(IEnumerable<SocialNetworkDto> SocialNetwork)
{
    public UpdateSocialNetworkCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, SocialNetwork);
}
