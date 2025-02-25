using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.UpdateInfoPet.Commands;

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
