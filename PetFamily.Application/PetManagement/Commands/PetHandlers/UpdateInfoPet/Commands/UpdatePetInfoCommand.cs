using PetFamily.Application.Abstraction;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.PetManagement.Commands.PetHandlers.UpdateInfoPet.Commands
{
    public record UpdatePetInfoCommand(
        Guid VolunteerId,
        Guid PetId,
        string Nickname,
        string Description,
        string Color,
        string HealthInformation,
        AddressDto Address,
        SizeDto Size,
        string PhoneNumber,
        bool IsCastrated,
        DateTime? DateOfBirth,
        bool IsVaccinated,
        string AssistanceStatus,
        IEnumerable<DetailsForAssistanceDto>? DetailsForAssistance,
        SpeciesAndBreedDto SpeciesAndBreed) : ICommand;
}
