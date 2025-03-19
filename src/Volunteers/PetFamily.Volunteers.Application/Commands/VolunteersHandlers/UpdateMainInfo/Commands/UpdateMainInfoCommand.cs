using PetFamily.Core.Abstractions;

namespace PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateMainInfo.Commands;

public record UpdateMainInfoCommand(
    Guid Id,
    string? Description,
    string PhoneNumber) : ICommand;
