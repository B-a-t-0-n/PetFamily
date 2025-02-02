using PetFamily.Application.Dtos;

namespace PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.UpdateMainInfo.Commands
{
    public record UpdateMainInfoCommand(
        Guid Id,
        FullNameDto FullName,
        string? Description,
        int YearsExperience,
        string PhoneNumber);
}
