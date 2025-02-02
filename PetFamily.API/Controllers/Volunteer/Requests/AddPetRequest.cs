using PetFamily.Application.Dtos;
using PetFamily.Application.Volunteers.AddPet.Commands;

namespace PetFamily.API.Controllers.Volunteer.Requests
{
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
        IEnumerable<DetailsForAssistanceDto>? DetailsForAssistance,
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
}
