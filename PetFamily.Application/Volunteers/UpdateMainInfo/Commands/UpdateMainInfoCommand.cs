using PetFamily.Application.Dtos;

namespace PetFamily.Application.Volunteers.UpdateMainInfo.Commands
{
    public record UpdateMainInfoCommand(
        Guid Id,
        FullNameDto FullName,
        string? Description,
        int YearsExperience,
        string PhoneNumber);
}
