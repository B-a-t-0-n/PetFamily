using PetFamily.Application.Abstraction;

namespace PetFamily.Application.PetManagement.Queries.PetHandlers.GetPetWithPaginationFiltration
{
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

}
