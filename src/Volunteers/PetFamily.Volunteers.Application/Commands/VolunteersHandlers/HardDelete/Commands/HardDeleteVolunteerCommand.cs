using PetFamily.Core.Abstractions;

namespace PetFamily.Volunteers.Application.Commands.VolunteersHandlers.HardDelete.Commands;

public record HardDeleteVolunteerCommand(Guid Id) : ICommand;
