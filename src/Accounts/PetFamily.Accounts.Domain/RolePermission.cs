namespace PetFamily.Accounts.Domain;

public class RolePermission
{
    public Guid RoleId { get; set; }

    public Role Role { get; set; } = default!;

    public Guid PermissionId { get; set; } = default!;

    public Permission Permission { get; set; } = default!;
}
