using PetFamily.Application.SpeciesManagment.Queries.BreedQuery.GetBreedWithPagination;

namespace PetFamily.API.Controllers.Species.Requests
{
    public record GetBreedWithPaginationRequest(int Page, int PageSize)
    {
        public GetBreedWithPaginationQuery ToQuery(Guid id) =>
            new(id, Page, PageSize);
    }
}
