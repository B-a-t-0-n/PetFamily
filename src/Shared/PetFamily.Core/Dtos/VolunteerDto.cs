namespace PetFamily.Core.Dtos;

public class VolunteerDto
{
    public Guid Id { get; init; }
    
    public string? Description { get; init; }

    public string PhoneNumber { get; init; } = string.Empty;

}
