namespace PetFamily.Accounts.Domain;

public class PartisipantAccount
{
    public const string PARTISIPANT = nameof(PARTISIPANT);

    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = default!;

    public List<Guid> FavoritePets { get; set; } = [];
}
