namespace PetFamily.Accounts.Application.Models;

public record TokenResult(string AccessToken, Guid Jti);
