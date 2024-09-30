using PetFamily.Application.Dtos;

namespace PetFamily.Application.Volunteers.AddPetPtotos.Commands
{
    public record AddPetPhotosCommand(Guid VolunteerId, Guid PetId, IEnumerable<FileDto> Files);
}
