using PetFamily.Core.Abstractions;

namespace PetFamily.Volunteers.Application.Commands.VolunteersHandlers.SoftDelete.Commands;

public record SoftDeleteVolunteerCommand(Guid Id) : ICommand;
