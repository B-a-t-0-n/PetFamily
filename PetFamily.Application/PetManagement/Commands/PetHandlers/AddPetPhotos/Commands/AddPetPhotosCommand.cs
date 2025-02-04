using PetFamily.Application.Abstraction;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.PetManagement.UseCases.PetHandlers.AddPetPhotos.Commands
{
    public record AddPetPhotosCommand(Guid VolunteerId, Guid PetId, IEnumerable<CreateFileDto> Files) : ICommand;
}
