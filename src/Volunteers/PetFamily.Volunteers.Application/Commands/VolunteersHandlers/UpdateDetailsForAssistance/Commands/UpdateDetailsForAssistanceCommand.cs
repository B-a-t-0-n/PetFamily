using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateDetailsForAssistance.Commands;

public record UpdateDetailsForAssistanceCommand(Guid Id, IEnumerable<DetailsForAssistanceDto> DetailsForAssistance) : ICommand;
