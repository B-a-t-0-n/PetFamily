using PetFamily.Accounts.Application.Commands.RefreshTokens.Command;

namespace PetFamily.Accounts.Presentation.Accounts.Requests
{
    public record RefreshTokensRequest(string AccessToken, Guid RefreshToken)
    {
        public RefreshTokensCommand ToCommand() => new RefreshTokensCommand(AccessToken, RefreshToken);
    }
}