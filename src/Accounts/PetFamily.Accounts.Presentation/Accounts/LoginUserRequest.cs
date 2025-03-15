using PetFamily.Accounts.Application.Commands.Login;
using System.ComponentModel.DataAnnotations;

namespace PetFamily.Accounts.Presentation.Accounts
{
    public record LoginUserRequest(string Email, string Password)
    {
        public LoginCommand ToCommand() => 
            new(Email, Password);   
    }
}