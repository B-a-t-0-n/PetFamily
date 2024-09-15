using PetFamily.Application.Dtos;

namespace PetFamily.Application.Volunteers.UpdateSocialNetwork.Dtos
{
    public record UpdateSocialNetworkDto(IEnumerable<SocialNetworkDto> SocialNetwork);
}
