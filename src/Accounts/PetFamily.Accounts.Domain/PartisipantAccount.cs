namespace PetFamily.Accounts.Domain;

public class PartisipantAccount : User
{
    public List<Guid> FavoritePets { get; set; } = [];
}
