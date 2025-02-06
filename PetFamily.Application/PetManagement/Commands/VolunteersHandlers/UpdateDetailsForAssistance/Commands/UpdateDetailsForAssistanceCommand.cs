using PetFamily.Application.Abstraction;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.UpdateDetailsForAssistance.Commands
{
    public record UpdateDetailsForAssistanceCommand(Guid Id, IEnumerable<DetailsForAssistanceDto> DetailsForAssistance) : ICommand;
}
