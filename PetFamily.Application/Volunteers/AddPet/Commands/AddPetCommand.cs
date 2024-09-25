using PetFamily.Application.Volunteers.AddPet.Dtos;

namespace PetFamily.Application.Volunteers.AddPet.Commands
{
    public record AddPetCommand(
        Guid volunteerId,
        PetDto PetDto);

}
