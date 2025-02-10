using PetFamily.Application.Dtos;
using PetFamily.Application.PetManagement.Commands.PetHandlers.UpdateInfoPet.Commands;

namespace PetFamily.API.Controllers.Volunteer.Requests
{
    public record UpdatePetInfoRequest(
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
        SpeciesAndBreedDto SpeciesAndBreed)
    {
        public UpdatePetInfoCommand ToCommand(Guid volunteerId, Guid petId) =>
            new(volunteerId,
                petId,
                Nickname,
                Description,
                Color,
                HealthInformation,
                Address,
                Size, PhoneNumber,
                IsCastrated, 
                DateOfBirth, 
                IsVaccinated,
                AssistanceStatus, 
                DetailsForAssistance,
                SpeciesAndBreed);
    }
}
