using PetFamily.Application.Dtos;

namespace PetFamily.Application.Volunteers.UpdateMainInfo.Dtos
{
    public record UpdateMainInfoDto(FullNameDto FullName,
        string? Description,
        int YearsExperience,
        string PhoneNumber);
}
