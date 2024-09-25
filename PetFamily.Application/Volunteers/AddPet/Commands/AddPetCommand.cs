using PetFamily.Application.Dtos;

namespace PetFamily.Application.Volunteers.AddPet.Commands
{
    public record AddPetCommand(
        Guid volunteerId,
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
