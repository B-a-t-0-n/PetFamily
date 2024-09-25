using PetFamily.Application.Volunteers.UpdateMainInfo.Dtos;

namespace PetFamily.Application.Volunteers.UpdateMainInfo.Commands
{
    public record UpdateMainInfoCommand(Guid Id, UpdateMainInfoDto MainInfo);
}
