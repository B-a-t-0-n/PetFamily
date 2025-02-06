using PetFamily.Application.Dtos;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.UpdateMainInfo.Commands;
using PetFamily.Domain.PetMenegment.ValueObjects;

namespace PetFamily.API.Controllers.Volunteer.Requests
{
    public record UpdateMainInfoRequest(
        FullNameDto FullName,
        string? Description,
        int YearsExperience,
        string PhoneNumber)
    {
        public UpdateMainInfoCommand ToCommand(Guid volunteerId) =>
            new(volunteerId, FullName, Description, YearsExperience, PhoneNumber);
    }
}
