using PetFamily.Core.Abstractions;

namespace PetFamily.Volunteers.Application.Queries.PetHandlers.GetPetWithPaginationFiltration;

public record GetPetWithPaginationFiltrationQuery(
    Guid? VolunteerId,
    string? Nickname,
    Guid? SpeciesId,
    Guid? BreedId,
    string? Color,
    bool? IsCastrated,
    DateTime? DateOfBirth,
    bool? IsVaccinated,
    string? AssistanceStatus,
    string? Сity,
    string? Street,
    string? House,
    string? Flat,
    int? SerialNumberFrom,
    int? SerialNumberTo,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize) : IQuery;
