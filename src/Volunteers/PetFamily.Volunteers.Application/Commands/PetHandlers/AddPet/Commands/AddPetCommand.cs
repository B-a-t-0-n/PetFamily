using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.AddPet.Commands;

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
    IEnumerable<RequisitesDto>? DetailsForAssistance,
    SpeciesAndBreedDto SpeciesAndBreed) : ICommand;
