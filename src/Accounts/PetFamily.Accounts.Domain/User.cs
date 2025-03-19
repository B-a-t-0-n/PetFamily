using Microsoft.AspNetCore.Identity;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    private List<SocialNetwork> _socialNetworks = [];

    public string Photo { get; set; } = default!;

    public Guid RoleId { get; set; }
    
    public Role Role { get; set; } = default!;

    public FullName FullName { get; set; } = default!;
    
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
}
