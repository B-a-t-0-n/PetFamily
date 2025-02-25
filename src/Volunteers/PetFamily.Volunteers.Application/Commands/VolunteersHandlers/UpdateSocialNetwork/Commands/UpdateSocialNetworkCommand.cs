using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateSocialNetwork.Commands;

public record UpdateSocialNetworkCommand(Guid Id, IEnumerable<SocialNetworkDto> SocialNetwork) : ICommand;
