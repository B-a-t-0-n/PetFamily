using PetFamily.Core.Dtos;
using PetFamily.Volunteers.Application.Commands.PetHandlers.AddPet.Commands;

namespace PetFamily.Volunteers.Presentation.Volunteer.Requests;

public record AddPetRequest(
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
    IEnumerable<RequisitesDto>? DetailsForAssistance,
    SpeciesAndBreedDto SpeciesAndBreed)
{
    public AddPetCommand ToCommand(Guid volunteerId) =>
        new(volunteerId,
            Nickname,
            Description,
            Color,
            HealthInformation,
            Address,
            Size,
            PhoneNumber,
            IsCastrated,
            DateOfBirth,
            IsVaccinated,
            AssistanceStatus,
            DetailsForAssistance, 
            SpeciesAndBreed);
}
