using PetFamily.Application.Dtos;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.UpdateSocialNetwork.Commands;

namespace PetFamily.API.Controllers.Volunteer.Requests
{
    public record UpdateSocialNetworkRequest(IEnumerable<SocialNetworkDto> SocialNetwork)
    {
        public UpdateSocialNetworkCommand ToCommand(Guid volunteerId) =>
            new(volunteerId, SocialNetwork);
    }    
}
