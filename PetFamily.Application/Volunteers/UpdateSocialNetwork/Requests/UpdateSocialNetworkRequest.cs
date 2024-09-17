using PetFamily.Application.Volunteers.UpdateSocialNetwork.Dtos;

namespace PetFamily.Application.Volunteers.UpdateSocialNetwork.Requests
{
    public record UpdateSocialNetworkRequest(Guid Id, UpdateSocialNetworkDto SocialNetworkDto);
}
