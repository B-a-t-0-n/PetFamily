using System.Globalization;

namespace PetFamily.Core.Models;

public class CustomClaims
{
    public const string Role = nameof(Role);

    public static string Id = nameof(Id);

    public static string Email = nameof(Email);

    public static string Jti = nameof(Jti);
}
