using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Domain;

public class VolunteerAccount
{
    private List<Requisites> _requisites = [];
    private List<string> _certificates = [];

    public const string VOLUNTEER = nameof(VOLUNTEER);

    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = default!;

    public YearsExperience YearsExperience { get; set; } = default!;

    public IReadOnlyList<Requisites> Requisites => _requisites;

    public IReadOnlyList<string> Certificates => _certificates;
}
