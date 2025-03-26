namespace PetFamily.Core.Dtos;

public class UserDto
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Photo { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public IEnumerable<RoleDto> Roles { get; set; } = null!;

    public IEnumerable<SocialNetworkDto> SocialNetworks { get; set; } = null!;

    public AdminAccountDto? AdminAccount { get; set; }

    public PartisipantAccountDto? PartisipantAccount { get; set; }

    public VolunteerAccountDto? VolunteerAccount { get; set; }
}
