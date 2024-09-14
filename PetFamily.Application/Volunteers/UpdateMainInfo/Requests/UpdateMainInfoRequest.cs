using PetFamily.Application.Volunteers.UpdateMainInfo.Dtos;

namespace PetFamily.Application.Volunteers.UpdateMainInfo.Requests
{
    public record UpdateMainInfoRequest(Guid Id, UpdateMainInfoDto MainInfo);
}
