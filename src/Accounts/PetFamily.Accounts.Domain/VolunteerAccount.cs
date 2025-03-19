using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Accounts.Domain;

public class VolunteerAccount : User
{
    private List<Requisites> _requisites = [];
    private List<string> _certificates = [];

    public YearsExperience YearsExperience { get; set; } = default!;

    public IReadOnlyList<Requisites> Requisites => _requisites;

    public IReadOnlyList<string> Certificates => _certificates;
}
