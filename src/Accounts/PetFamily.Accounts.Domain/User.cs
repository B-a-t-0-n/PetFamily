using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    private List<SocialNetwork> _socialNetworks = [];
    private List<Role> _roles = [];

    private User() {}

    public string? Photo { get; set; }

    public IReadOnlyList<Role> Roles => _roles;

    public FullName FullName { get; set; } = null!;

    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;

    public static Result<User, Error> CreateAdmin(string userName, FullName fullName, string email, Role role)
    {
        if (role.Name!.ToUpper() != AdminAccount.ADMIN)
            return Error.Failure("role.name.failure", "role name is not admin");

        return new User
        {
            UserName = userName,
            FullName = fullName,
            Email = email,
            _roles = [role],
        };
    }

    public static Result<User, Error> CreatePartisipant(
        string userName,
        FullName fullName,
        string email,
        Role role,
        IEnumerable<SocialNetwork> socialNetworks)
    {
        if (role.Name!.ToUpper() != PartisipantAccount.PARTISIPANT)
            return Error.Failure("role.name.failure", "role name is not partisipant");

        return new User
        {
            UserName = userName,
            FullName = fullName,
            Email = email,
            _roles = [role],
            _socialNetworks = socialNetworks.ToList()
        };
    }

    public void UpdateSocialNetwork(IEnumerable<SocialNetwork> socialNetworks)
    {
        _socialNetworks = socialNetworks.ToList();
    }
}

