using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;

namespace PetFamily.Accounts.Application.Commands.Register.Command;

public record RegisterUserCommand(
    string Email,
    string UserName,
    string Password, 
    FullNameDto FullName,
    IEnumerable<SocialNetworkDto>? SocialNetworks) : ICommand;
