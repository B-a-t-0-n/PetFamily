using PetFamily.Core.Abstractions;

namespace PetFamily.Volunteers.Application.Commands.VolunteersHandlers.Create.Commands;

public record CreateVolunteerCommand(
    string? Description,
    string PhoneNumber) : ICommand;