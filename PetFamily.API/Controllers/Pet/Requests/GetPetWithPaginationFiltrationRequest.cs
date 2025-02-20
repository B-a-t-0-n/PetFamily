using PetFamily.Application.PetManagement.Queries.PetHandlers.GetPetWithPaginationFiltration;

namespace PetFamily.API.Controllers.Pet.Requests
{
    public record GetPetWithPaginationFiltrationRequest(
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
        int PageSize)
    {
        public GetPetWithPaginationFiltrationQuery ToQuery() =>
            new(
                VolunteerId,
                Nickname,
                SpeciesId,
                BreedId,
                Color,
                IsCastrated,
                DateOfBirth,
                IsVaccinated,
                AssistanceStatus,
                Сity,
                Street,
                House,
                Flat,
                SerialNumberFrom,
                SerialNumberTo,
                SortBy,
                SortDirection,
                Page,
                PageSize);
    }
}
