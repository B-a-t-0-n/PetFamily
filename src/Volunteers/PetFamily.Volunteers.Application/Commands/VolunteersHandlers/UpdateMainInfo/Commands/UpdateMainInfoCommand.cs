using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateMainInfo.Commands;

public record UpdateMainInfoCommand(
    Guid Id,
    FullNameDto FullName,
    string? Description,
    int YearsExperience,
    string PhoneNumber) : ICommand;
