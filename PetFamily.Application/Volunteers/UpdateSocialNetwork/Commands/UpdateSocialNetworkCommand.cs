using PetFamily.Application.Volunteers.UpdateSocialNetwork.Dtos;

namespace PetFamily.Application.Volunteers.UpdateSocialNetwork.Commands
{
    public record UpdateSocialNetworkCommand(Guid Id, UpdateSocialNetworkDto SocialNetworkDto);
}
