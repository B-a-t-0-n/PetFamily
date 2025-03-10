using PetFamily.Species.Application.Queries.BreedQuery.GetBreedWithPagination;

namespace PetFamily.Species.Presentation.Species.Requests;

public record GetBreedWithPaginationRequest(int Page, int PageSize)
{
    public GetBreedWithPaginationQuery ToQuery(Guid id) =>
        new(id, Page, PageSize);
}
