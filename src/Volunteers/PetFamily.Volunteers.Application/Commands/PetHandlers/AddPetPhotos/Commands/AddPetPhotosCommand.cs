using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.AddPetPhotos.Commands;

public record AddPetPhotosCommand(Guid VolunteerId, Guid PetId, IEnumerable<CreateFileDto> Files) : ICommand;
