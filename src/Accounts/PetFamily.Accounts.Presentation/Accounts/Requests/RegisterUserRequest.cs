using PetFamily.Accounts.Application.Commands.Register.Command;
using PetFamily.Core.Dtos;

namespace PetFamily.Accounts.Presentation.Accounts.Requests;

public record RegisterUserRequest(
    string Email,
    string UserName, 
    string Password, 
    FullNameDto FullName,
    IEnumerable<SocialNetworkDto>? SocialNetworks)
{
    public RegisterUserCommand ToCommand() => new(Email, UserName, Password, FullName, SocialNetworks);
}
