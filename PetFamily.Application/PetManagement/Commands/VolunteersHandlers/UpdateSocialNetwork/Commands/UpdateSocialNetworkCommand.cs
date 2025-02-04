using PetFamily.Application.Abstraction;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.UpdateSocialNetwork.Commands
{
    public record UpdateSocialNetworkCommand(Guid Id, IEnumerable<SocialNetworkDto> SocialNetwork) : ICommand;
}
