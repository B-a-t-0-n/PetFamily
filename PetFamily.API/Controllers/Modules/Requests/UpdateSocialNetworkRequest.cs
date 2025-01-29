using PetFamily.Application.Dtos;
using PetFamily.Application.Volunteers.UpdateSocialNetwork.Commands;


namespace PetFamily.API.Controllers.Modules.Requests
{
    public record UpdateSocialNetworkRequest(IEnumerable<SocialNetworkDto> SocialNetwork)
    {
        public UpdateSocialNetworkCommand ToCommand(Guid volunteerId) =>
            new(volunteerId, SocialNetwork);
    }    
}
