using PetFamily.Accounts.Application.Commands.RefreshTokens.Command;

namespace PetFamily.Accounts.Presentation.Accounts.Requests
{
    public record RefreshTokensRequest(string AccessToken)
    {
        public RefreshTokensCommand ToCommand(Guid refreshToken) => 
            new RefreshTokensCommand(AccessToken, refreshToken);
    }
}