using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Domain;

public class AdminAccount
{
    public const string ADMIN = nameof(ADMIN);
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = default!;
}
