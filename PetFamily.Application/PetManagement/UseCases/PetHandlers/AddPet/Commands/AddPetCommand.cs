using PetFamily.Application.Dtos;
namespace PetFamily.Application.PetManagement.UseCases.PetHandlers.AddPet.Commands
{
    public record AddPetCommand(
        Guid VolunteerId,
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
        SpeciesAndBreedDto SpeciesAndBreed);

}
