using PetFamily.Application.Dtos;

namespace PetFamily.Application.Volunteers.UpdateSocialNetwork.Commands
{
    public record UpdateSocialNetworkCommand(Guid Id, IEnumerable<SocialNetworkDto> SocialNetwork);
}
