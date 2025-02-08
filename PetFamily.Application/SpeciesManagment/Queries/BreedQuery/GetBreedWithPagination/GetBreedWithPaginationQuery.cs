using PetFamily.Application.Abstraction;

namespace PetFamily.Application.SpeciesManagment.Queries.BreedQuery.GetBreedWithPagination
{
    public record GetBreedWithPaginationQuery(Guid SpeciesId ,int Page, int PageSize) : IQuery;
}
