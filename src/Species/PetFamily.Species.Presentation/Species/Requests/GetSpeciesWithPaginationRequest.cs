using PetFamily.Species.Application.Queries.SpeciesQuery.GetSpeciesWithPagination;

namespace PetFamily.Species.Presentation.Species.Requests;

public record GetSpeciesWithPaginationRequest(int Page, int PageSize)
{
    public GetSpeciesWithPaginationQuery ToQuery() =>
        new(Page, PageSize);
}
