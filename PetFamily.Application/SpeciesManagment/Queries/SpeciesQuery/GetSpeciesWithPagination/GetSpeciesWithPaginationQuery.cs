using PetFamily.Application.Abstraction;

namespace PetFamily.Application.SpeciesManagment.Queries.SpeciesQuery.GetSpeciesWithPagination
{
    public record GetSpeciesWithPaginationQuery(int Page, int PageSize) : IQuery;
}
